using Microsoft.EntityFrameworkCore;

using Librebooks.Core.Types;

namespace Librebooks.Models.Entity.SalesSpace
{
    public class SalesInvoiceReceipt
    {
        public virtual string? InvoiceId { get; set; }
        public virtual string? ReceiptId { get; set; }
        public virtual decimal Amount { get; set; }

        public virtual SalesInvoice? Invoice { get; set; }
        public virtual SalesReceipt? Receipt { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<SalesInvoiceReceipt>(options =>
            {
                options.ToTable(nameof(SalesInvoiceReceipt))
                    .HasKey(p => new { p.InvoiceId, p.ReceiptId });

                options.Property(p => p.Amount)
                    .HasColumnType(ColumnTypes.MONETARY);
            });
    }
}
