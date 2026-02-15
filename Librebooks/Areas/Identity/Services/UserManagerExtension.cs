using Librebooks.Models.Entity.IdentitySpace;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Librebooks.Areas.Identity.Services;

public class UserManagerExtension : UserManager<User>
{
	private readonly IConfiguration Configuration;
	public readonly new IdentityErrorDescriberExtension ErrorDescriber;
	public readonly new ILogger<UserManagerExtension> Logger;

	public UserManagerExtension (IUserStore<User> store,
		IOptions<IdentityOptions> optionsAccessor,
		IPasswordHasher<User> passwordHasher,
		IEnumerable<IUserValidator<User>> userValidators,
		IEnumerable<IPasswordValidator<User>> passwordValidators,
		ILookupNormalizer keyNormalizer,
		IdentityErrorDescriberExtension errors,
		IServiceProvider services,
		ILogger<UserManagerExtension> logger,
		IConfiguration configuration)
		: base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
	{
		Configuration = configuration;
		ErrorDescriber = errors;
		Logger = logger;
	}

	public async Task<IdentityResult> GenerateRefreshTokenAsync (User user)
	{
		var salt = BCrypt.Net.BCrypt.GenerateSalt();
		var token = BCrypt.Net.BCrypt.HashPassword(user.UserName, salt);
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

}
