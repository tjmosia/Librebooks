using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Moskit.Models.Entity.IdentitySpace
{
    public class RoleClaim : IdentityRoleClaim<string>
    {
        public virtual Role? Role { get; set; }

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<RoleClaim>(options =>
            {
                options.ToTable(nameof(RoleClaim));
            });
        }
    }
}
