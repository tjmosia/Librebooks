using Microsoft.EntityFrameworkCore;

using Librebooks.Core.Types;

namespace Librebooks.Models.Entity.SalesSpace
{
    public class SalesDocumentLine
    {
        public virtual string? DocumentId { get; set; }
        public virtual string? LineId { get; set; }
        public virtual decimal Quantity { get; set; }

        public virtual SalesLine? Line { get; set; }
        public virtual SalesDocument? Document { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<SalesDocumentLine>(options =>
            {
                options.ToTable(nameof(SalesDocumentLine))
                    .HasKey(e => new { e.DocumentId, e.LineId })
                    .IsClustered(true);

                options.Property(p => p.Quantity)
                    .HasColumnType(ColumnTypes.NUMBER);
            });
    }
}
