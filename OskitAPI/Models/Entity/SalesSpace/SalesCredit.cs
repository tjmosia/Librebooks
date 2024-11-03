using Microsoft.EntityFrameworkCore;

using OskitAPI.Models.Entity.CompanySpace;
using OskitAPI.Models.Entity.CustomerSpace;

namespace OskitAPI.Models.Entity.SalesSpace
{
    public class SalesCredit
    {
        public virtual string? DocumentId { get; set; }
        public virtual string? CustomerId { get; set; }
        public virtual string? CompanyId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Company? Company { get; set; }
        public virtual SalesDocument? Document { get; set; }
        public virtual ICollection<SalesInvoiceCredit>? CreditedInvoices { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<SalesCredit>(options =>
            {
                options.ToTable(nameof(SalesCredit))
                    .HasKey(e => e.DocumentId)
                    .IsClustered(false);

                options.HasIndex(p => new { p.CompanyId, p.CustomerId })
                    .IsClustered();

                options.HasOne(p => p.Document)
                    .WithOne()
                    .HasForeignKey<SalesCredit>(p => p.DocumentId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasMany(p => p.CreditedInvoices)
                    .WithOne(p => p.Credit)
                    .HasForeignKey(p => p.CreditId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
