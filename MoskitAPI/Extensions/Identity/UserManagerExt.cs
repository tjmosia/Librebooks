using BCrypt.Net;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using OskitAPI.Models.Entity.IdentitySpace;

namespace OskitAPI.Extensions.Identity
{
    public class UserManagerExt : UserManager<User>
    {
        private readonly IConfiguration Configuration;
        public readonly new IdentityErrorDescriberExt ErrorDescriber;
        public readonly new ILogger<UserManagerExt> Logger;

        public UserManagerExt (IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriberExt errors,
            IServiceProvider services,
            ILogger<UserManagerExt> logger,
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
        public (string Code, string CodeHashString) GenerateVerificationCode (string userEmail)
        {
            ArgumentNullException.ThrowIfNull(userEmail, nameof(userEmail));

            var Code = Guid.NewGuid().ToString("N").Substring(0, 8);

            Logger.LogInformation("Verification Token {code} created for {email}", Code, userEmail);

            var salt = BCrypt.Net.BCrypt.GenerateSalt();

            try
            {
                var hash = $"{BCrypt.Net.BCrypt.HashPassword(Code + userEmail + salt + GenerateVerificationCodeTimeStamp())}$sl{userEmail.Substring(0, 2)}ts${salt}";
                Logger.LogInformation("Verification Code {hash} created for {email}", hash, userEmail);
                return (Code, hash);
            }
            catch (Exception e)
            {
                Logger.LogInformation("Error with Exception {exception} at {method}", e.GetType().Name, nameof(GenerateVerificationCode));
                throw;
            }
        }

        /// <summary>
        /// Verifies the token provided for the user email.
        /// </summary>
        /// <param name="userEmail">User's email to be verified.</param>
        /// <param name="token">Token to be verified.</param>
        /// <param name="tokenHashString">HashString for the token to be verified against.</param>
        /// <returns>true if verified. Otherwise, false.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public bool VerifyCode (string userEmail, string token, string tokenHashString)
        {
            ArgumentNullException.ThrowIfNull(userEmail, nameof(userEmail));
            ArgumentNullException.ThrowIfNull(token, nameof(userEmail));
            ArgumentNullException.ThrowIfNull(tokenHashString, nameof(tokenHashString));

            try
            {
                var split = tokenHashString.Split($"$sl{userEmail.Substring(0, 2)}ts$");
                var isVerified = BCrypt.Net.BCrypt.Verify(token + userEmail + split.LastOrDefault() + GenerateVerificationCodeTimeStamp(), split.FirstOrDefault());
                return isVerified;
            }
            catch (Exception e)
            {
                Logger.LogInformation("Error with Exception {exception} at {method}", e.GetType().Name, nameof(GenerateVerificationCode));
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
