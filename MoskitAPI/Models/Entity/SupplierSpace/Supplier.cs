using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using Moskit.Core.Types;
using Moskit.Models.Entity.CompanySpace;
using Moskit.Models.Entity.PurchasesSpace;

namespace Moskit.Models.Entity.SupplierSpace
{
    public class Supplier
    {
        public virtual string? Id { get; set; }
        public virtual string? RegisteredName { get; set; }
        public virtual string? TradingName { get; set; }
        public virtual string? VendorNumber { get; set; }
        public virtual string? VATRegNumber { get; set; }
        public virtual string? Telephone { get; set; }
        public virtual string? Email { get; set; }
        public virtual string? Fax { get; set; }
        public virtual decimal Balance { get; set; }
        public virtual string? PhysicalAddress { get; set; }
        public virtual string? PostalAddress { get; set; }
        public virtual decimal Discount { get; set; }
        public virtual int PaymentTermId { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? CategoryId { get; set; }
        public virtual string? VATId { get; set; }

        [Timestamp, ConcurrencyCheck]
        public virtual byte[]? RowVersion { get; set; }

        public virtual SupplierCategory? Category { get; set; }
        public virtual CompanyValueAddedTax? VAT { get; set; }
        public virtual Company? Company { get; set; }

        public virtual ICollection<PurchaseOrder>? Orders { get; set; }
        public virtual ICollection<PurchaseInvoice>? Invoices { get; set; }
        public virtual ICollection<PurchaseReturn>? Returns { get; set; }
        public virtual ICollection<PurchaseReceipt>? Receipts { get; set; }
        public virtual ICollection<SupplierAdjustment>? Adjustments { get; set; }
        public virtual ICollection<SupplierContact>? Contacts { get; set; }
        public virtual ICollection<SupplierAccountsContact>? AccountsContacts { get; set; }

        public Supplier ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<Supplier>(options =>
            {
                options.ToTable(nameof(Supplier))
                    .HasKey(p => p.Id)
                    .IsClustered(false);

                options.HasIndex(p => new { p.CompanyId, p.VendorNumber })
                    .IsClustered()
                    .IsUnique();

                options.Property(p => p.Discount)
                    .HasColumnType(ColumnTypes.Percentage);

                options.Property(p => p.Balance)
                    .HasColumnType(ColumnTypes.Percentage);

                options.HasOne(p => p.VAT)
                    .WithOne()
                    .HasForeignKey<Supplier>(p => p.VATId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.AccountsContacts)
                    .WithOne(p => p.Supplier)
                    .HasForeignKey(p => p.SupplierId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Orders)
                    .WithOne(p => p.Supplier)
                    .HasForeignKey(p => p.SupplierId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Adjustments)
                    .WithOne(p => p.Supplier)
                    .HasForeignKey(p => p.SupplierId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Contacts)
                    .WithOne(p => p.Supplier)
                    .HasForeignKey(p => p.SupplierId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Invoices)
                    .WithOne(p => p.Supplier)
                    .HasForeignKey(p => p.SupplierId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Returns)
                    .WithOne(p => p.Supplier)
                    .HasForeignKey(p => p.SupplierId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Receipts)
                    .WithOne(p => p.Supplier)
                    .HasForeignKey(p => p.SupplierId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
