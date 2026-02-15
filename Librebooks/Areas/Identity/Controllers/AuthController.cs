using System.Security.Claims;
using Librebooks.Areas.Identity.Data;
using Librebooks.Areas.Identity.Models;
using Librebooks.Areas.Identity.Models.Authentication.Models;
using Librebooks.Areas.Identity.Services;
using Librebooks.Core.Identity;
using Librebooks.CoreLib.Operations;
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
	IOptions<JwtParams> jwtParameters, IVerificationManager verificationManager)
	: SessionControllerBase(userManager, signInManager, logger)
{
	private readonly JwtParams jwtParameters = jwtParameters.Value;
	private readonly IVerificationManager verificationManager = verificationManager;
	private readonly VerificationStore verificationStore = verificationStore;

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
			return BadRequest(validation.Errors.Select(p => TransactionError.Create(p.PropertyName, p.ErrorMessage)));

		var user = await userManager!.FindByEmailAsync(input.Email!);

		if (user == null)
			return Ok(TransactionResult.Failure(TransactionError.Create(nameof(input.Email),
					IdentityErrorDescriptions.InvalidEmail)));

		if (!user.EmailConfirmed)
			return Ok(TransactionResult.Failure(TransactionError.Create("Unverified", "true")));

		var result = await signInManager!.CheckPasswordSignInAsync(user, input.Password!, false);

		if (result.Succeeded)
		{
			user.DateLastLoggedIn = DateOnly.FromDateTime(DateTime.Now);
			await userManager.UpdateAsync(user);

			var nameClaim = (await userManager.GetClaimsAsync(user))
				.FirstOrDefault(p => p.Type == ClaimTypes.Name);

			var (Token, ExpiryDate) = signInManager.GenerateJsonWebToken(nameClaim!);
			logger!.LogInformation($"Jwt Token: {Token}");
			SetAuthenticationCookie(HttpContext, Token, ExpiryDate);

			return Ok(TransactionResult<object>
				.Success(signInManager!.GenerateUserSessionDTO(user)));
		}
		else
		{
			return Ok(TransactionResult.Failure(TransactionError.Create(nameof(input.Password),
					IdentityErrorDescriptions.PasswordMismatch)));
		}
	}

	[HttpPost("register")]
	public async Task<IActionResult> RegisterAsync ([FromBody] RegisterModel.Request input)
	{
		var modelState = RegisterModel.Validate(input);

		if (!modelState.IsValid)
			return BadRequest(modelState.Errors.Select(p => TransactionError.Create(p.PropertyName, p.ErrorMessage)));

		var user = await userManager!.FindByEmailAsync(input.Email!);

		if (user != null)
			return Ok(TransactionResult.Failure(TransactionError.Create(nameof(input.Email), IdentityErrorDescriptions.DuplicateEmail)));

		var verifyEmail = await verificationManager.VerifyAsync(input.Email!, EmailVerificationTypes.Registration, input.Code!);

		if (!verifyEmail.Succeeded)
			return BadRequest(TransactionError.Create(nameof(input.Code), "Code is invalid."));

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

			user!.DateLastLoggedIn = DateOnly.FromDateTime(DateTime.Now);
			_ = await userManager.UpdateAsync(user);
			_ = await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.Email!));

			var nameClaim = (await userManager.GetClaimsAsync(user))
				.FirstOrDefault(p => p.Type == ClaimTypes.Name);

			return Ok(TransactionResult.Success);
		}
		else
		{
			IList<TransactionError> errors = [];

			if (createResult.Errors.Any())
				foreach (var error in createResult.Errors)
				{
					if (error.Code == nameof(userManager.ErrorDescriber.DuplicateEmail)
						&& errors.FirstOrDefault(p => p.Code == nameof(input.Email)) == null)
						errors.Add(new TransactionError(nameof(input.Email), IdentityErrorDescriptions.DuplicateEmail));

					if (error.Code.Contains("Password")
						&& errors.FirstOrDefault(p => p.Code == nameof(input.Password)) == null)
						errors.Add(new TransactionError(nameof(input.Email), IdentityErrorDescriptions.PasswordWeak));
				}
			else
				errors.Add(new TransactionError(nameof(input.Email), "Unable to register user. Please try again."));

			return Ok(TransactionResult.Failure([.. errors]));
		}
	}

	[HttpPost("reset-password")]
	public async Task<IActionResult> ResetPasswordAsync ([FromBody] ResetPasswordModel.Request input)
	{
		var validation = ResetPasswordModel.Validate(input);

		if (!validation.IsValid)
			return BadRequest(validation.Errors.Select(p => TransactionError.Create(p.PropertyName, p.ErrorMessage)));

		var verifyEmail = await verificationManager.VerifyAsync(input.Email!, EmailVerificationTypes.PasswordReset, input.Code!);

		if (!verifyEmail.Succeeded)
			return BadRequest(TransactionResult.Failure(verifyEmail.Errors));

		var user = await userManager!.FindByEmailAsync(input.Email!);

		if (user == null)
			return NotFound();

		var result = await userManager.CheckPasswordAsync(user, input.Password!);

		if (result)
			return Ok(TransactionResult.Failure(TransactionError.Create(nameof(input.Password), "Password matches your current password.")));

		var resetPasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);
		var resetPasswordResult = await userManager.ResetPasswordAsync(user, resetPasswordToken, input.Password!);

		if (resetPasswordResult.Succeeded)
			return Ok(TransactionResult.Success);

		foreach (var error in resetPasswordResult.Errors)
			logger!.LogInformation("Model Error: Code: {code} - Message: {description}", error.Code, error.Description);

		return Ok(TransactionResult.Failure(TransactionError.Create(nameof(input.Password), "Password doesn't meet requirements.")));
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
				return Ok(TransactionResult<object>.Success(signInManager!.GenerateUserSessionDTO(user)));
		}

		return Unauthorized();
	}

	private void SetAuthenticationCookie (HttpContext context, string token, DateTimeOffset expires)
	{
		context.Response.Cookies.Append(JwtTokenKeys.AccessToken, token, new CookieOptions
		{
			HttpOnly = true,
			Expires = expires,
			IsEssential = true,
			Secure = false,
			SameSite = SameSiteMode.Strict,
			Domain = "localhost",
			MaxAge = TimeSpan.FromMinutes(jwtParameters.ExpiryTimeInMinutes),
		});
	}
}