using System.ComponentModel.DataAnnotations;

using Librebooks.Core.Types;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.DocumentSpace;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SalesSpace
{
    public class SalesDocument
    {
        public virtual string Id { get; set; }
        public virtual string? Title { get; set; }
        public virtual string? Number { get; set; }
        public virtual string? CustomerReference { get; set; }
        public virtual string? CustomerDetailsId { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual DateTime? DueDate { get; set; }
        public virtual string? Message { get; set; }
        public virtual string? FooterMessage { get; set; }
        public virtual bool TaxExempt { get; set; }
        public virtual string? Currency { get; set; }
        public virtual string? SalesPersonId { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? ShippingTermId { get; set; }
        public virtual string? ShippingMethodId { get; set; }
        public virtual string? CustomerId { get; set; }
        public virtual bool Recorded { get; set; }
        public virtual string? StatusId { get; set; }
        public virtual bool Printed { get; set; }
        public virtual string? CreatorId { get; set; }
        public virtual string? LogoId { get; set; }
        [ConcurrencyCheck]
        public virtual string RowVersion { get; set; }

        public virtual CompanyImage? Logo { get; set; }
        public virtual DocumentStatus? Status { get; set; }
        public virtual SalesPerson? SalesPerson { get; set; }
        public virtual ShippingMethod? ShippingMethod { get; set; }
        public virtual ShippingTerm? ShippingTerm { get; set; }
        public virtual SalesDocumentCustomerDetails? CustomerDetails { get; set; }
        public virtual ICollection<SalesDocumentNote>? Notes { get; set; }
        public virtual ICollection<SalesDocumentLine>? Lines { get; set; }
        public virtual User? Creator { get; set; }

        public SalesDocument ()
        {
            Id = Guid.NewGuid().ToString("N").ToUpperInvariant();
            RowVersion = Guid.NewGuid().ToString("N").ToUpperInvariant();
        }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<SalesDocument>(options =>
            {
                options.ToTable(nameof(SalesDocument))
                    .HasKey(e => e.Id)
                    .IsClustered(false);

                options.HasIndex(p => new { p.CompanyId, p.Number })
                    .IsUnique()
                    .IsClustered();

                options.Property(p => p.Date)
                    .HasColumnType(ColumnTypes.Date);

                options.Property(p => p.DueDate)
                    .HasColumnType(ColumnTypes.Date);

                options.HasMany(p => p.Lines)
                    .WithOne(p => p.Document)
                    .HasForeignKey(p => p.DocumentId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasOne(p => p.Creator)
                    .WithOne()
                    .HasForeignKey<SalesDocument>(p => p.CreatorId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

                options.HasMany(p => p.Notes)
                    .WithOne()
                    .HasForeignKey(p => p.DocumentId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });
    }
}
