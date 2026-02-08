using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Librebooks.Models.Entity.CompanySpace;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.IdentitySpace
{
    public class User : IdentityUser<int>
    {
        [Required]
        [MaxLength(50)]
        public virtual string? FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual string? LastName { get; set; }

        public virtual DateOnly? Birthday { get; set; }

        [MaxLength(10)]
        public virtual string? Gender { get; set; }

        public virtual byte[]? Photo { get; set; }

        public virtual DateOnly DateRegistered { get; set; }
        public virtual DateOnly DateLastLoggedIn { get; set; }

        [MaxLength(155)]
        public virtual string? LoginHash { get; set; }

        [MaxLength(155)]
        public virtual string? RefreshSecurityStamp { get; set; }

        [MaxLength(155)]
        public virtual string? RefreshLoginHash { get; set; }

        public virtual ICollection<UserRole>? Roles { get; set; }
        public virtual ICollection<UserLogin>? Logins { get; set; }
        public virtual ICollection<UserToken>? Tokens { get; set; }
        public virtual ICollection<UserClaim>? Claims { get; set; }
        public virtual ICollection<CompanyUser>? Companies { get; set; }

        [NotMapped]
        public virtual string? FullName { get => $"{FirstName} + {LastName}"; }

        public string GetPhotoAsBase64 ()
            => Photo == null ? "" : Convert.ToBase64String(Photo!);

        public static void OnModelCreating (ModelBuilder builder)
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
