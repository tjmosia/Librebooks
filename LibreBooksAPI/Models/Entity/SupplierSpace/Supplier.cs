using System.ComponentModel.DataAnnotations;

using LibreBooks.Core.Types;
using LibreBooks.Models.Entity.CompanySpace;
using LibreBooks.Models.Entity.PurchasesSpace;

using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Models.Entity.SupplierSpace
{
    public class Supplier
    {
        public virtual string Id { get; set; }
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
        public virtual string? TaxTypeId { get; set; }

        [ConcurrencyCheck]
        public virtual string RowVersion { get; set; }

        public virtual SupplierCategory? Category { get; set; }
        public virtual CompanyTaxType? TaxType { get; set; }
        public virtual Company? Company { get; set; }

        public virtual ICollection<PurchaseOrder>? Orders { get; set; }
        public virtual ICollection<PurchaseInvoice>? Invoices { get; set; }
        public virtual ICollection<PurchaseReturn>? Returns { get; set; }
        public virtual ICollection<PurchaseReceipt>? Receipts { get; set; }
        public virtual ICollection<SupplierAdjustment>? Adjustments { get; set; }
        public virtual ICollection<SupplierContact>? Contacts { get; set; }
        public virtual ICollection<SupplierNote>? Notes { get; set; }
        public virtual ICollection<SupplierAccountsContact>? AccountsContacts { get; set; }

        public Supplier ()
        {
            Id = Guid.NewGuid().ToString("N").ToUpper();
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

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

                options.HasOne(p => p.TaxType)
                    .WithOne()
                    .HasForeignKey<Supplier>(p => p.TaxTypeId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Notes)
                    .WithOne()
                    .HasForeignKey(p => p.SupplierId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.AccountsContacts)
                    .WithOne()
                    .HasForeignKey(p => p.SupplierId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Orders)
                    .WithOne()
                    .HasForeignKey(p => p.SupplierId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Adjustments)
                    .WithOne(p => p.Supplier)
                    .HasForeignKey(p => p.SupplierId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Contacts)
                    .WithOne()
                    .HasForeignKey(p => p.SupplierId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Invoices)
                    .WithOne()
                    .HasForeignKey(p => p.SupplierId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Returns)
                    .WithOne()
                    .HasForeignKey(p => p.SupplierId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Receipts)
                    .WithOne()
                    .HasForeignKey(p => p.SupplierId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
