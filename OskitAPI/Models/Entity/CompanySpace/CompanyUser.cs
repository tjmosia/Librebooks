using Microsoft.EntityFrameworkCore;

using MacbooksAPI.Models.Entity.IdentitySpace;
using MacbooksAPI.Models.Entity.SalesSpace;

namespace MacbooksAPI.Models.Entity.CompanySpace
{
    public class CompanyUser
    {
        public virtual string? Id { get; set; }
        public virtual string? UserId { get; set; }
        public virtual string? CompanyId { get; set; }

        public virtual Company? Company { get; set; }
        public virtual User? User { get; set; }

        public CompanyUser ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<CompanyUser>(options =>
            {
                options.ToTable(nameof(CompanyUser))
                    .HasKey(p => p.Id)
                    .IsClustered(false);

                options.HasIndex(p => new { p.UserId, p.CompanyId })
                    .IsUnique()
                    .IsClustered();

                options.HasOne<SalesPerson>()
                    .WithOne(p => p.CompanyUser)
                    .HasForeignKey<SalesPerson>(p => p.CompanyUserId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
