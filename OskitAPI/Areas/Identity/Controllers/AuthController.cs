using MacbooksAPI.Areas.Identity.Models;
using MacbooksAPI.Core.Identity;
using MacbooksAPI.CoreLib.Operations;
using MacbooksAPI.Extensions.Identity;
using MacbooksAPI.Extensions.Mvc;
using MacbooksAPI.Models.Entity.IdentitySpace;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using static MacbooksAPI.Areas.Identity.Models.AuthReqModels;
using static MacbooksAPI.Areas.Identity.Models.AuthRespDTOs;

namespace MacbooksAPI.Areas.Identity.Controllers
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
                var (Token, ExpiryDate) = await signInManager.GenerateJsonWebTokenAsync(user);
                SetAuthenticationCookie(HttpContext, Token, ExpiryDate);
                user.DateLastLoggedIn = DateTime.Now;
                await userManager.UpdateAsync(user);
                return Ok(TransactionResult<LoginDto>
                    .Success(GenerateNewUserLoginDto(user!)));
            }
            else
                return Ok(TransactionResult<LoginDto>
                    .Failure(TransactionError.Create(nameof(input.Password),
                        IdentityErrorDescriptions.PasswordMismatch)));
        }

        private LoginDto GenerateNewUserLoginDto (User user)
        => new LoginDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.UserName,
            Photo = user.Photo
        };

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

                if (DateTime.Now.Year - birthday.Value.Year >= 15)
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
                BirthDay = DateOnly.FromDateTime(birthday!.Value),
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
                user = await userManager.FindByEmailAsync(user.Email) ?? user;

                var (Token, ExpiryDate) = await signInManager!.GenerateJsonWebTokenAsync(user!);

                user.DateLastLoggedIn = DateTime.Now;
                await userManager.UpdateAsync(user);
                SetAuthenticationCookie(HttpContext, Token, ExpiryDate);

                return Ok(TransactionResult<LoginDto>
                    .Success(GenerateNewUserLoginDto(user!)));
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

        private void SetAuthenticationCookie (HttpContext context, string token, DateTimeOffset expires)
        {
            context.Response.Cookies.Append(JwtTokenKeys.AccessToken, token, new CookieOptions
            {
                HttpOnly = true,
                Expires = expires,
                IsEssential = true,
                SameSite = SameSiteMode.None,
                Domain = "localhost",
                Secure = true, // Change to True in Production
                MaxAge = TimeSpan.FromMinutes(jwtParameters.ExpiryTimeInMinutes)
            });
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
        [Route("confirm-login")]
        [Authorize]
        public async Task<IActionResult> ConfirmSignInAsync ()
        {
            logger!.LogInformation(Request.Headers.Authorization.FirstOrDefault()?.ToString());
            logger!.LogInformation("Confirm clicked.");

            var user = await userManager!.FindByEmailAsync(User!.Identity!.Name!);



            if (user != null)
                return Ok(TransactionResult<LoginDto>.Success(
                    GenerateNewUserLoginDto(user)
                ));

            return Unauthorized();
        }

        [HttpGet]
        [Route("food")]
        [Authorize]
        public IActionResult Food ()
        {
            return Ok(new string[]
            {
                "Apples", "Oranges"
            });
        }
    }
}