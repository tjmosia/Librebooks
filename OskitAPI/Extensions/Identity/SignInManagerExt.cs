using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

using OskitAPI.Models.Entity.IdentitySpace;

namespace OskitAPI.Extensions.Identity
{
    public class SignInManagerExt : SignInManager<User>
    {
        public readonly IConfiguration Configuration;
        private readonly JwtParams jwtParams;

        public SignInManagerExt (UserManager<User> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<User> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<User>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<User> confirmation,
            IConfiguration configuration,
            IOptions<JwtParams> jwtParameters)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            Configuration = configuration;
            jwtParams = jwtParameters.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException" />
        public async Task<(string Token, DateTime ExpiryDateTime)> GenerateJsonWebTokenAsync (User user)
        {
            var expiryDate = DateTime.Now.AddHours(jwtParams.ExpiryTimeInMinutes);

            return (
                Token: new JsonWebTokenHandler()
                    .CreateToken(
                        new SecurityTokenDescriptor
                        {
                            Issuer = jwtParams.Issuer,
                            Audience = jwtParams.Audience,
                            Subject = new ClaimsIdentity(await UserManager.GetClaimsAsync(user)),
                            Expires = expiryDate,
                            SigningCredentials = new SigningCredentials(
                                key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtParams.SecretKey!)),
                                //key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is a test SecretKey for Authentication")),
                                algorithm: SecurityAlgorithms.HmacSha256
                            )
                        }),
                ExpiryDateTime: expiryDate
            );
        }
    }
}
