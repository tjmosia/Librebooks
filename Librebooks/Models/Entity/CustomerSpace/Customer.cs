﻿using System.ComponentModel.DataAnnotations;

using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CustomerSpace
{
    public class Customer
    {
        public virtual string Id { get; set; }
        public virtual string? LegalName { get; set; }
        public virtual string? TradingName { get; set; }
        public virtual string? DeliveryAddress { get; set; }
        public virtual string? BillingAddress { get; set; }
        public virtual string? Phone { get; set; }
        public virtual string? Email { get; set; }
        public virtual string? ValueAddedTaxNumber { get; set; }
        public virtual string? AccountNumber { get; set; }
        public virtual int PaymentTermsInDays { get; set; }
        public virtual string? PaymentTermsFrom { get; set; }
        public virtual bool Active { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? CategoryId { get; set; }
        public virtual string? SalesPersonId { get; set; }
        public virtual bool AcceptsElectronicInvoices { get; set; } = false;
        public virtual string? Website { get; set; }
        public virtual string? ShippingTermId { get; set; }
        public virtual string? ShippingMethodId { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public virtual Company? Company { get; set; }
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

        public Customer ()
        {
            Id = Guid.NewGuid().ToString("N").ToUpper();
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<Customer>(options =>
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

                options.HasMany(p => p.Orders)
                    .WithOne()
                    .HasForeignKey(p => p.CustomerId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Invoices)
                    .WithOne()
                    .HasForeignKey(p => p.CustomerId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Credits)
                    .WithOne()
                    .HasForeignKey(p => p.CustomerId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Receipts)
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

                options.HasOne(p => p.ShippingMethod)
                    .WithOne()
                    .HasForeignKey<Customer>(p => p.ShippingMethodId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
