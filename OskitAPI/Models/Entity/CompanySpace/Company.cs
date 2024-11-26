using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using MacbooksAPI.Models.Entity.AccountingSpace;
using MacbooksAPI.Models.Entity.BankingSpace;
using MacbooksAPI.Models.Entity.CustomerSpace;
using MacbooksAPI.Models.Entity.DocumentSpace;
using MacbooksAPI.Models.Entity.InventorySpace;
using MacbooksAPI.Models.Entity.PurchasesSpace;
using MacbooksAPI.Models.Entity.SalesSpace;
using MacbooksAPI.Models.Entity.SupplierSpace;
using MacbooksAPI.Models.Entity.SystemSpace;

namespace MacbooksAPI.Models.Entity.CompanySpace
{
    public class Company
    {
        public virtual string? Id { get; set; }
        public virtual string? Number { get; set; }
        public virtual string? LegalName { get; set; }
        public virtual string? TradingName { get; set; }
        public virtual string? RegNumber { get; set; }
        public virtual string? ValueAddedTaxNumber { get; set; }
        public virtual string? BillingAddress { get; set; }
        public virtual string? DeliveryAddress { get; set; }
        public virtual string? Telephone { get; set; }
        public virtual string? EmailAddress { get; set; }
        public virtual string? FaxNumber { get; set; }
        public virtual string? Logo { get; set; }
        public virtual int YearsInBusienss { get; set; }
        public virtual string? BusinessSectorId { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public void UpdateConcurrencyToken ()
            => RowVersion = Guid.NewGuid().ToString("N");

        public virtual BusinessSector? BusinessSector { get; set; }
        public virtual ICollection<CompanyUser>? Users { get; set; }
        public virtual ICollection<DocumentSetup>? DocumentSetups { get; set; }
        public virtual CompanyRegionalSettings? RegionalSettings { get; set; }
        public virtual CompanyMailSettings? MailSettings { get; set; }
        public virtual ICollection<CompanyValueAddedTax>? ValueAddedTaxes { get; set; }
        public virtual CompanyDefaultValueAddedTax? DefaultVAT { get; set; }
        public virtual ICollection<SalesPerson>? SalesPeople { get; set; }
        public virtual ICollection<PurchaseBuyer>? Buyers { get; set; }

        public virtual ICollection<Supplier>? Suppliers { get; set; }
        public virtual ICollection<SupplierAdjustment>? SupplierAdjustments { get; set; }
        public virtual ICollection<PurchaseReceipt>? PurchaseReceipts { get; set; }
        public virtual ICollection<PurchaseInvoice>? PurchaseInvoices { get; set; }
        public virtual ICollection<PurchaseOrder>? PurchaseOrders { get; set; }
        public virtual ICollection<PurchaseReturn>? PurchaseReturns { get; set; }

        public virtual ICollection<Customer>? Customers { get; set; }
        public virtual ICollection<SalesQuote>? SalesQuotes { get; set; }
        public virtual ICollection<SalesOrder>? SalesOrders { get; set; }
        public virtual ICollection<SalesInvoice>? SalesInvoices { get; set; }
        public virtual ICollection<SalesCredit>? SalesCredits { get; set; }
        public virtual ICollection<SalesReceipt>? SalesReceipts { get; set; }

        public virtual ICollection<Item>? Items { get; set; }
        public virtual ICollection<ItemAdjustment>? ItemAdjustments { get; set; }
        public virtual ICollection<ItemCategory>? ItemCategories { get; set; }

        public virtual ICollection<Journal>? JournalEntries { get; set; }
        public virtual ICollection<Account>? ChartOfAccounts { get; set; }
        public virtual ICollection<BankAccount>? BankAccounts { get; set; }
        public virtual CompanyDefaultBankAccount? DefaultBankAccount { get; set; }

        public Company ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<Company>(options =>
            {
                options.ToTable(nameof(Company))
                    .HasKey(p => p.Id)
                    .IsClustered();

                options.HasOne(p => p.RegionalSettings)
                    .WithOne(p => p.Company)
                    .HasForeignKey<CompanyRegionalSettings>(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasOne(p => p.MailSettings)
                    .WithOne(p => p.Company)
                    .HasForeignKey<CompanyMailSettings>(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasOne(p => p.DefaultBankAccount)
                    .WithOne(p => p.Company)
                    .HasForeignKey<CompanyDefaultBankAccount>(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.ValueAddedTaxes)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.DocumentSetups)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Users)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Customers)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Suppliers)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.SalesPeople)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Buyers)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.SalesQuotes)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.SalesOrders)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.SalesInvoices)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.SalesReceipts)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.ItemCategories)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Items)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.ItemAdjustments)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.PurchaseReceipts)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.JournalEntries)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.SupplierAdjustments)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.ChartOfAccounts)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.BankAccounts)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
