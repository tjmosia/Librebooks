using Librebooks.Models.Entity.GeneralSpace;

namespace Librebooks.Providers
{
    public interface IVerificationManager
    {
        Task<VerificationRequest?> GetAsync (string subject, string requestUri);
        Task<VerificationRequest?> AddAsync (VerificationRequest request);
        Task<VerificationRequest?> UpdateAsync (VerificationRequest request);
        Task<bool> DeleteAsync (VerificationRequest request);
        Task<VerificationRequest?> VerifyAsync (string subject, string requestUri, string code);
    }
}
