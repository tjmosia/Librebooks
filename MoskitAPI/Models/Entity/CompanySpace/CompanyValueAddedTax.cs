using Microsoft.EntityFrameworkCore;

using Moskit.Models.Entity.AccountingSpace;
using Moskit.Models.Entity.InventorySpace;
using Moskit.Models.Entity.PurchasesSpace;
using Moskit.Models.Entity.SalesSpace;
using Moskit.Models.Entity.SystemSpace;

namespace Moskit.Models.Entity.CompanySpace
{
    public class CompanyValueAddedTax
    {
        public virtual string? Id { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? VATId { get; set; }

        public virtual Company? Company { get; set; }
        public virtual ValueAddedTax? VAT { get; set; }
        public virtual CompanyDefaultValueAddedTax? CompanyDefaultVAT { get; set; }

        public CompanyValueAddedTax ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<CompanyValueAddedTax>(options =>
            {
                options.ToTable(nameof(CompanyValueAddedTax))
                    .HasKey(x => x.Id)
                    .IsClustered(false);

                options.HasIndex(p => new { p.CompanyId, p.VATId })
                    .IsUnique()
                    .IsClustered();

                options.HasOne(p => p.VAT)
                    .WithOne()
                    .HasForeignKey<CompanyValueAddedTax>(p => p.VATId)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany<Journal>()
                    .WithOne(p => p.VAT)
                    .HasForeignKey(p => p.VATId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

                options.HasMany<Account>()
                    .WithOne(p => p.VAT)
                    .HasForeignKey(p => p.VATId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

                options.HasMany<SalesLine>()
                    .WithOne(p => p.VAT)
                    .HasForeignKey(p => p.VATId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

                options.HasMany<PurchaseLine>()
                    .WithOne(p => p.VAT)
                    .HasForeignKey(p => p.VATId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

                options.HasMany<Item>()
                    .WithOne(p => p.VAT)
                    .HasForeignKey(p => p.VATId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);
            });
    }
}
