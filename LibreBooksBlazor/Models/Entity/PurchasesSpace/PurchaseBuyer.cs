using Microsoft.EntityFrameworkCore;

using OskitBlazor.Models.Entity.CompanySpace;
using OskitBlazor.Models.Entity.GeneralSpace;

namespace OskitBlazor.Models.Entity.PurchasesSpace
{
    public class PurchaseBuyer
    {
        public virtual string? Id { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? ContactId { get; set; }
        public virtual string? CompanyUserId { get; set; }

        public virtual Company? Company { get; set; }
        public virtual Contact? Contact { get; set; }
        public virtual CompanyUser? CompanyUser { get; set; }

        public PurchaseBuyer ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<PurchaseBuyer>(options =>
            {
                options.ToTable(nameof(PurchaseBuyer))
                    .HasKey(p => p.Id)
                    .IsClustered(false);

                options.HasIndex(p => new { p.CompanyId, p.Id })
                    .IsClustered();

                options.HasOne(p => p.Contact)
                    .WithOne()
                    .HasForeignKey<PurchaseBuyer>(p => p.ContactId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasOne(p => p.CompanyUser)
                    .WithOne()
                    .HasForeignKey<PurchaseBuyer>(p => p.CompanyUserId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}