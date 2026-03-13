using System.Security.Claims;
using LibrebooksRazor.Data;
using LibrebooksRazor.Models.Entity.IdentitySpace;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LibrebooksRazor.Extensions.Identity;

public class UserManagerExtension (IUserStore<User> store,
	IOptions<IdentityOptions> optionsAccessor,
	IPasswordHasher<User> passwordHasher,
	IEnumerable<IUserValidator<User>> userValidators,
	IEnumerable<IPasswordValidator<User>> passwordValidators,
	ILookupNormalizer keyNormalizer,
	IdentityErrorDescriberExtension errorDescriber,
	IServiceProvider services,
	ILogger<UserManagerExtension> logger,
	AppDbContext context) : UserManager<User>(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errorDescriber, services, logger)
{
	public readonly new ILogger<UserManagerExtension> Logger = logger;
	public readonly AppDbContext Context = context;
	public readonly new IdentityErrorDescriberExtension ErrorDescriber = errorDescriber;

	public async Task<bool> IsInRoleAsync (User user, string roleName, string? associatedValue)
	{
		var role = await Context.Roles.Where(p => p.NormalizedName == NormalizeName(roleName))
			.Include(p => p.Users!.Where(u => u.UserId == user.Id))
			.FirstOrDefaultAsync();

		if (role == null || role.Users!.Count == 0)
			return false;

		if (associatedValue != null && role.Users.First().AssociatedTo != associatedValue)
			return false;
		return true;

	}

	public async Task<bool> HasClaimASync (User user, Claim claim)
	{
		var claims = await GetClaimsAsync(user);

		if (claims.Any(c => NormalizeName(c.Type) == NormalizeName(claim.Type) && c.Value == claim.Value))
			return true;

		return false;
	}

	public async Task<IList<UserRole>> GetUserRolesAsync (User user)
	{
		return await Context.UserRoles.Where(p => p.UserId == user.Id)
				.Include(p => p.Role)
				.ToListAsync();
	}

	public async Task<IdentityResult> AddToRoleAsync (User user, string roleName, string associatedValue)
	{
		var role = await Context.Roles.Where(p => p.NormalizedName == NormalizeName(roleName))
				.FirstOrDefaultAsync();

		if (role == null)
			return IdentityResult.Failed(ErrorDescriber.InvalidRole("Role is invalid."));

		var existingUserRole = await Context.UserRoles.Where(p => p.UserId == user.Id && p.RoleId == role.Id)
			.FirstOrDefaultAsync();

		try
		{
			if (existingUserRole != null)
			{
				existingUserRole.AssociatedTo = associatedValue;
				Context.UserRoles.Update(existingUserRole);
			}
			else
			{
				var userRole = new UserRole
				{
					UserId = user.Id,
					RoleId = role.Id,
					AssociatedTo = associatedValue
				};

				await Context.UserRoles.AddAsync(userRole);
			}

			await Context.SaveChangesAsync();
			return IdentityResult.Success;
		}
		catch (Exception)
		{
			return IdentityResult.Failed();
		}
	}
}
