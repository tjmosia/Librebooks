using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Models.Entity.CustomerSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SalesSpace;

[Table(nameof(SalesDocumentCustomerDetails))]
public class SalesDocumentCustomerDetails
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }
    public virtual int CustomerId { get; set; }

    [Required, MaxLength(100)]
    public virtual string? CustomerName { get; set; }

    [Required, MaxLength(155)]
    public virtual string? BillingAddress { get; set; }

    [Required, MaxLength(105)]
    public virtual string? ShippingAddress { get; set; }

    [MaxLength(10)]
    public virtual string? VATNumber { get; set; }

    public virtual DateTime DateCreated { get; set; }
    public virtual bool Active { get; set; }
    public virtual Customer? Customer { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<SalesDocumentCustomerDetails>(options =>
           {
               options.HasIndex(p => p.CustomerId)
                   .IsClustered();

               options.HasOne<Customer>()
                   .WithOne()
                   .HasForeignKey<SalesDocumentCustomerDetails>(p => p.CustomerId)
                       .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);

               options.HasMany<SalesDocument>()
                   .WithOne(p => p.CustomerDetails)
                   .HasForeignKey(propa => propa.CustomerDetailsId)
                       .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

               options.HasOne(p => p.Customer)
                   .WithOne()
                   .HasForeignKey<SalesDocumentCustomerDetails>(p => p.CustomerId)
                       .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);

               options.HasMany<SalesReceipt>()
                   .WithOne(p => p.CustomerDetails)
                   .HasForeignKey(p => p.CustomerDetailsId)
                       .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
           });
    }
}
