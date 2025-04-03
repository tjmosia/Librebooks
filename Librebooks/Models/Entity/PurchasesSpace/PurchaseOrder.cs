using Librebooks.Models.Entity.CompanySpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.PurchasesSpace
{
    public class PurchaseOrder
    {
        public virtual string? DocumentId { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? SupplierId { get; set; }

        public virtual PurchaseDocument? Document { get; set; }
        public virtual Company? Company { get; set; }
        public virtual ICollection<PurchaseOrderInvoice>? Invoices { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<PurchaseOrder>(options =>
            {
                options.ToTable(nameof(PurchaseOrder))
                    .HasKey(p => p.DocumentId)
                    .IsClustered(false);

                options.HasIndex(p => new { p.SupplierId, p.CompanyId })
                    .IsClustered()
                    .IsUnique();

                options.HasOne(p => p.Document)
                    .WithOne()
                    .HasForeignKey<PurchaseOrder>(p => p.DocumentId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasMany(p => p.Invoices)
                    .WithOne(p => p.Order)
                    .HasForeignKey(p => p.OrderId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
