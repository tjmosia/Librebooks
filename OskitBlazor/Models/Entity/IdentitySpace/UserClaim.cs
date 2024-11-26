using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OskitBlazor.Models.Entity.IdentitySpace
{
    public class UserClaim : IdentityUserClaim<string>
    {
        public virtual User? User { get; set; }

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<UserClaim>(options =>
            {
                options.ToTable(nameof(UserClaim));
            });
        }
    }
}
