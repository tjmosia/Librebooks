using Microsoft.EntityFrameworkCore;

namespace OskitBlazor.Models.Entity.SalesSpace
{
    public class SalesInvoiceCredit
    {
        public virtual string? InvoiceId { get; set; }
        public virtual string? CreditId { get; set; }
        public virtual string? Comment { get; set; }

        public virtual SalesCredit? Credit { get; set; }
        public virtual SalesInvoice? Invoice { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<SalesInvoiceCredit>(options =>
            {
                options.ToTable(nameof(SalesInvoiceCredit))
                    .HasKey(x => new { x.InvoiceId, x.CreditId });
            });
    }
}
