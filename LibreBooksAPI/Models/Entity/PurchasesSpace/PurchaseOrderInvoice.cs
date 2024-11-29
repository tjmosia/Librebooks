using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Models.Entity.PurchasesSpace
{
    public class PurchaseOrderInvoice
    {
        public virtual string? OrderId { get; set; }
        public virtual PurchaseOrder? Order { get; set; }
        public virtual string? InvoiceId { get; set; }
        public virtual PurchaseInvoice? Invoice { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<PurchaseOrderInvoice>(options =>
            {
                options.ToTable(nameof(PurchaseOrderInvoice))
                    .HasKey(p => new { p.OrderId, p.InvoiceId })
                    .IsClustered();
            });
    }
}