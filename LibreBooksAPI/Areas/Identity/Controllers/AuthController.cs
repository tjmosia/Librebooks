using System.Security.Claims;

using LibreBooks.Areas.Identity.Models;
using LibreBooks.Core.Identity;
using LibreBooks.CoreLib.Operations;
using LibreBooks.Extensions.Identity;
using LibreBooks.Extensions.Mvc;
using LibreBooks.Models.Entity.IdentitySpace;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using static LibreBooks.Areas.Identity.Models.AuthReqModels;
using static LibreBooks.Areas.Identity.Models.AuthRespDTOs;

namespace LibreBooks.Areas.Identity.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController (UserManagerExt userManager,
        SignInManagerExt signInManager,
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
                return Ok(new FindUserDto
                {
                    Username = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Photo = user.Photo
                });
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
        public async Task<IActionResult> SendVerificationCodeAsync ([FromBody] SendVerificationModel input)
        {
            if (ModelState.IsValid)
            {
                if (input.Reason == EmailVerificationTypes.PasswordReset || input.Reason == EmailVerificationTypes.Registration)
                {
                    if (input.Reason == EmailVerificationTypes.PasswordReset)
                    {
                        var user = await userManager!.FindByEmailAsync(input.Email!);

                        if (user == null)
                            return NoContent();
                    }

                    var (Code, CodeHashString) = userManager!.GenerateVerificationCode(input.Email!, input.Reason);

                    logger!.LogInformation("Verification Code {code} created for email {email}", Code, input.Email);

                    return Ok(TransactionResult<SendVerificationDto>.Success(new SendVerificationDto(CodeHashString)));
                }
            }

            return BadRequest(ModelState);
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

            var result = await signInManager!.CheckPasswordSignInAsync(user, input.Password!, false);

            if (result.Succeeded)
            {
                user.DateLastLoggedIn = DateTime.Now;
                await userManager.UpdateAsync(user);

                var nameClaim = (await userManager.GetClaimsAsync(user))
                    .FirstOrDefault(p => p.Type == ClaimTypes.Name);

                var (Token, ExpiryDate) = signInManager.GenerateJsonWebToken(nameClaim!);

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

            var verified = userManager.VerifyCode(input.Email!, EmailVerificationTypes.Registration, input.Code!, input.CodeHashString!);

            if (!verified)
                return Ok(TransactionResult.Failure(TransactionError.Create(nameof(input.Email), IdentityErrorDescriptions.UnVerifiedEmail)));

            DateTime? birthday = null;

            try
            {
                birthday = DateTime.Parse(input.Birthday!);

                if (DateTime.Now.Year - birthday.Value.Year <= 15)
                {
                    ModelState.AddModelError(nameof(input.Birthday), "You need to be atleast 15 years to register.");
                    return BadRequest(ModelState);
                }
            }
            catch (FormatException formatEx)
            {
                logger!.LogError("Error while trying to create birthday with value={date}", input.Birthday!);
                logger!.LogError("{stackTrace}", formatEx.Message);
                ModelState.AddModelError(nameof(input.Birthday), "Invalid date provided.");
                return BadRequest(ModelState);
            }

            user = new User
            {
                Email = input.Email!.ToLower(),
                UserName = input.Email.ToLower(),
                FirstName = input.FirstName,
                LastName = input.LastName,
                Birthday = DateOnly.FromDateTime(birthday!.Value),
                Gender = input.Gender,
                NormalizedEmail = userManager.NormalizeEmail(input.Email),
                NormalizedUserName = userManager.NormalizeName(input.Email),
                DateLastLoggedIn = DateTime.Now,
                DateRegistered = DateTime.Now,
                EmailConfirmed = true
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

                var (Token, ExpiryDate) = signInManager!.GenerateJsonWebToken(nameClaim!);

                SetAuthenticationCookie(HttpContext, Token, ExpiryDate);

                return Ok(TransactionResult<object>
                    .Success(signInManager!.GenerateUserSessionDTO(user)));
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
                    errors.Add(new TransactionError(nameof(RegisterModel.Email), "Unable to register user."));

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

        private LoginDto GenerateUserLoginDto (User user)
        => new LoginDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.UserName,
            Photo = user.Photo
        };
    }
}