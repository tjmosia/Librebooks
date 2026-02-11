using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.CustomerSpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SalesSpace;

[Table(nameof(SalesInvoice))]
public class SalesInvoice
{
    [Key]
    public virtual int DocumentId { get; set; }
    public virtual int CompanyId { get; set; }
    public virtual int CustomerId { get; set; }

    public virtual SalesDocument? Document { get; set; }

    public virtual SalesOrderInvoice? Order { get; set; }
    public virtual SalesQuoteInvoice? Quote { get; set; }
    public virtual ICollection<SalesInvoiceCredit>? Credits { get; set; }
    public virtual ICollection<SalesInvoiceReceipt>? Receipts { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<SalesInvoice>(options =>
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

            options.HasOne(p => p.Order)
                .WithOne(p => p.Invoice)
                .HasForeignKey<SalesOrderInvoice>(p => p.InvoiceId)
                    .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            options.HasOne(p => p.Quote)
                .WithOne(p => p.Invoice)
                .HasForeignKey<SalesOrderInvoice>(p => p.InvoiceId)
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

            options.HasOne<Company>()
                .WithMany()
                .HasForeignKey(p => p.CompanyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(p => p.CustomerId)
                    .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}