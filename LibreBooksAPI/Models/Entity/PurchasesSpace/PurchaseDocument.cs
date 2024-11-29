using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using LibreBooks.Core.Types;
using LibreBooks.Models.Entity.DocumentSpace;

namespace LibreBooks.Models.Entity.PurchasesSpace
{
    public class PurchaseDocument
    {
        public virtual string? Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual DateTime? DueDate { get; set; }
        public virtual string? Number { get; set; }
        public virtual string? SuppplierReference { get; set; }
        public virtual string? SupplierDetailsId { get; set; }
        public virtual string? Message { get; set; }
        public virtual string? Footer { get; set; }
        public virtual string? Currency { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual bool IsDraft { get; set; }
        public virtual bool Printed { get; set; }
        public virtual string? StatusId { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public void UpdateConcurrencyToken ()
            => RowVersion = Guid.NewGuid().ToString("N");

        public virtual DocumentStatus? Status { get; set; }
        public virtual ICollection<PurchaseDocumentLine>? Lines { get; set; }
        public virtual ICollection<PurchaseDocumentNote>? Notes { get; set; }
        public virtual PurchaseDocumentSupplierDetails? SupplierDetails { get; set; }

        public PurchaseDocument ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<PurchaseDocument>(options =>
            {
                options.ToTable(nameof(PurchaseDocument))
                    .HasKey(e => e.Id)
                    .IsClustered(false);

                options.Property(p => p.Date)
                    .HasColumnType(ColumnTypes.Date);

                options.HasIndex(p => new { p.CompanyId, p.Number })
                    .IsUnique()
                    .IsClustered();

                options.HasMany(p => p.Lines)
                    .WithOne(p => p.Document)
                    .HasForeignKey(p => p.DocumentId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Notes)
                    .WithOne()
                    .HasForeignKey(p => p.DocumentId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                options.Property(p => p.Date)
                    .HasColumnType(ColumnTypes.Date);

                options.Property(p => p.DueDate)
                    .HasColumnType(ColumnTypes.Date);
            });
    }
}
