using Microsoft.EntityFrameworkCore;

using OskitAPI.Models.Entity.CompanySpace;
using OskitAPI.Models.Entity.CustomerSpace;

namespace OskitAPI.Models.Entity.SalesSpace
{
    public class SalesOrder
    {
        public virtual string? DocumentId { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? CustomerId { get; set; }

        public virtual SalesDocument? Document { get; set; }
        public virtual Company? Company { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual ICollection<SalesOrderInvoice>? Invoices { get; set; }
        public virtual ICollection<SalesQuoteOrder>? Quotes { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<SalesOrder>(options =>
            {
                options.ToTable(nameof(SalesOrder))
                    .HasKey(p => p.DocumentId)
                    .IsClustered(false);

                options.HasIndex(p => new { p.CustomerId, p.CompanyId })
                    .IsClustered();

                options.HasMany(p => p.Invoices)
                    .WithOne(p => p.Order)
                    .HasForeignKey(p => p.OrderId)
                        .IsRequired(true)
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasOne(p => p.Document)
                    .WithOne()
                    .HasForeignKey<SalesOrder>(p => p.DocumentId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasMany(p => p.Quotes)
                    .WithOne(p => p.Order)
                    .HasForeignKey(p => p.OrderId)
                        .IsRequired(true)
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
