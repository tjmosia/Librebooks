using Librebooks
	.Models.Entity;
using Librebooks.Models.Entity.AccountingSpace;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.DocumentSpace;
using Librebooks.Models.Entity.GeneralSpace;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Models.Entity.InventorySpace;
using Librebooks.Models.Entity.PurchasesSpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SupplierSpace;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Data
{
	public class AppDbContext :
		IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
	{
		public AppDbContext (DbContextOptions<AppDbContext> options)
			: base(options) { }

		public AppDbContext () { }

		/************************************************************************************************
         * Company Space
         ************************************************************************************************/
		public DbSet<Company>? Companies { get; set; }
		public DbSet<CompanyUser>? CompanyUsers { get; set; }
		public DbSet<CompanyBankAccount>? CompanyDefaultBankAccounts { get; set; }
		public DbSet<CompanyTax>? CompanyTaxes { get; set; }
		public DbSet<CompanyMailSetup>? CompanyMailSettings { get; set; }
		public DbSet<CompanyLogo>? CompanyLogos { get; set; }
		public DbSet<CompanyImage>? CompanyImages { get; set; }

		/************************************************************************************************
         * Customer Space
         ************************************************************************************************/
		public DbSet<Customer>? Customers { get; set; }
		public DbSet<CustomerAccountsContact>? CustomerAccountsContacts { get; set; }
		public DbSet<CustomerAdjustment>? CustomerAdjustments { get; set; }
		public DbSet<CustomerCategory>? CustomerCategorys { get; set; }
		public DbSet<CustomerContact>? CustomerContacts { get; set; }
		public DbSet<CustomerNote>? CustomerNotes { get; set; }
		public DbSet<CustomerSetup>? CustomerSetups { get; set; }
		public DbSet<CompanyRegionalSetup>? CompanyRegionalSettings { get; set; }

		/************************************************************************************************
         * Sales Space
         ************************************************************************************************/
		public DbSet<SalesPerson>? SalesPeople { get; set; }
		public DbSet<SalesDocument>? SalesDocuments { get; set; }
		public DbSet<SalesDocumentNote>? SalesDocumentNotes { get; set; }
		public DbSet<SalesDocumentLine>? SalesDocumentLines { get; set; }
		public DbSet<SalesOrder>? SalesOrders { get; set; }
		public DbSet<SalesInvoice>? SalesInvoices { get; set; }
		public DbSet<SalesInvoiceReceipt>? SalesInvoiceReceipts { get; set; }
		public DbSet<SalesOrderInvoice>? SalesOrderInvoices { get; set; }
		public DbSet<SalesQuote>? SalesQuotes { get; set; }
		public DbSet<SalesQuoteOrder>? SalesQuoteOrders { get; set; }
		public DbSet<SalesCredit>? SalesCredits { get; set; }
		public DbSet<SalesReceipt>? SalesReceipts { get; set; }
		public DbSet<SalesLine>? SalesLines { get; set; }
		public DbSet<DocumentCustomerDetails>? DocumentCustomerDetails { get; set; }
		public DbSet<SalesInvoiceCredit>? SalesInvoiceCredits { get; set; }
		public DbSet<SalesInvoiceWriteoff>? SalesInvoiceWriteoffs { get; set; }

		/************************************************************************************************
         * Inventory Space
         ************************************************************************************************/
		public DbSet<Item>? Items { get; set; }
		public DbSet<ItemSetup>? ItemSetups { get; set; }
		public DbSet<ItemAdjustment>? ItemAdjustments { get; set; }
		public DbSet<ItemCategory>? ItemCategories { get; set; }
		public DbSet<ItemInventory>? ItemInventories { get; set; }
		public DbSet<ItemInfo>? ItemDetails { get; set; }


		/************************************************************************************************
         * Accounting Space
         ************************************************************************************************/
		public DbSet<Account>? Accounts { get; set; }
		public DbSet<AccountCategory>? AccountCategories { get; set; }
		public DbSet<JournalEntry>? JournalEntries { get; set; }
		public DbSet<JournalNote>? JournalNotes { get; set; }
		public DbSet<AccountCashFlowType>? AccountCashFlowTypes { get; set; }

		/************************************************************************************************
         * Banking Space
         ************************************************************************************************/
		public DbSet<BankAccount>? BankAccounts { get; set; }
		public DbSet<BankAccountCategory>? BankAccountCategories { get; set; }

		/************************************************************************************************
         * Document Space
         ************************************************************************************************/
		public DbSet<DocumentSetup>? DocumentSetups { get; set; }
		public DbSet<DocumentStatus>? DocumentStatuses { get; set; }
		public DbSet<DocumentPrintTemplate>? DocumentPrintTemplates { get; set; }
		public DbSet<DocumentCompanyDetail>? DocumentCompanyDetails { get; set; }
		public DbSet<DocumentType>? DocumentTypes { get; set; }

		/************************************************************************************************
         * System Space
         ************************************************************************************************/
		public DbSet<ShippingTerm>? ShippingTerms { get; set; }
		public DbSet<ShippingMethod>? ShippingMethods { get; set; }
		public DbSet<Country>? Countries { get; set; }
		public DbSet<Currency>? Currencies { get; set; }
		public DbSet<DateFormat>? DateFormats { get; set; }
		public DbSet<Tax>? Taxes { get; set; }
		public DbSet<PaymentMethod>? PaymentMethods { get; set; }
		public DbSet<PaymentTerm>? PaymentTerms { get; set; }
		public DbSet<CompanySetup>? CompanySetup { get; set; }
		public DbSet<BusinessSector>? BusinessSectors { get; set; }

		/************************************************************************************************
         * Supplier Space
         ************************************************************************************************/
		public DbSet<Supplier>? Suppliers { get; set; }
		public DbSet<SupplierNote>? SupplierNotes { get; set; }
		public DbSet<SupplierAccountsContact>? SupplierAccountsContacts { get; set; }
		public DbSet<SupplierAdjustment>? SupplierAdjustments { get; set; }
		public DbSet<SupplierCategory>? SupplierCategories { get; set; }
		public DbSet<SupplierContact>? SupplierContacts { get; set; }
		public DbSet<SupplierSetup>? SupplierSetups { get; set; }

		/************************************************************************************************
         * Purchases Space
         ************************************************************************************************/
		public DbSet<PurchaseDocument>? PurchaseDocuments { get; set; }
		public DbSet<PurchaseDocumentNote>? PurchaseDocumentNotes { get; set; }
		public DbSet<PurchaseDocumentLine>? PurchaseDocumentLines { get; set; }
		public DbSet<PurchaseOrder>? PurchaseOrders { get; set; }
		public DbSet<PurchaseBuyer>? PurchaseBuyers { get; set; }
		public DbSet<PurchaseInvoice>? PurchaseInvoices { get; set; }
		public DbSet<PurchaseLine>? PurchaseLines { get; set; }
		public DbSet<PurchaseReturn>? PurchaseReturns { get; set; }
		public DbSet<PurchaseInvoiceReturn>? PurchaseReturnInvoices { get; set; }
		public DbSet<PurchaseReceipt>? PurchaseReceipts { get; set; }
		public DbSet<PurchaseOrderInvoice>? PurchaseOrderInvoices { get; set; }
		public DbSet<PurchaseInvoiceReceipt>? PurchaseInvoiceReceipts { get; set; }
		public DbSet<DocumentSupplierDetail>? DocumentSupplierDetails { get; set; }
		public DbSet<PurchaseRequestForQuote>? PurchaseRequestForQuotes { get; set; }

		/************************************************************************************************
         * GENERAL SPACE
         ************************************************************************************************/
		public DbSet<Contact>? Contacts { get; set; }
		public DbSet<Note>? Notes { get; set; }
		public DbSet<VerificationRequest>? VerificationRequests { get; set; }

		protected override void OnModelCreating (ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			ModelsBuilderAll.BuildModels(builder);
		}
	}
}
