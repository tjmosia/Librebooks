using LibreBooks.Models.Entity.AccountingSpace;
using LibreBooks.Models.Entity.InventorySpace;
using LibreBooks.Models.Entity.PurchasesSpace;
using LibreBooks.Models.Entity.SalesSpace;
using LibreBooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Models.Entity.CompanySpace
{
    public class CompanyTaxType
    {
        public virtual string? Id { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? TaxTypeId { get; set; }

        public virtual Company? Company { get; set; }
        public virtual TaxTypes? TaxType { get; set; }
        public virtual CompanySalesTaxType? CompanySalesTaxType { get; set; }

        public CompanyTaxType ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<CompanyTaxType>(options =>
            {
                options.ToTable(nameof(CompanyTaxType))
                    .HasKey(x => x.Id)
                    .IsClustered(false);

                options.HasIndex(p => new { p.CompanyId, p.TaxTypeId })
                    .IsUnique()
                    .IsClustered();

                options.HasOne(p => p.TaxType)
                    .WithOne()
                    .HasForeignKey<CompanyTaxType>(p => p.TaxTypeId)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany<Journal>()
                    .WithOne(p => p.TaxType)
                    .HasForeignKey(p => p.TaxTypeId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

                options.HasMany<Account>()
                    .WithOne(p => p.TaxType)
                    .HasForeignKey(p => p.TaxTypeId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

                options.HasMany<SalesLine>()
                    .WithOne(p => p.TaxType)
                    .HasForeignKey(p => p.TaxTypeId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

                options.HasMany<PurchaseLine>()
                    .WithOne(p => p.TaxType)
                    .HasForeignKey(p => p.TaxTypeId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

                options.HasMany<Item>()
                    .WithOne(p => p.TaxType)
                    .HasForeignKey(p => p.TaxTypeId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

                options.HasOne(p => p.CompanySalesTaxType)
                .WithOne(p => p.CompanyTaxType)
                .HasForeignKey<CompanySalesTaxType>(p => p.CompanyTaxTypeId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            });
    }
}
