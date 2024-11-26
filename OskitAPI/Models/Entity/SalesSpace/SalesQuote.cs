using Microsoft.EntityFrameworkCore;

using MacbooksAPI.Models.Entity.CompanySpace;
using MacbooksAPI.Models.Entity.CustomerSpace;

namespace MacbooksAPI.Models.Entity.SalesSpace
{
    public class SalesQuote
    {
        public virtual string? CustomerId { get; set; }
        public virtual string? DocumentId { get; set; }
        public virtual string? CompanyId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual SalesDocument? Document { get; set; }
        public virtual Company? Company { get; set; }

        public virtual ICollection<SalesQuoteOrder>? Orders { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<SalesQuote>(options =>
            {
                options.ToTable(nameof(SalesQuote))
                    .HasKey(p => p.DocumentId)
                    .IsClustered(false);

                options.HasIndex(p => new { p.CompanyId, p.CustomerId })
                    .IsClustered()
                    .IsUnique();

                options.HasOne(p => p.Document)
                    .WithOne()
                    .HasForeignKey<SalesQuote>(p => p.DocumentId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasMany(p => p.Orders)
                    .WithOne(p => p.Quote)
                    .HasForeignKey(p => p.QuoteId)
                        .IsRequired(true)
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
