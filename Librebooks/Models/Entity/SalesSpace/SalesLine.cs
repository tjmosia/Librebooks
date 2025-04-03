using System.ComponentModel.DataAnnotations;

using Librebooks.Core.Types;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.InventorySpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SalesSpace
{
    public class SalesLine
    {
        public virtual string? Id { get; set; }
        public virtual bool IsItemType { get; set; }
        public virtual string? ItemCode { get; set; }
        public virtual string? AccountId { get; set; }
        public virtual string? Description { get; set; }
        public virtual string? Unit { get; set; }
        public virtual decimal Price { get; set; }
        public virtual decimal DiscountRate { get; set; }
        public virtual decimal TaxRate { get; set; }
        public virtual string? TaxTypeId { get; set; }
        public virtual string? Comment { get; set; }
        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public virtual ICollection<SalesDocumentLine>? DocumentLines { get; set; }
        public virtual Item? Item { get; set; }
        public virtual CompanyTaxType? TaxType { get; set; }

        public SalesLine ()
        {
            Id = Guid.NewGuid().ToString("N").ToUpper();
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<SalesLine>(options =>
            {
                options.ToTable(nameof(SalesLine))
                    .HasKey(e => e.Id)
                    .IsClustered();

                options.Property(p => p.Price)
                    .HasColumnType(ColumnTypes.Monetary);

                options.Property(p => p.TaxRate)
                    .HasColumnType(ColumnTypes.Percentage);

                options.Property(p => p.DiscountRate)
                    .HasColumnType(ColumnTypes.Percentage);

                options.HasMany(p => p.DocumentLines)
                    .WithOne(p => p.Line)
                    .HasForeignKey(p => p.LineId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasOne(p => p.Item)
                    .WithOne()
                    .HasForeignKey<SalesLine>(p => p.ItemCode)
                    .HasPrincipalKey<Item>(p => p.Code)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.NoAction);
            });
    }
}
