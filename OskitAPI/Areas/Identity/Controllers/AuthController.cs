using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using OskitAPI.Areas.Identity.Models;
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
                    GivenName = user.FirstName,
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

            return userManager!.VerifyCode(input.Email!, input.Code!, input.CodeHashString!)
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
                        return Ok(TransactionResult.Failure(TransactionError
                            .FromIdentityError(userManager
                                .ErrorDescriber.InvalidEmail())));
                }

                if (input.Reason == EmailVerificationTypes.Registration)
                {
                    var (Code, CodeHashString) = userManager!.GenerateVerificationCode(input.Email!);

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
                    .Failure(TransactionError.FromIdentityError(userManager!.ErrorDescriber!.InvalidEmail(input.Email))));

            var result = await signInManager!.PasswordSignInAsync(user, input.Password!, false, false);

            if (result.Succeeded)
            {
                var (Token, ExpiryDate) = await signInManager.GenerateJsonWebTokenAsync(user, jwtParameters);

                return Ok(TransactionResult<LoginDto>.Success(GenerateNewUserLoginDto(user, Token)));
            }
            else
            {
                return Ok(TransactionResult<LoginDto>
                    .Failure(TransactionError.FromIdentityError(userManager!.ErrorDescriber!.PasswordMismatch())));
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
                return Ok(TransactionResult.Failure(TransactionError
                            .FromIdentityError(userManager!.ErrorDescriber!.DuplicateEmail(input.Email!))));

            var verified = userManager.VerifyCode(input.Email!, input.Code!, input.CodeHashString!);

            if (!verified)
                return Ok(TransactionResult.Failure(TransactionError
                    .FromIdentityError(userManager.ErrorDescriber.UnVerifiedEmail())));

            user = new User
            {
                Email = input.Email!.ToLower(),
                UserName = input.Email.ToLower(),
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
                return Ok(TransactionResult.Failure(
                    TransactionError.FromIdentityErrors(
                        createResult.Errors.ToArray())
                    .ToArray()));
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
                return Ok(TransactionResult.Failure(TransactionError
                    .FromIdentityError(userManager.ErrorDescriber.InvalidEmail(input.Email!))));

            var verified = userManager.VerifyCode(input.Email!, input.Code!, input.CodeHashString!);

            if (!verified)
                return Ok(TransactionResult.Failure(TransactionError
                    .FromIdentityError(userManager.ErrorDescriber.UnVerifiedEmail())));

            var resetPasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);
            var resetPasswordResult = await userManager.ResetPasswordAsync(user, resetPasswordToken, input.Password!);

            if (resetPasswordResult.Succeeded)
                return Ok(TransactionResult.Success);

            return Ok(TransactionResult.Failure(TransactionError.FromIdentityErrors(resetPasswordResult.Errors.ToArray()).ToArray()));
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