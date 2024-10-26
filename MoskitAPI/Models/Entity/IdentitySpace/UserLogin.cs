using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Moskit.Models.Entity.IdentitySpace
{
    public class UserLogin : IdentityUserLogin<string>
    {
        public virtual User? User { get; set; }

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<UserLogin>(options =>
            {
                options.ToTable(nameof(UserLogin));
            });
        }
    }
}
