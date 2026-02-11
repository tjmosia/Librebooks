using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.PurchasesSpace
{
    [Table(nameof(PurchaseOrderInvoice))]
    public class PurchaseOrderInvoice
    {
        public virtual int OrderId { get; set; }
        public virtual int InvoiceId { get; set; }

        public virtual PurchaseOrder? Order { get; set; }
        public virtual PurchaseInvoice? Invoice { get; set; }

        public static void OnModelCreating (ModelBuilder builder)
        {
            builder.Entity<PurchaseOrderInvoice>(options =>
            {
                options.HasKey(p => new { p.OrderId, p.InvoiceId })
                    .IsClustered();
            });
        }
    }
}