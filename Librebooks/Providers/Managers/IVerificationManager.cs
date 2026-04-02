using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.GeneralSpace;

namespace Librebooks.Providers.Managers
{
	public interface IVerificationManager
	{
		Task<(VerificationRequest? Request, string? Code)> AddAsync (VerificationRequest request);
		Task<Result<VerificationRequest>> VerifyAsync (string subject, string reason, string code);
	}
}
