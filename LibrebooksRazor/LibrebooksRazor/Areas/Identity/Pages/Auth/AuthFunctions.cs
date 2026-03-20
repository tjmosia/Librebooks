using LibrebooksRazor.Models.Entity.GeneralSpace;
using LibrebooksRazor.Providers.Managers;

namespace LibrebooksRazor.Areas.Identity.Pages.Auth;

internal class AuthFunctions
{
	public static async Task<(VerificationRequest? request, string? code)> SendVerificationEmailAsync (IVerificationManager verificationManager, string email, string reason)
	{
		var request = new VerificationRequest(email, reason);
		var token = await verificationManager.AddAsync(request);
		return token;
	}
}
