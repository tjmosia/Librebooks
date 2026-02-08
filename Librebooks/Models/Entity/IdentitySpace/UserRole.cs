using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.IdentitySpace
{
    public class UserRole : IdentityUserRole<int>
    {
        public virtual Role? Role { get; set; }
        public virtual User? User { get; set; }

        public static void OnModelCreating (ModelBuilder builder)
        {
            builder.Entity<UserRole>(options =>
            {
                options.ToTable(nameof(UserRole));
            });
        }
    }
}
