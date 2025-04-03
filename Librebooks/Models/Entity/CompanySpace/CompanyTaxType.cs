using Librebooks.Models.Entity.AccountingSpace;
using Librebooks.Models.Entity.InventorySpace;
using Librebooks.Models.Entity.PurchasesSpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CompanySpace
{
    public class CompanyTaxType
    {
        public virtual string? Id { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? TaxTypeId { get; set; }

        public virtual Company? Company { get; set; }
        public virtual TaxType? TaxType { get; set; }
        public virtual CompanyDefaultTaxType? CompanySalesTaxType { get; set; }

        public CompanyTaxType ()
            => Id = Guid.NewGuid().ToString("N").ToUpper();

        public CompanyTaxType (string companyId, string taxTypeId) : this()
        {
            CompanyId = companyId;
            TaxTypeId = taxTypeId;
        }

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
                .HasForeignKey<CompanyDefaultTaxType>(p => p.CompanyTaxTypeId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            });
    }
}
