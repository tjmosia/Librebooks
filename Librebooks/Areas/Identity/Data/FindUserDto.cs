using Librebooks.Models.Entity.IdentitySpace;

namespace Librebooks.Areas.Identity.Data;

public readonly struct FindUserDto (User user)
{
	public readonly string? Email = user.Email;
	public readonly string? GivenName = user.FirstName;
	public readonly string? Photo = user.GetPhotoAsBase64();
}
