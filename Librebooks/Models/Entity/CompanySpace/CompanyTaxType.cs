using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Models.Entity.AccountingSpace;
using Librebooks.Models.Entity.InventorySpace;
using Librebooks.Models.Entity.PurchasesSpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CompanySpace
{
    [Table(nameof(CompanyTaxType))]
    public class CompanyTaxType ()
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public virtual int CompanyId { get; set; }
        public virtual int TaxTypeId { get; set; }

        public CompanyTaxType (int companyId, int taxTypeId)
            : this()
        {
            CompanyId = companyId;
            TaxTypeId = taxTypeId;
        }

        public virtual TaxType? TaxType { get; set; }
        public virtual CompanyDefaultTaxType? CompanyDefaultTaxType { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<CompanyTaxType>(options =>
            {

                options.HasIndex(p => new { p.CompanyId, p.TaxTypeId })
                    .IsUnique()
                    .IsClustered();

                options.HasOne<Company>()
                    .WithMany()
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

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

                options.HasOne(p => p.CompanyDefaultTaxType)
                    .WithOne(p => p.CompanyTaxType)
                    .HasForeignKey<CompanyDefaultTaxType>(p => p.CompanyTaxTypeId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);
            });
    }
}
