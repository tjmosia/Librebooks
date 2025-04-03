using System.Security.Claims;

using Librebooks.Areas.Identity.Models;
using Librebooks.Areas.Identity.Services;
using Librebooks.CoreLib.Operations;
using Librebooks.Extensions.Mvc;
using Librebooks.Models.Entity.IdentitySpace;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using static Librebooks.Areas.Identity.Models.AccountReqModels;

namespace Librebooks.Areas.Identity.Controllers
{
    [Authorize]
    [Route("account")]
    [ApiController]
    public class AccountController (
            UserManagerExtension userManager,
            ILogger<SessionControllerBase> logger,
            SignInManagerExtension signInManager,
            IOptions<JwtParams> jwtParameters
        ) : SessionControllerBase(
            userManager: userManager,
            signInManager: signInManager,
            logger: logger
        )
    {
        private readonly JwtParams jwtParameters = jwtParameters.Value;

        [HttpPost]
        [Route("claims")]
        public async Task<IActionResult> GetClaimsAsync ()
        {
            logger!.LogInformation("{token}", HttpContext.Request.Headers.Authorization.FirstOrDefault());
            var username = User.Identity!.Name;
            if (username == null)
                return Forbid();

            var user = await userManager!.FindByNameAsync(username);

            if (user == null)
            {
                RemoveSessionCookie(HttpContext);
                return Unauthorized();
            }

            return Ok(await userManager.GetClaimsAsync(user));
        }

        [HttpPost]
        [Route("roles")]
        public async Task<IActionResult> GetRolesAsync ()
        {
            var user = await userManager!.FindByNameAsync(User.Identity!.Name!);

            if (user == null)
            {
                RemoveSessionCookie(HttpContext);
                return Unauthorized();
            }

            return Ok(await userManager.GetRolesAsync(user));
        }

        [HttpPost]
        [Route("profile")]
        public async Task<IActionResult> GetProfileAsync ()
        {
            var user = await userManager!.FindByNameAsync(User.Identity!.Name!);

            if (user == null)
            {
                RemoveSessionCookie(HttpContext);
                return Unauthorized();
            }

            return Ok(new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                user.PhoneNumber,
                user.Birthday,
                user.Gender,
                DateRegistered = DateOnly.FromDateTime(user.DateRegistered),
                user.Photo
            });
        }

