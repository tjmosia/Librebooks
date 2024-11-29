using System.IdentityModel.Tokens.Jwt;
using System.Text;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using OskitBlazor.Models.Entity.IdentitySpace;

namespace OskitBlazor.Extensions.Identity
{
    public class SignInManagerExt : SignInManager<User>
    {
        public readonly IConfiguration Configuration;

        public SignInManagerExt (UserManager<User> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<User> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<User>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<User> confirmation,
            IConfiguration configuration)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            Configuration = configuration;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException" />
        public async Task<(string Token, DateTime ExpiryDateTime)> GenerateJsonWebTokenAsync (User user, JwtTokenValidationParameters parameters)
        {
            ArgumentNullException.ThrowIfNull(parameters, nameof(parameters));

            var expiryDate = DateTime.Now.AddMinutes(parameters.ExpiryTimeInMinutes);

            return (
                Token: new JwtSecurityTokenHandler()
                    .WriteToken(
                        token: new JwtSecurityToken
                        (
                            issuer: parameters.Issuer,
                            audience: parameters.Audience,
                            claims: await UserManager.GetClaimsAsync(user),
                            expires: expiryDate,
                            signingCredentials: new SigningCredentials
                            (
                                key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(parameters.SecretKey!)),
                                algorithm: SecurityAlgorithms.HmacSha256
                            )
                        )
                    ),
                ExpiryDateTime: expiryDate
            );
        }
    }
}
