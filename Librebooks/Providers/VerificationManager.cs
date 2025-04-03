
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
            request.Subject = request.Subject!.ToLower();
            request.Subject = request.RequestUrl!.ToLower();

            var code = GenerateAsync(request.Subject, request.RequestUrl, GenerateRandomCode());
            request.HashString = code.HashString;

            return await store.CreateAsync(request);
        }

        public async Task<VerificationRequest?> UpdateAsync (VerificationRequest request)
            => await store.UpdateAsync(request);

        public async Task<bool> DeleteAsync (VerificationRequest request)
            => await store.DeleteAsync(request);

        public async Task<VerificationRequest?> VerifyAsync (string subject, string requestUri, string code)
        {
            var request = await GetAsync(subject, requestUri);

            if (request == null)
                return null;

            request.Attempts += 1;
            request.RefreshConcurrencyToken();

            var newCode = GenerateAsync(subject, requestUri, code);

            if (request.HashString!.Equals(newCode.HashString))
            {
                request.Confirm();
                request = await store.UpdateAsync(request);
            }

            return request;
        }

        private (string Code, string HashString) GenerateAsync (string subject, string requestUri, string code)
        {
            return (code, BCrypt.Net.BCrypt.HashPassword(string.Concat(subject, requestUri, code)));
        }

        private string GenerateRandomCode ()
            => new Random().Next(100000, 999999).ToString("000000");
    }
}
