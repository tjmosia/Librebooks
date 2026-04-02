using System.Security.Claims;
using Librebooks.Areas.Identity.Data;
using Librebooks.Areas.Identity.Models;
using Librebooks.Areas.Identity.Models.Authentication.Models;
using Librebooks.Areas.Identity.Services;
using Librebooks.Core.Identity;
using Librebooks.CoreLib.Operations;
using Librebooks.Data;
using Librebooks.Extensions.Mvc;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Providers;
using Librebooks.Providers.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Librebooks.Areas.Identity.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController (UserManagerExtension userManager,
	SignInManagerExtension signInManager,
	ILogger<SessionControllerBase> logger,
	VerificationStore verificationStore,
	IOptions<JwtParams> jwtParameters,
	IVerificationManager verificationManager,
	AppDbContext context)
	: SessionControllerBase(userManager, signInManager, logger)
{
	private readonly JwtParams jwtParameters = jwtParameters.Value;
	private readonly IVerificationManager verificationManager = verificationManager;
	private readonly VerificationStore verificationStore = verificationStore;
	private readonly AppDbContext context = context;

	[HttpPost("")]
	public async Task<IActionResult> FindUserAsync ([FromBody] UsernameModel.Request input)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var user = await userManager!.FindByEmailAsync(input.Email!);

		if (user == null)
			return NotFound();
		else
			return Ok(new FindUserDto(user));
	}

	[HttpPost("login")]
	public async Task<IActionResult> LoginAsync ([FromBody] LoginModel.Request input)
	{
		var validation = LoginModel.Validate(input);

		if (!validation.IsValid)
			return BadRequest(validation.Errors.Select(p => Error.Create(p.PropertyName, p.ErrorMessage)));

		var user = await userManager!.FindByEmailAsync(input.Email!);

		if (user == null)
			return Ok(Result.Failure(Error.Create(nameof(input.Email),
					IdentityErrorDescriptions.InvalidEmail)));

		if (!user.EmailConfirmed)
			return Ok(Result.Failure(Error.Create("Unverified", "true")));

		var result = await signInManager!.CheckPasswordSignInAsync(user, input.Password!, false);

		if (result.Succeeded)
		{
			user.DateLastLoggedIn = DateOnly.FromDateTime(DateTime.Now);
			await userManager.UpdateAsync(user);

			var nameClaim = (await userManager.GetClaimsAsync(user))
				.FirstOrDefault(p => p.Type == ClaimTypes.Name);

			var (Token, ExpiryDate) = signInManager.GenerateJsonWebToken(nameClaim!);
			SetAuthenticationCookie(HttpContext, Token, ExpiryDate);

			var claims = await userManager.GetClaimsAsync(user);
			var roles = await userManager.GetUserRolesAsync(user);

			return Ok(Result<object>
				.Success(new LoginDto(user, [.. roles], [.. claims])));
		}
		else
		{
			return Ok(Result.Failure(Error.Create(nameof(input.Password),
					IdentityErrorDescriptions.PasswordMismatch)));
		}
	}

	[HttpPost("register")]
	public async Task<IActionResult> RegisterAsync ([FromBody] RegisterModel.Request input)
	{
		var modelState = RegisterModel.Validate(input);

		if (!modelState.IsValid)
			return BadRequest(modelState.Errors.Select(p => Error.Create(p.PropertyName, p.ErrorMessage)));

		var user = await userManager!.FindByEmailAsync(input.Email!);

		if (user != null)
			return Ok(Result.Failure(Error.Create(nameof(input.Email), IdentityErrorDescriptions.DuplicateEmail)));

		var verifyEmail = await verificationManager.VerifyAsync(input.Email!, EmailVerificationTypes.Registration, input.Code!);

		if (!verifyEmail.Succeeded)
			return Ok(Result.Failure([.. verifyEmail.Errors.Select(p => Error.Create(nameof(input.Code), p.Description))]));

		user = new User
		{
			Email = input.Email!.ToLower(),
			UserName = input.Email.ToLower(),
			FirstName = input.FirstName,
			LastName = input.LastName,
			Gender = input.Gender,
			NormalizedEmail = userManager.NormalizeEmail(input.Email),
			NormalizedUserName = userManager.NormalizeName(input.Email),
			DateLastLoggedIn = DateOnly.FromDateTime(DateTime.Now),
			DateRegistered = DateOnly.FromDateTime(DateTime.Now),
			EmailConfirmed = true
		};

		var createResult = await userManager.CreateAsync(user, input.Password!);

		if (createResult.Succeeded)
		{
			await verificationStore.DeleteAsync(verifyEmail.Model!);
			user = await userManager.FindByEmailAsync(user.Email);

			if (user == null)
				return Ok(Result.Failure(Error.Create("", "Something went wrong. Please try again later.")));

			await userManager.AddClaimAsync(user!, new Claim(ClaimTypes.Name, user!.Email!));

			var nameClaim = (await userManager.GetClaimsAsync(user))
				.FirstOrDefault(p => p.Type == ClaimTypes.Name);

			var (Token, ExpiryDate) = signInManager!.GenerateJsonWebToken(nameClaim!);
			SetAuthenticationCookie(HttpContext, Token, ExpiryDate);

			var claims = await userManager.GetClaimsAsync(user);
			var roles = await userManager.GetUserRolesAsync(user);

			return Ok(Result<object>
				.Success(new LoginDto(user, [.. roles], [.. claims])));
		}
		else
		{
			IList<Error> errors = [];

			if (createResult.Errors.Any())
				foreach (var error in createResult.Errors)
				{
					if (error.Code == nameof(userManager.ErrorDescriber.DuplicateEmail)
						&& errors.FirstOrDefault(p => p.Code == nameof(input.Email)) == null)
						errors.Add(new Error(nameof(input.Email), IdentityErrorDescriptions.DuplicateEmail));

					if (error.Code.Contains("Password")
						&& errors.FirstOrDefault(p => p.Code == nameof(input.Password)) == null)
						errors.Add(new Error(nameof(input.Password), IdentityErrorDescriptions.PasswordWeak));
				}
			else
				errors.Add(new Error("", "Unable to register user. Please try again."));

			return Ok(Result.Failure([.. errors]));
		}
	}

	[HttpPost("reset-password")]
	public async Task<IActionResult> ResetPasswordAsync ([FromBody] ResetPasswordModel.Request input)
	{
		var validation = ResetPasswordModel.Validate(input);

		if (!validation.IsValid)
			return BadRequest(validation.Errors.Select(p => Error.Create(p.PropertyName, p.ErrorMessage)));

		var verifyEmail = await verificationManager.VerifyAsync(input.Email!, EmailVerificationTypes.PasswordReset, input.Code!);

		if (!verifyEmail.Succeeded)
			return Ok(Result.Failure([.. verifyEmail.Errors.Select(p => Error.Create(nameof(input.Code), p.Description))]));

		var user = await userManager!.FindByEmailAsync(input.Email!);

		if (user == null)
			return NotFound();

		var result = await userManager.CheckPasswordAsync(user, input.Password!);

		if (result)
			return Ok(Result.Failure(Error.Create(nameof(input.Password), "You are already using this password.")));

		var resetPasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);
		var resetPasswordResult = await userManager.ResetPasswordAsync(user, resetPasswordToken, input.Password!);

		if (resetPasswordResult.Succeeded)
		{
			await verificationStore.DeleteAsync(verifyEmail.Model!);
			return Ok(Result.Success);
		}

		foreach (var error in resetPasswordResult.Errors)
			logger!.LogInformation("Model Error: Code: {code} - Message: {description}", error.Code, error.Description);

		return Ok(Result.Failure(Error.Create(nameof(input.Password), "Password doesn't meet requirements.")));
	}

	[HttpPost]
	[Authorize]
	[Route("confirm-login")]
	public async Task<IActionResult> ConfirmSignInAsync ()
	{
		if (!string.IsNullOrEmpty(User!.Identity!.Name))
		{
			var user = await userManager!.FindByEmailAsync(User!.Identity!.Name);

			if (user != null)
			{
				var claims = await userManager.GetClaimsAsync(user);
				var roles = await userManager.GetUserRolesAsync(user);

				return Ok(new LoginDto(user, [.. roles], [.. claims]));
			}
		}

		return Unauthorized();
	}

	[HttpPost]
	[Authorize]
	[Route("logout")]
	public async Task<IActionResult> Logout ()
	{
		HttpContext.Request.Headers.Authorization = "";
		var accessTokenCookie = HttpContext.Request.Cookies[JwtTokenKeys.AccessToken];

		if (accessTokenCookie != null)
			HttpContext.Response.Cookies.Delete(JwtTokenKeys.AccessToken, GetCookieOptions());

		return Ok();
	}

	private static void SetAuthenticationCookie (HttpContext context, string token, DateTimeOffset expires)
	{
		context.Response.Cookies.Append(JwtTokenKeys.AccessToken, token, GetCookieOptions());
	}

	private static CookieOptions GetCookieOptions ()
	{
		return new CookieOptions
		{
			HttpOnly = true,
			IsEssential = true,
			Secure = true,
			SameSite = SameSiteMode.None,
			Domain = "localhost",
			MaxAge = TimeSpan.FromDays(1),
		};
	}
}