using Microsoft.EntityFrameworkCore;

using OskitAPI.Core.Types;

namespace OskitAPI.Models.Entity.PurchasesSpace
{
    public class PurchaseDocumentLine
    {
        public virtual string? DocumentId { get; set; }
        public virtual string? LineId { get; set; }
        public virtual decimal Quantity { get; set; }

        public virtual PurchaseLine? Line { get; set; }
        public virtual PurchaseDocument? Document { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<PurchaseDocumentLine>(options =>
            {
                options.ToTable(nameof(PurchaseDocumentLine))
                    .HasKey(e => new { e.DocumentId, e.LineId })
                    .IsClustered(true);

                options.Property(p => p.Quantity)
                    .HasColumnType(ColumnTypes.Number);
            });
    }
}
