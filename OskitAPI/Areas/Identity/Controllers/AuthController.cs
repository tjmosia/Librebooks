using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using OskitAPI.Areas.Identity.Models;
using OskitAPI.Core.Identity;
using OskitAPI.CoreLib.Operations;
using OskitAPI.Extensions.Identity;
using OskitAPI.Extensions.Mvc;
using OskitAPI.Models.Entity.IdentitySpace;

using static OskitAPI.Areas.Identity.Models.AuthReqModels;
using static OskitAPI.Areas.Identity.Models.AuthRespDTOs;

namespace OskitAPI.Areas.Identity.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController (UserManagerExt userManager,
        SignInManagerExt signInManager,
        ILogger<SessionControllerBase> logger,
        IOptions<JwtTokenValidationParameters> jwtParameters)
        : SessionControllerBase(userManager, signInManager, logger)
    {
        private readonly JwtTokenValidationParameters jwtParameters = jwtParameters.Value;

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
            {
                return Ok(new FindUserDto
                {
                    Username = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Photo = user.Photo
                });
            }
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
                if (input.Reason == EmailVerificationTypes.PasswordReset)
                {
                    var user = await userManager!.FindByEmailAsync(input.Email!);

                    if (user == null)
                        return NoContent();

                    var (Code, CodeHashString) = userManager!.GenerateVerificationCode(input.Email!, input.Reason);

                    logger!.LogInformation("Verification Code {code} created for email {email}", Code, input.Email);

                    return Ok(TransactionResult<SendVerificationDto>.Success(new SendVerificationDto(CodeHashString)));
                }

                if (input.Reason == EmailVerificationTypes.Registration)
                {
                    var (Code, CodeHashString) = userManager!.GenerateVerificationCode(input.Email!, input.Reason);

                    logger!.LogInformation("Verification Code {code} created for email {email}", Code, input.Email);

                    /*******************************************************************************************************************
                     * SEND USER THE VERIFICATION CODE ON EMAIL HERE!!!
                     *******************************************************************************************************************/

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

            var result = await signInManager!.PasswordSignInAsync(user, input.Password!, false, false);

            if (result.Succeeded)
            {
                var (Token, ExpiryDate) = await signInManager.GenerateJsonWebTokenAsync(user, jwtParameters);

                return Ok(TransactionResult<LoginDto>.Success(GenerateNewUserLoginDto(user, Token)));
            }
            else
            {
                return Ok(TransactionResult<LoginDto>
                    .Failure(TransactionError.Create(nameof(input.Password),
                        IdentityErrorDescriptions.PasswordMismatch)));
            }
        }

        private LoginDto GenerateNewUserLoginDto (User user, string token, string? refreshToken = null)
        => new LoginDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            AccessToken = token,
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

            var verified = userManager.VerifyCode(input.Email!, input.Code!, input.CodeHashString!, EmailVerificationTypes.Registration);

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

                await userManager.AddClaimsAsync(user!, [
                    new Claim (type: ClaimTypes.GivenName, input.FirstName!, ClaimValueTypes.String),
                    new Claim (type: ClaimTypes.Surname, input.LastName!, ClaimValueTypes.String),
                    new Claim (type: ClaimTypes.Name, user.Email, ClaimValueTypes.String)
                ]);

                user = await userManager.FindByNameAsync(user.Email) ?? user;

                var (Token, ExpiryDateTime) = await signInManager!.GenerateJsonWebTokenAsync(user!, jwtParameters);
                var claims = await userManager.GetClaimsAsync(user!);

                user.LoginHash = BCrypt.Net.BCrypt.HashPassword(Token + user.Id);

                await userManager.GenerateRefreshTokenAsync(user);

                return Ok(TransactionResult<LoginDto>
                    .Success(GenerateNewUserLoginDto(user!, Token)));
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

            var verified = userManager.VerifyCode(input.Email!, input.Code!, input.CodeHashString!, EmailVerificationTypes.PasswordReset);

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
        public async Task<IActionResult> ConfirmSignInAsync ()
        {
            if (User.Identity!.IsAuthenticated)
            {
                var user = await userManager!.FindByNameAsync(User.Identity.Name!);

                if (user != null)
                {
                    var token = Request.Headers.Authorization.FirstOrDefault()?
                        .Split(" ").LastOrDefault()!;

                    if (token != null)
                        return Ok(TransactionResult<LoginDto>.Success(
                            GenerateNewUserLoginDto(user, token.Split(" ").LastOrDefault()!)
                        ));
                }
            }

            return Unauthorized();
        }
    }
}