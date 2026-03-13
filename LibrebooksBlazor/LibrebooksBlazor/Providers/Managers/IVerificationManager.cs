using LibrebooksBlazor.CoreLib.Operations;
using LibrebooksBlazor.Models.Entity.GeneralSpace;

namespace LibrebooksBlazor.Providers.Managers
{
	public interface IVerificationManager
	{
		Task<(VerificationRequest? Request, string? Code)> AddAsync (VerificationRequest request);
		Task<TransactionResult<VerificationRequest>> VerifyAsync (string subject, string reason, string code);
	}
}
