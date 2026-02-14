using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.GeneralSpace;

namespace Librebooks.Providers
{
    public interface IVerificationManager
    {
        Task<VerificationRequest?> GetAsync (string subject, string reason);
        Task<VerificationRequest?> AddAsync (VerificationRequest request);
        Task<VerificationRequest?> UpdateAsync (VerificationRequest request);
        Task<bool> DeleteAsync (VerificationRequest request);
        Task<TransactionResult<VerificationRequest>> VerifyAsync (string subject, string reason, string code);
    }
}
