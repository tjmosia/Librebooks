using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OskitAPI.Models.Entity.IdentitySpace
{
    public class UserToken : IdentityUserToken<string>
    {
        public virtual User? User { get; set; }
        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<UserToken>(options =>
            {
                options.ToTable(nameof(UserToken));
            });
        }
    }
}
