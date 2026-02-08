using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.IdentitySpace
{
    public class UserToken : IdentityUserToken<int>
    {
        public virtual User? User { get; set; }

        public static void OnModelCreating (ModelBuilder builder)
        {
            builder.Entity<UserToken>(options =>
            {
                options.ToTable(nameof(UserToken));
            });
        }
    }
}
