using Microsoft.EntityFrameworkCore;

namespace OskitBlazor.Models.Entity.SalesSpace
{
    public class SalesQuoteOrder
    {
        public virtual string? QuoteId { get; set; }
        public virtual string? OrderId { get; set; }

        public virtual SalesQuote? Quote { get; set; }
        public virtual SalesOrder? Order { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<SalesQuoteOrder>(options =>
            {
                options.ToTable(nameof(SalesQuoteOrder))
                    .HasKey(p => new { p.QuoteId, p.OrderId })
                    .IsClustered();
            });

    }
}
