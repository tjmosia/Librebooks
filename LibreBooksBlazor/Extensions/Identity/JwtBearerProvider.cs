using System.IdentityModel.Tokens.Jwt;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using OskitBlazor.Models.Entity.IdentitySpace;

namespace OskitBlazor.Extensions.Identity
{
    public class JwtBearerProvider
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger<JwtBearerProvider> Logger;
        private readonly UserManagerExt UserManager;

        public JwtBearerProvider (IConfiguration config, ILogger<JwtBearerProvider> logger, UserManagerExt userManager)
        {
            Configuration = config;
            Logger = logger;
            UserManager = userManager;
        }

        public string CreateToken ()
        {
            return string.Empty;
        }

        public static void ConfigureJwtBearerAuthenticationService (
            IServiceCollection service,
            TokenValidationParameters tokenValidationParameters)
        {
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });

        }

        private async Task<string> GenerateJSONWebTokenAsync (User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtTokenValidationParameters:SecretKey"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = await UserManager.GetClaimsAsync(user);

            var token = new JwtSecurityToken(Configuration["JwtTokenValidationParameters:Issuer"],
              Configuration["JwtTokenValidationParameters:Issuer"],
              userClaims,
              expires: DateTime.Now.AddHours(7),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
