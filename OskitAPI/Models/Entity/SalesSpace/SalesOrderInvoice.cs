using Microsoft.EntityFrameworkCore;

namespace MacbooksAPI.Models.Entity.SalesSpace
{
    public class SalesOrderInvoice
    {
        public virtual string? OrderId { get; set; }
        public virtual SalesOrder? Order { get; set; }
        public virtual string? InvoiceId { get; set; }
        public virtual SalesInvoice? Invoice { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<SalesOrderInvoice>(options =>
            {
                options.ToTable(nameof(SalesOrderInvoice))
                    .HasKey(p => new { p.OrderId, p.InvoiceId })
                    .IsClustered();
            });
    }
}
