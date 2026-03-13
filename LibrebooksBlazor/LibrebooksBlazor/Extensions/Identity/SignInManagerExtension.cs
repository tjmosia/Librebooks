using LibrebooksBlazor.Models.Entity.IdentitySpace;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace LibrebooksBlazor.Extensions.Identity;

public class SignInManagerExtension : SignInManager<User>
{
	public readonly IConfiguration Configuration;

	public SignInManagerExtension (UserManager<User> userManager,
		IHttpContextAccessor contextAccessor,
		IUserClaimsPrincipalFactory<User> claimsFactory,
		IOptions<IdentityOptions> optionsAccessor,
		ILogger<SignInManager<User>> logger,
		IAuthenticationSchemeProvider schemes,
		IUserConfirmation<User> confirmation,
		IConfiguration configuration)
		: base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
	{
		Configuration = configuration;
	}
}
