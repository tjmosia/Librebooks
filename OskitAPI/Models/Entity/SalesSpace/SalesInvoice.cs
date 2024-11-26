using Microsoft.EntityFrameworkCore;

using MacbooksAPI.Models.Entity.CompanySpace;
using MacbooksAPI.Models.Entity.CustomerSpace;

namespace MacbooksAPI.Models.Entity.SalesSpace
{
    public class SalesInvoice
    {
        public virtual string? DocumentId { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? CustomerId { get; set; }

        public virtual SalesDocument? Document { get; set; }
        public virtual Company? Company { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual ICollection<SalesOrderInvoice>? Orders { get; set; }
        public virtual ICollection<SalesInvoiceCredit>? Credits { get; set; }
        public virtual ICollection<SalesInvoiceReceipt>? Receipts { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<SalesInvoice>(options =>
            {
                options.ToTable(nameof(SalesInvoice))
                    .HasKey(p => p.DocumentId)
                    .IsClustered(false);

                options.HasIndex(p => new { p.CompanyId, p.CustomerId })
                    .IsClustered()
                    .IsUnique();

                options.HasOne(p => p.Document)
                    .WithOne()
                    .HasForeignKey<SalesInvoice>(p => p.DocumentId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasMany(p => p.Orders)
                    .WithOne(p => p.Invoice)
                    .HasForeignKey(p => p.InvoiceId)
                        .IsRequired(true)
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Credits)
                    .WithOne(p => p.Invoice)
                    .HasForeignKey(p => p.InvoiceId)
                        .IsRequired(true)
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Receipts)
                    .WithOne(p => p.Invoice)
                    .HasForeignKey(p => p.InvoiceId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}