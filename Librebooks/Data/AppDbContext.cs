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
        IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options) { }

        public AppDbContext () { }

        /************************************************************************************************
         * Company Space
         ************************************************************************************************/
        public DbSet<Company>? Company { get; set; }
        public DbSet<CompanyUser>? CompanyUser { get; set; }
        public DbSet<CompanyDefaultTaxType>? CompanyDefaultTaxType { get; set; }
        public DbSet<CompanyDefaultBankAccount>? CompanyDefaultBankAccount { get; set; }
        public DbSet<CompanyTaxType>? CompanyTaxType { get; set; }
        public DbSet<CompanyMailSettings>? CompanyMailSettings { get; set; }
        public DbSet<CompanyLogo>? CompanyLogo { get; set; }
        public DbSet<CompanyImage>? CompanyImage { get; set; }

        /************************************************************************************************
         * Customer Space
         ************************************************************************************************/
        public DbSet<Customer>? Customer { get; set; }
        public DbSet<CustomerAccountsContact>? customerAccountsContact { get; set; }
        public DbSet<CustomerAdjustment>? CustomerAdjustment { get; set; }
        public DbSet<CustomerCategory>? CustomerCategory { get; set; }
        public DbSet<CustomerContact>? CustomerContact { get; set; }
        public DbSet<CustomerNote>? CustomerNote { get; set; }
        public DbSet<CustomerSetup>? CustomerSetup { get; set; }
        public DbSet<CompanyRegionalSettings>? CompanyRegionalSettings { get; set; }

        /************************************************************************************************
         * Sales Space
         ************************************************************************************************/
        public DbSet<SalesPerson>? SalesPerson { get; set; }
        public DbSet<SalesDocument>? SalesDocument { get; set; }
        public DbSet<SalesDocumentNote>? SalesDocumentNote { get; set; }
        public DbSet<SalesDocumentLine>? SalesDocumentLine { get; set; }
        public DbSet<SalesOrder>? SalesOrder { get; set; }
        public DbSet<SalesInvoice>? SalesInvoice { get; set; }
        public DbSet<SalesInvoiceReceipt>? SalesInvoiceReceipt { get; set; }
        public DbSet<SalesOrderInvoice>? SalesOrderInvoice { get; set; }
        public DbSet<SalesQuote>? SalesQuote { get; set; }
        public DbSet<SalesCredit>? SalesCredit { get; set; }
        public DbSet<SalesReceipt>? SalesReceipt { get; set; }
        public DbSet<SalesLine>? SalesLine { get; set; }
        public DbSet<SalesQuoteOrder>? SalesQuoteOrder { get; set; }
        public DbSet<SalesDocumentCustomerDetails>? SalesDocumentCustomerDetails { get; set; }
        public DbSet<SalesDocumentCompanyDetails>? SalesDocumentCompanyDetails { get; set; }

        /************************************************************************************************
         * Inventory Space
         ************************************************************************************************/
        public DbSet<Item>? Item { get; set; }
        public DbSet<ItemSetup>? ItemSetup { get; set; }
        public DbSet<ItemAdjustment>? ItemAdjustment { get; set; }
        public DbSet<ItemCategory>? ItemCategory { get; set; }
        public DbSet<ItemInventory>? ItemInventory { get; set; }

        /************************************************************************************************
         * Accounting Space
         ************************************************************************************************/
        public DbSet<Account>? Account { get; set; }
        public DbSet<AccountCategory>? AccountCategory { get; set; }
        public DbSet<Journal>? Journal { get; set; }
        public DbSet<JournalNote>? JournalNote { get; set; }
        public DbSet<AccountCashFlowType>? AccountCashFlowType { get; set; }

        /************************************************************************************************
         * Banking Space
         ************************************************************************************************/
        public DbSet<BankAccount>? BankAccount { get; set; }
        public DbSet<BankAccountCategory>? BankAccountCategory { get; set; }

        /************************************************************************************************
         * Document Space
         ************************************************************************************************/
        public DbSet<DocumentSetup>? DocumentSetup { get; set; }
        public DbSet<DocumentStatus>? DocumentStatus { get; set; }
        public DbSet<DocumentPrintTemplate>? DocumentPrintTemplate { get; set; }

        /************************************************************************************************
         * System Space
         ************************************************************************************************/
        public DbSet<ShippingTerm>? ShippingTerm { get; set; }
        public DbSet<ShippingMethod>? ShippingMethod { get; set; }
        public DbSet<Country>? Country { get; set; }
        public DbSet<Currency>? Currency { get; set; }
        public DbSet<DateFormat>? DateFormat { get; set; }
        public DbSet<TaxType>? TaxType { get; set; }
        public DbSet<PaymentMethod>? PaymentMethod { get; set; }
        public DbSet<PaymentTerm>? PaymentTerm { get; set; }
        public DbSet<CompanySetup>? SystemCompanyNumber { get; set; }
        public DbSet<BusinessSector>? BusinessSector { get; set; }

        /************************************************************************************************
         * Supplier Space
         ************************************************************************************************/
        public DbSet<Supplier>? Supplier { get; set; }
        public DbSet<SupplierNote>? SupplierNote { get; set; }
        public DbSet<SupplierAccountsContact>? SupplierAccountsContact { get; set; }
        public DbSet<SupplierAdjustment>? SupplierAdjustment { get; set; }
        public DbSet<SupplierCategory>? SupplierCategory { get; set; }
        public DbSet<SupplierContact>? SupplierContact { get; set; }
        public DbSet<SupplierSetup>? SupplierSetup { get; set; }

        /************************************************************************************************
         * Purchases Space
         ************************************************************************************************/
        public DbSet<PurchaseDocument>? PurchaseDocument { get; set; }
        public DbSet<PurchaseDocumentNote>? PurchaseDocumentNote { get; set; }
        public DbSet<PurchaseDocumentLine>? PurchaseDocumentLine { get; set; }
        public DbSet<PurchaseOrder>? PurchaseOrder { get; set; }
        public DbSet<PurchaseBuyer>? PurchaseBuyer { get; set; }
        public DbSet<PurchaseInvoice>? PurchaseInvoice { get; set; }
        public DbSet<PurchaseLine>? PurchaseLine { get; set; }
        public DbSet<PurchaseReturn>? PurchaseReturn { get; set; }
        public DbSet<PurchaseInvoiceReturn>? PurchaseReturnInvoice { get; set; }
        public DbSet<PurchaseReceipt>? PurchaseReceipt { get; set; }
        public DbSet<PurchaseOrderInvoice>? PurchaseOrderInvoice { get; set; }
        public DbSet<PurchaseInvoiceReceipt>? PurchaseInvoiceReceipt { get; set; }
        public DbSet<PurchaseDocumentSupplierDetails>? PurchaseDocumentSupplierDetails { get; set; }

        /************************************************************************************************
         * GENERAL SPACE
         ************************************************************************************************/
        public DbSet<Contact>? Contact { get; set; }
        public DbSet<Note>? Note { get; set; }
        public DbSet<VerificationRequest>? VerificationRequest { get; set; }

        protected override void OnModelCreating (ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ModelsBuilderAll.BuildModels(builder);
        }
    }
}
