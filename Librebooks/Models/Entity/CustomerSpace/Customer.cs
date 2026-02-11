using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CustomerSpace;

[Table(nameof(Customer))]
public class Customer () : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [Required, MaxLength(20)]
    public virtual string? Number { get; set; }

    [Required, MaxLength(100)]
    public virtual string? LegalName { get; set; }

    [MaxLength(100)]
    public virtual string? TradingName { get; set; }

    public virtual string? DeliveryAddress { get; set; }
    public virtual string? BillingAddress { get; set; }

    [MaxLength(15), Required]
    public virtual string? Phone { get; set; }

    [MaxLength(75)]
    public virtual string? Email { get; set; }

    [MaxLength(10)]
    public virtual string? VATNumber { get; set; }

    public virtual int PaymentTermInDays { get; set; }
    public virtual int PaymentTermId { get; set; }

    public virtual bool Active { get; set; }
    public virtual int CompanyId { get; set; }
    public virtual int CategoryId { get; set; }
    public virtual int SalesPersonId { get; set; }
    public virtual bool AcceptsElectronicInvoices { get; set; } = false;
    public virtual int ShippingTermId { get; set; }
    public virtual int ShippingMethodId { get; set; }
    public virtual string? Website { get; set; }

    public virtual PaymentTerm? PaymentTerm { get; set; }
    public virtual CustomerCategory? Category { get; set; }
    public virtual ICollection<SalesInvoice>? Invoices { get; set; }
    public virtual ICollection<SalesQuote>? Quotes { get; set; }
    public virtual ICollection<SalesOrder>? Orders { get; set; }
    public virtual ICollection<SalesReceipt>? Receipts { get; set; }
    public virtual ICollection<SalesCredit>? Credits { get; set; }
    public virtual ICollection<CustomerContact>? ContactPeople { get; set; }
    public virtual ICollection<CustomerNote>? Notes { get; set; }
    public virtual ShippingTerm? ShippingTerm { get; set; }
    public virtual ShippingMethod? ShippingMethod { get; set; }
    public virtual SalesPerson? SalesPerson { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<Customer>(options =>
        {
            options.ToTable(nameof(Customer))
                .HasKey(p => p.Id)
                .IsClustered(false);

            options.HasIndex(p => new { p.CompanyId, p.CategoryId })
                .IsClustered();

            options.HasMany(p => p.Quotes)
                .WithOne()
                .HasForeignKey(p => p.CustomerId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasMany<SalesDocumentCustomerDetails>()
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasMany(p => p.Notes)
                .WithOne()
                .HasForeignKey(p => p.CustomerId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasMany(p => p.ContactPeople)
                .WithOne()
                .HasForeignKey(p => p.CustomerId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasOne(p => p.ShippingTerm)
                .WithOne()
                .HasForeignKey<Customer>(p => p.ShippingTermId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasOne(p => p.PaymentTerm)
                .WithOne()
                .HasForeignKey<Customer>(p => p.PaymentTermId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasOne(p => p.ShippingMethod)
                .WithOne()
                .HasForeignKey<Customer>(p => p.ShippingMethodId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasOne<Company>()
                .WithMany()
                .HasForeignKey(p => p.CompanyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

        });
    }
}
