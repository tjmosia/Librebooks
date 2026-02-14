
using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.GeneralSpace;

namespace Librebooks.Providers
{
    public class VerificationManager (VerificationStore store, ILogger<VerificationManager> logger) : IVerificationManager
    {
        private readonly VerificationStore store = store;
        private readonly ILogger<VerificationManager> logger = logger;

        public async Task<VerificationRequest?> GetAsync (string subject, string requestUri)
            => await store.FindAsync(subject, requestUri);

        public async Task<VerificationRequest?> AddAsync (VerificationRequest request)
        {
            request.Email = request.Email!.ToLower();
            request.Reason = request.Reason!.ToUpper();

            var code = GenerateAsync(request.Email, request.Reason, GenerateRandomCode());

            request.HashString = code.HashString;

            return await store.CreateAsync(request);
        }

        public async Task<VerificationRequest?> UpdateAsync (VerificationRequest request)
            => await store.UpdateAsync(request);

        public async Task<bool> DeleteAsync (VerificationRequest request)
            => await store.DeleteAsync(request);

        public async Task<TransactionResult<VerificationRequest>> VerifyAsync (string email, string reason, string code)
        {
            var request = await GetAsync(email, reason);

            if (request == null)
                return TransactionResult<VerificationRequest>.Failure(TransactionError.Create("Email", "Invalid verification request."));

            request.Attempts += 1;
            request.RefreshConcurrencyToken();

            var confirmed = BCrypt.Net.BCrypt.Verify(string.Concat(email, reason, code), request.HashString);

            if (!confirmed)
            {
                request = await store.UpdateAsync(request);
                return TransactionResult<VerificationRequest>.Failure(TransactionError.Create("Code", "Code is invalid."));
            }

            return TransactionResult<VerificationRequest>.Success(request);
        }

        private static (string Code, string HashString) GenerateAsync (string email, string reason, string code)
        {
            return (code, BCrypt.Net.BCrypt.HashPassword(string.Concat(email, reason, code)));
        }

        private static string GenerateRandomCode ()
            => new Random().Next(100000, 999999).ToString("000000");
    }
}
