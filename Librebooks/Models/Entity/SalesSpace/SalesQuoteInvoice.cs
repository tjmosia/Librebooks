using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SalesSpace
{
    [Table(nameof(SalesQuoteInvoice))]
    public class SalesQuoteInvoice
    {
        [Key]
        public virtual int DocumentId { get; set; }
        public virtual int InvoiceId { get; set; }
        public virtual int QuoteId { get; set; }

        public virtual SalesDocument? Document { get; set; }
        public virtual SalesInvoice? Invoice { get; set; }
        public virtual SalesQuote? Quote { get; set; }

        public static void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesQuoteInvoice>(options =>
            {
                options.HasOne<SalesDocument>()
                    .WithOne()
                    .HasForeignKey<SalesQuoteInvoice>(p => p.DocumentId)
                        .IsRequired(true)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
