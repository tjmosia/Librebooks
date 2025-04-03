using BCrypt.Net;

using Librebooks.Models.Entity.IdentitySpace;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Librebooks.Areas.Identity.Services
{
    public class UserManagerExtension : UserManager<User>
    {
        private readonly IConfiguration Configuration;
        public readonly new IdentityErrorDescriberExtension ErrorDescriber;
        public readonly new ILogger<UserManagerExtension> Logger;

        public UserManagerExtension (IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriberExtension errors,
            IServiceProvider services,
            ILogger<UserManagerExtension> logger,
            IConfiguration configuration)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            Configuration = configuration;
            ErrorDescriber = errors;
            Logger = logger;
        }

        /// <summary>
        /// Generate a 8 characters  Verificate Code for the given user email. 
        /// </summary>
        /// <param name="userEmail">User's email to be verified.</param>
        /// <returns>A tuple with token generated and it's hash string</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="SaltParseException"></exception>
        public (string Code, string CodeHashString) GenerateVerificationCode (string userEmail, string reason)
        {
            ArgumentNullException.ThrowIfNull(userEmail, nameof(userEmail));

            var Code = Guid.NewGuid().ToString("N").Substring(0, 8);

            Logger.LogInformation("Verification Token {code} created for {email}", Code, userEmail);

            var salt = BCrypt.Net.BCrypt.GenerateSalt();

            try
            {
                var hash = BCrypt.Net.BCrypt.HashPassword(userEmail + reason + Code + GenerateVerificationCodeTimeStamp(), salt);
                Logger.LogInformation("Verification Code {hash} created for {email}", hash, userEmail);
                return (Code, hash);
            }
            catch (SaltParseException ex)
            {
                Logger.LogInformation("Error with Exception {exception} at {method}", ex.GetType().Name, nameof(GenerateVerificationCode));
                throw;
            }
        }

        /// <summary>
        /// Verifies the token provided for the user email.
        /// </summary>
        /// <param name="userEmail">User's email to be verified.</param>
        /// <param name="code">Code to be verified.</param>
        /// <param name="codeHashString">HashString for the code to be verified against.</param>
        /// <returns>true if verified. Otherwise, false.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public bool VerifyCode (string userEmail, string reason, string code, string codeHashString)
        {
            ArgumentNullException.ThrowIfNull(userEmail, nameof(userEmail));
            ArgumentNullException.ThrowIfNull(code, nameof(code));
            ArgumentNullException.ThrowIfNull(codeHashString, nameof(codeHashString));
            ArgumentNullException.ThrowIfNull(code, nameof(code));

            try
            {
                return BCrypt.Net.BCrypt.Verify(userEmail + reason + code + GenerateVerificationCodeTimeStamp(), codeHashString);
            }
            catch (SaltParseException ex)
            {
                Logger.LogInformation("Error with Exception {exception} at {method}", ex.GetType().Name, nameof(VerifyCode));
                Logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<IdentityResult> GenerateRefreshTokenAsync (User user)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var token = BCrypt.Net.BCrypt.HashPassword(user.Id, salt);
            user.RefreshSecurityStamp = salt;
            user.RefreshLoginHash = token;
            return await UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteRefreshTokenAsync (User user)
        {
            user.RefreshSecurityStamp = null;
            user.RefreshLoginHash = null;
            return await UpdateAsync(user);
        }

        private string GenerateVerificationCodeTimeStamp ()
            => DateTime.Now.ToString("yyyy/MM/dd").Replace("/", "");

    }
}
