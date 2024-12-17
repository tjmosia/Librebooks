using LibreBooks.Models.Entity.CompanySpace;

using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Models.Entity.PurchasesSpace
{
    public class PurchaseInvoice
    {
        public virtual string? DocumentId { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? SupplierId { get; set; }

        public virtual PurchaseDocument? Document { get; set; }
        public virtual Company? Company { get; set; }
        public virtual ICollection<PurchaseOrderInvoice>? Orders { get; set; }
        public virtual ICollection<PurchaseInvoiceReturn>? Returns { get; set; }
        public virtual ICollection<PurchaseInvoiceReceipt>? Receipts { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<PurchaseInvoice>(options =>
            {
                options.ToTable(nameof(PurchaseInvoice))
                    .HasKey(p => p.DocumentId)
                    .IsClustered(false);

                options.HasIndex(p => new { p.CompanyId, p.SupplierId })
                    .IsClustered()
                    .IsUnique();

                options.HasOne(p => p.Document)
                    .WithOne()
                    .HasForeignKey<PurchaseInvoice>(p => p.DocumentId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasMany(p => p.Orders)
                    .WithOne(p => p.Invoice)
                    .HasForeignKey(p => p.InvoiceId)
                        .IsRequired(true)
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Returns)
                    .WithOne(p => p.Invoice)
                    .HasForeignKey(p => p.InvoiceId)
                        .IsRequired(true)
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Receipts)
                    .WithOne(p => p.Invoice)
                    .HasForeignKey(p => p.InvoiceId)
                        .IsRequired(true)
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}