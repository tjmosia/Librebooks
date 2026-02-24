using System.Security.Claims;
using Librebooks.Models.Entity.IdentitySpace;

namespace Librebooks.Areas.Identity.Data;

public readonly struct LoginDto (User user, UserRole[] userRoles, Claim[] claims)
{
	public readonly FindUserDto User = new(user);
	public readonly object[] Roles = [..userRoles.Select(p => new {
		p.Role?.Name,
		p.AssociatedTo
	})];

	public readonly object[] Claims = [..claims.Select(p => new {
		p.Type,
		p.Value
	})];
}
