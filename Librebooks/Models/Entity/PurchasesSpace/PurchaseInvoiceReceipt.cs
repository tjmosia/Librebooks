﻿using System.ComponentModel.DataAnnotations;

using Librebooks.Core.Types;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.PurchasesSpace
{
    public class PurchaseInvoiceReceipt
    {
        public virtual string? ReceiptId { get; set; }
        public virtual string? InvoiceId { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual string? Comment { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public virtual PurchaseReceipt? Receipt { get; set; }
        public virtual PurchaseInvoice? Invoice { get; set; }

        public PurchaseInvoiceReceipt ()
            => RowVersion = Guid.NewGuid().ToString("N").ToUpper();

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<PurchaseInvoiceReceipt>(options =>
            {
                options.ToTable(nameof(PurchaseInvoiceReceipt))
                    .HasKey(x => new { x.ReceiptId, x.InvoiceId })
                    .IsClustered();

                options.HasOne<PurchaseInvoice>()
                    .WithOne()
                    .HasForeignKey<PurchaseInvoiceReceipt>(p => p.InvoiceId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.Property(p => p.Amount)
                    .HasColumnType(ColumnTypes.Monetary);
            });
    }
}