        [HttpPost]
        [Route("personal-info/edit")]
        public async Task<IActionResult> UpdateUserPersonalInfoAsync ([FromBody] UpdateUserPersonalInfoModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager!.FindByNameAsync(User.Identity!.Name!);

            if (user == null)
            {
                RemoveSessionCookie(HttpContext);
                return Unauthorized();
            }

            try
            {
                DateTime.TryParse(input.Birthday, out var birthday);

                user.FirstName = input.FirstName;
                user.LastName = input.LastName;
                user.Birthday = DateOnly.FromDateTime(birthday);
                user.Gender = input.Gender;

                await userManager.UpdateAsync(user);

                return Ok(TransactionResult<object>.Success(GenerateUserSessionDto(user)));
            }
            catch (Exception ex)
            {
                logger!.LogError("Error occured with exception {ex} while Parsing Birthday value={birthday}", ex.InnerException?.GetType().Name, input.Birthday);
                logger!.LogError("{stackTrace}", ex.StackTrace);

                ModelState.AddModelError(nameof(input.Birthday), "Birthday is invalid.");
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout ()
        {
            RemoveSessionCookie(HttpContext);
            return Ok();
        }

        [HttpPost]
        [Route("send-verification-code")]
        public async Task<IActionResult> SendVerificationCodeAsync ([FromBody] SendVerificationCodeModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (input.Email!.Equals(User.Identity!.Name!, StringComparison.CurrentCultureIgnoreCase))
                return Ok(TransactionResult.Failure(
                    TransactionError.Create(nameof(input.Email), "You're already using this email.")));

            var user = await userManager!.FindByEmailAsync(input.Email);

            if (user != null)
                return Ok(TransactionResult.Failure(
                    TransactionError.Create(nameof(input.Email), "Email is associated with an existing account.")));

            (string Code, string CodeHashString) = userManager!.GenerateVerificationCode(input.Email!, EmailVerificationTypes.EmailChange);

            logger!.LogInformation("Email Change Verification code ({code}) created for {email}", Code, input.Email);

            return Ok(TransactionResult<object>.Success(new { CodeHashString }));
        }

        [HttpPost]
        [Route("change-email")]
        public async Task<IActionResult> ChangeEmailAsync ([FromBody] ChangeEmailModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (User.Identity!.Name!.Equals(model.Email!, StringComparison.OrdinalIgnoreCase))
                return Ok(TransactionResult.Failure(
                    TransactionError.Create(nameof(model.Email), "You're already using this email.")));

            var user = await userManager!.FindByEmailAsync(User.Identity!.Name!);

            if (user == null)
            {
                RemoveSessionCookie(HttpContext);
                return Unauthorized();
            }

            try
            {
                var verified = userManager.VerifyCode(model.Email!, EmailVerificationTypes.EmailChange, model.Code!, model.CodeHashString!);

                if (!verified)
                    return Ok(TransactionResult.Failure(
                        TransactionError.Create(nameof(model.Code), "Invalid code.")));

                user.Email = model.Email!.ToLower();
                user.EmailConfirmed = true;
                user.UserName = model.Email!.ToLower();
                user.NormalizedEmail = userManager.NormalizeEmail(user.UserName);
                user.NormalizedUserName = userManager.NormalizeName(user.UserName);
                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    await userManager.SetUserNameAsync(user, model.Email);
                    await userManager.UpdateNormalizedUserNameAsync(user);
                    var claims = await userManager.GetClaimsAsync(user);

                    await userManager.ReplaceClaimAsync(user,
                        claims.Where(p => p.Type == ClaimTypes.Name).First(),
                        new Claim(ClaimTypes.Name, user.Email));

                    var newClaims = await userManager.GetClaimsAsync(user);

                    (string Token, DateTime ExpiryDateTime) = signInManager!.GenerateJsonWebToken(newClaims.Where(p => p.Type == ClaimTypes.Name).First());
                    RemoveSessionCookie(HttpContext);
                    SetAuthenticationCookie(HttpContext, Token, ExpiryDateTime);
                    return Ok(TransactionResult.Success);
                }

                return Ok(TransactionResult.Failure(
                    TransactionError.Create(nameof(model.Email),
                    result.Errors.FirstOrDefault()?.Description ?? "Unable to change email. Please try again.")
                ));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Email", "Server error. Please try again.");
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePasswordAsync ([FromBody] ChangePasswordModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager!.FindByEmailAsync(User.Identity!.Name!);

            if (user == null)
            {
                RemoveSessionCookie(HttpContext);
                return Unauthorized();
            }

            if (!await userManager.CheckPasswordAsync(user, input.OldPassword!))
                return Ok(TransactionResult.Failure(
                    TransactionError.Create(nameof(input.OldPassword), "Incorrect password.")));

            if (input.Password == input.OldPassword)
                return Ok(TransactionResult.Failure(
                    TransactionError.Create(nameof(input.Password), "Use a different password.")));

            var result = await userManager.ChangePasswordAsync(user, input.OldPassword!, input.Password!);

            TransactionResult transactionResult = TransactionResult.Success;

            if (!result.Succeeded)
            {
                TransactionError? resultError = TransactionError.Create(nameof(input.Password), "Password doesn't meet requirements.");

                foreach (var error in result.Errors)
                {
                    if (error.Code == nameof(userManager.ErrorDescriber.PasswordMismatch))
                    {
                        resultError = TransactionError.Create(nameof(input.OldPassword), "Incorrect password.");
                        break;
                    }
                }

                transactionResult = TransactionResult.Failure(resultError);
            }

            return Ok(transactionResult);
        }

        private object GenerateUserSessionDto (User user)
        => new
        {
            user.FirstName,
            user.LastName,
            user.Email,
            user.Birthday,
            user.Gender,
            DateRegistered = DateOnly.FromDateTime(user.DateRegistered),
        };

        private void SetAuthenticationCookie (HttpContext context, string token, DateTimeOffset expires)
        {
            context.Response.Cookies.Append(JwtTokenKeys.AccessToken, token, new CookieOptions
            {
                HttpOnly = true,
                Expires = expires,
                IsEssential = true,
                SameSite = SameSiteMode.Strict,
                Domain = "localhost",
                Secure = true, // Change to True in Production
                MaxAge = TimeSpan.FromMinutes(jwtParameters.ExpiryTimeInMinutes)
            });
        }

        private void RemoveSessionCookie (HttpContext context)
            => HttpContext.Response.Cookies.Delete(JwtTokenKeys.AccessToken);
    }
}