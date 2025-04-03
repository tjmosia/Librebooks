using System.ComponentModel.DataAnnotations;

using Librebooks.Models.Entity.AccountingSpace;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.DocumentSpace;
using Librebooks.Models.Entity.InventorySpace;
using Librebooks.Models.Entity.PurchasesSpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SupplierSpace;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CompanySpace
{
    public class Company
    {
        public virtual string Id { get; set; }
        public virtual string? UniqueNumber { get; set; }
        public virtual string? LegalName { get; set; }
        public virtual string? TradingName { get; set; }
        public virtual string? RegNumber { get; set; }
        public virtual string? VATNumber { get; set; }
        public virtual string? PostalAddress { get; set; }
        public virtual string? PhysicalAddress { get; set; }
        public virtual string? PhoneNumber { get; set; }
        public virtual string? EmailAddress { get; set; }
        public virtual string? FaxNumber { get; set; }
        public virtual int YearsInBusiness { get; set; }
        public virtual string? BusinessSectorId { get; set; }
        public virtual string? LogoId { get; set; }
        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public virtual BusinessSector? BusinessSector { get; set; }
        public virtual ICollection<CompanyUser>? Users { get; set; }
        public virtual ICollection<DocumentSetup>? DocumentSetups { get; set; }
        public virtual CompanyRegionalSettings? RegionalSettings { get; set; }
        public virtual CompanyMailSettings? MailSettings { get; set; }
        public virtual ICollection<CompanyTaxType>? TaxTypes { get; set; }
        public virtual CompanyDefaultTaxType? DefaultTaxType { get; set; }
        public virtual ICollection<SalesPerson>? SalesPeople { get; set; }
        public virtual ICollection<PurchaseBuyer>? Buyers { get; set; }
        public virtual ItemSetup? ItemSetup { get; set; }

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
        public virtual CompanyLogo? Logo { get; set; }

        public virtual ICollection<Item>? Items { get; set; }
        public virtual ICollection<ItemAdjustment>? ItemAdjustments { get; set; }
        public virtual ICollection<ItemCategory>? ItemCategories { get; set; }

        public virtual ICollection<Journal>? JournalEntries { get; set; }
        public virtual ICollection<Account>? ChartOfAccounts { get; set; }
        public virtual ICollection<BankAccount>? BankAccounts { get; set; }
        public virtual CompanyDefaultBankAccount? DefaultBankAccount { get; set; }
        public virtual SupplierSetup? SupplierSetup { get; set; }
        public virtual CustomerSetup? CustomerSetup { get; set; }

        public Company ()
        {
            Id = Guid.NewGuid().ToString("N").ToUpper();
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

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
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasOne(p => p.MailSettings)
                    .WithOne(p => p.Company)
                    .HasForeignKey<CompanyMailSettings>(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasOne(p => p.DefaultBankAccount)
                    .WithOne(p => p.Company)
                    .HasForeignKey<CompanyDefaultBankAccount>(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasOne(p => p.Logo)
                    .WithOne()
                    .HasForeignKey<CompanyLogo>(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.DocumentSetups)
                    .WithOne()
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasOne(p => p.CustomerSetup)
                    .WithOne()
                    .HasForeignKey<CustomerSetup>(p => p.CompanyId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasOne(p => p.ItemSetup)
                    .WithOne()
                    .HasForeignKey<ItemSetup>(p => p.CompanyId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasOne(p => p.SupplierSetup)
                    .WithOne()
                    .HasForeignKey<SupplierSetup>(p => p.CompanyId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasMany<CompanyImage>()
                    .WithOne()
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

                options.HasMany(p => p.TaxTypes)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasOne(p => p.DefaultTaxType)
                    .WithOne(p => p.Company)
                    .HasForeignKey<CompanyDefaultTaxType>(p => p.CompanyId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });
    }
}
