using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibrebooksRazorPages.Data
{
	public class ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
	{
	}
}
