using LibrebooksRazor.CoreLib.Operations;
using LibrebooksRazor.Models.Entity.GeneralSpace;

namespace LibrebooksRazor.Providers.Managers
{
	public interface IVerificationManager
	{
		Task<(VerificationRequest? Request, string? Code)> AddAsync (VerificationRequest request);
		Task<TransactionResult<VerificationRequest>> VerifyAsync (string subject, string reason, string code);
	}
}
