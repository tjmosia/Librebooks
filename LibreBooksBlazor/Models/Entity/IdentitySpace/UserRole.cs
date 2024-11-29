using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OskitBlazor.Models.Entity.IdentitySpace
{
    public class UserRole : IdentityUserRole<string>
    {
        public virtual Role? Role { get; set; }
        public virtual User? User { get; set; }

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<UserRole>(options =>
            {
                options.ToTable(nameof(UserRole));
            });
        }
    }
}
