using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Librebooks.Core.Types;
using Librebooks.Extensions.Models;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.PurchasesSpace
{
    [Table(nameof(PurchaseInvoiceReceipt))]
    public class PurchaseInvoiceReceipt () : VersionedEntityBase()
    {
        public virtual int ReceiptId { get; set; }
        public virtual int InvoiceId { get; set; }

        [Column(TypeName = ColumnTypes.MONETARY)]
        public virtual decimal Amount { get; set; }

        [MaxLength(255)]
        public virtual string? Comment { get; set; }

        public virtual PurchaseReceipt? Receipt { get; set; }
        public virtual PurchaseInvoice? Invoice { get; set; }

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<PurchaseInvoiceReceipt>(options =>
            {
                options.HasKey(x => new { x.ReceiptId, x.InvoiceId })
                    .IsClustered();
            });
        }
    }
}
