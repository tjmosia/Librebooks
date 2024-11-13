using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using OskitAPI.Models.Entity.CompanySpace;

namespace OskitAPI.Models.Entity.IdentitySpace
{
    public class User : IdentityUser<string>
    {
        public virtual string? FirstName { get; set; }
        public virtual string? LastName { get; set; }
        public virtual DateOnly? BirthDay { get; set; }
        public virtual string? Gender { get; set; }
        public virtual string? Photo { get; set; }

        public virtual DateTime DateRegistered { get; set; }
        public virtual DateTime DateLastLoggedIn { get; set; }
        public virtual string? LoginHash { get; set; }
        public virtual string? RefreshSecurityStamp { get; set; }
        public virtual string? RefreshLoginHash { get; set; }

        public virtual ICollection<UserRole>? Roles { get; set; }
        public virtual ICollection<UserLogin>? Logins { get; set; }
        public virtual ICollection<UserToken>? Tokens { get; set; }
        public virtual ICollection<UserClaim>? Claims { get; set; }
        public virtual ICollection<CompanyUser>? Companies { get; set; }

        [NotMapped]
        public virtual string? FullName { get => $"{FirstName} + {LastName}"; }

        public User ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<User>(options =>
            {
                options.ToTable(nameof(User));

                options.HasMany(p => p.Roles)
                    .WithOne(p => p.User)
                        .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasMany(p => p.Logins)
                    .WithOne(p => p.User)
                        .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasMany(p => p.Tokens)
                    .WithOne(p => p.User)
                        .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasMany(p => p.Claims)
                    .WithOne(p => p.User)
                        .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasMany(p => p.Companies)
                    .WithOne(p => p.User)
                        .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
