using System.Security.Claims;

using Librebooks.Areas.Identity.Models;
using Librebooks.Areas.Identity.Services;
using Librebooks.Core.Identity;
using Librebooks.CoreLib.Operations;
using Librebooks.Extensions.Mvc;
using Librebooks.Models.Entity.IdentitySpace;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using static Librebooks.Areas.Identity.Models.AuthReqModels;
using static Librebooks.Areas.Identity.Models.AuthRespDTOs;

namespace Librebooks.Areas.Identity.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController (UserManagerExtension userManager,
        SignInManagerExtension signInManager,
        ILogger<SessionControllerBase> logger,
        IOptions<JwtParams> jwtParameters,
        IConfiguration config)
        : SessionControllerBase(userManager, signInManager, logger)
    {
        private readonly JwtParams jwtParameters = jwtParameters.Value;
        private readonly IConfiguration config = config;

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> FindUserAsync ([FromBody] UsernameModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager!.FindByEmailAsync(input.Email!);

            if (user == null)
                return NoContent();
            else
                return Ok(FindUserDto.Create(user));
        }

        [HttpPost]
        [Route("verify-email")]
        public IActionResult VerifyEmail ([FromBody] VerifyModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return userManager!.VerifyCode(input.Email!, input.Reason!, input.Code!, input.CodeHashString!)
                ? Ok(TransactionResult.Success) : Ok(TransactionResult.Failure());
        }

        [HttpPost]
        [Route("send-verification-code")]
        public async Task<IActionResult> SendVerificationCodeAsync ([FromBody] SendVerificationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.Reason == EmailVerificationTypes.PasswordReset)
            {
                var user = await userManager!.FindByEmailAsync(model.Email!);
                if (user == null)
                    return Ok(TransactionResult.Failure(TransactionError.Create(nameof(model.Email), IdentityErrorDescriptions.InvalidEmail)));
            }

            var (Code, CodeHashString) = userManager!.GenerateVerificationCode(model.Email!, model.Reason!);

            logger!.LogInformation("Verification Code {code} was created for email {email}", Code, model.Email);

            return Ok(TransactionResult<SendVerificationDto>.Success(new SendVerificationDto(CodeHashString)));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync ([FromBody] LoginModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager!.FindByEmailAsync(input.Email!);

            if (user == null)
                return Ok(TransactionResult<LoginDto>
                    .Failure(TransactionError.Create(nameof(input.Email),
                        IdentityErrorDescriptions.InvalidEmail)));

            if (!user.EmailConfirmed)
                return Ok(TransactionResult.Failure(TransactionError.Create("Unverified", "true")));

            var result = await signInManager!.CheckPasswordSignInAsync(user, input.Password!, false);

            if (result.Succeeded)
            {
                user.DateLastLoggedIn = DateTime.Now;
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
                return Ok(TransactionResult<LoginDto>
                    .Failure(TransactionError.Create(nameof(input.Password),
                        IdentityErrorDescriptions.PasswordMismatch)));
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync ([FromBody] RegisterModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager!.FindByEmailAsync(input.Email!);

            if (user != null)
                return Ok(TransactionResult.Failure(TransactionError.Create(nameof(input.Email), IdentityErrorDescriptions.DuplicateEmail)));

            user = new User
            {
                Email = input.Email!.ToLower(),
                UserName = input.Email.ToLower(),
                FirstName = input.FirstName,
                LastName = input.LastName,
                NormalizedEmail = userManager.NormalizeEmail(input.Email),
                NormalizedUserName = userManager.NormalizeName(input.Email),
                DateLastLoggedIn = DateTime.Now,
                DateRegistered = DateTime.Now,
                EmailConfirmed = false
            };

            var createResult = await userManager.CreateAsync(user, input.Password!);

            if (createResult.Succeeded)
            {
                logger!.LogInformation("User created with email {email}", user.Email);
                user = await userManager.FindByEmailAsync(user.Email);

                user!.DateLastLoggedIn = DateTime.Now;
                await userManager.UpdateAsync(user);
                await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.Email!));

                var nameClaim = (await userManager.GetClaimsAsync(user))
                    .FirstOrDefault(p => p.Type == ClaimTypes.Name);

                return Ok(TransactionResult.Success);
            }
            else
            {
                IList<TransactionError> errors = [];

                if (createResult.Errors.Count() > 0)
                    foreach (var error in createResult.Errors)
                    {
                        if (error.Code == nameof(userManager.ErrorDescriber.DuplicateEmail)
                            && errors.FirstOrDefault(p => p.Code == nameof(RegisterModel.Email)) == null)
                            errors.Add(new TransactionError(nameof(RegisterModel.Email), IdentityErrorDescriptions.DuplicateEmail));

                        if (error.Code.Contains("Password")
                            && errors.FirstOrDefault(p => p.Code == nameof(RegisterModel.Password)) == null)
                            errors.Add(new TransactionError(nameof(AuthReqModels.RegisterModel.Email), IdentityErrorDescriptions.PasswordWeak));
                    }
                else
                    errors.Add(new TransactionError(nameof(RegisterModel.Email), "Unable to register user. Please try again."));

                return Ok(TransactionResult.Failure(errors.ToArray()));
            }
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync ([FromBody] ResetPasswordModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager!.FindByEmailAsync(input.Email!);

            if (user == null)
                return NoContent();

            var verified = userManager.VerifyCode(input.Email!, EmailVerificationTypes.PasswordReset, input.Code!, input.CodeHashString!);

            if (!verified)
                return Ok(TransactionResult.Failure(TransactionError.Create(nameof(input.Email),
                    IdentityErrorDescriptions.UnVerifiedEmail)));

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
            logger!.LogInformation("Session by: {0}", User.Identity!.Name);

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
}