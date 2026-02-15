using Librebooks.Models.Entity.IdentitySpace;

namespace Librebooks.Areas.Identity.Data;

public readonly struct LoginDto (User user)
{
	public readonly string? Email = user.Email;
	public readonly string? FirstName = user.FirstName;
	public readonly string? LastName = user.LastName;
	public readonly string? Photo = user.GetPhotoAsBase64();
}
