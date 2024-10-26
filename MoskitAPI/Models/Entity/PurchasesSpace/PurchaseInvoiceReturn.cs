using Microsoft.EntityFrameworkCore;

namespace Moskit.Models.Entity.PurchasesSpace
{
    public class PurchaseInvoiceReturn
    {
        public virtual string? ReturnId { get; set; }
        public virtual string? InvoiceId { get; set; }
        public virtual string? Comment { get; set; }

        public virtual PurchaseReturn? Return { get; set; }
        public virtual PurchaseInvoice? Invoice { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<PurchaseInvoiceReturn>(options =>
            {
                options.ToTable(nameof(PurchaseInvoiceReturn))
                    .HasKey(x => new { x.InvoiceId, x.ReturnId })
                    .IsClustered();
            });
    }
}
