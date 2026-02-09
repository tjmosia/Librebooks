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

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity
{
    public class ModelsBuilderAll
    {
        public static void BuildModels (ModelBuilder builder)
        {
            /************************************************************************************************
             * Uer Space
             ************************************************************************************************/
            User.OnModelCreating(builder);
            Role.OnModelCreating(builder);
            RoleClaim.OnModelCreating(builder);
            UserRole.OnModelCreating(builder);
            UserToken.OnModelCreating(builder);
            UserLogin.OnModelCreating(builder);
            UserClaim.OnModelCreating(builder);

            /************************************************************************************************
             * Inventory Space
             ************************************************************************************************/
            Item.OnModelCreating(builder);
            ItemCategory.OnModelCreating(builder);
            ItemInventory.OnModelCreating(builder);
            ItemAdjustment.OnModelCreating(builder);
            ItemSetup.OnModelCreating(builder);
            ItemDetail.OnModelCreating(builder);

            /************************************************************************************************
             * Document Space
             ************************************************************************************************/
            DocumentStatus.OnModelCreating(builder);
            DocumentSetup.OnModelCreating(builder);
            DocumentPrintTemplate.OnModelCreating(builder);

            /************************************************************************************************
             * Sales Space
             ************************************************************************************************/
            SalesDocument.OnModelCreating(builder);
            SalesCredit.OnModelCreating(builder);
            SalesDocumentLine.BuildModel(builder);
            SalesInvoice.OnModelCreating(builder);
            SalesReceipt.BuildModel(builder);
            SalesOrder.BuildModel(builder);
            SalesOrderInvoice.BuildModel(builder);
            SalesQuote.BuildModel(builder);
            SalesQuoteOrder.BuildModel(builder);
            SalesInvoiceReceipt.BuildModel(builder);
            SalesLine.BuildModel(builder);
            SalesDocumentCustomerDetails.BuildModel(builder);
            SalesDocumentCompanyDetails.OnModelCreating(builder);
            SalesPerson.BuildModel(builder);
            SalesInvoiceCredit.BuildModel(builder);
            SalesDocumentNote.BuildModel(builder);


            /************************************************************************************************
             * Company Space
             ************************************************************************************************/
            Company.OnModelCreating(builder);
            CompanyDefaultBankAccount.OnModelCreating(builder);
            CompanyDefaultTaxType.OnModelCreating(builder);
            CompanyTaxType.BuildModel(builder);
            CompanyUser.OnModelCreating(builder);
            CompanyMailSettings.OnModelCreating(builder);
            CompanyRegionalSettings.OnModelCreating(builder);
            CompanyLogo.OnModelCreating(builder);
            CompanyImage.OnModelCreating(builder);

            /************************************************************************************************
             * System Space
             ************************************************************************************************/
            Country.OnModelCreating(builder);
            Currency.OnModelCreating(builder);
            DateFormat.OnModelCreating(builder);
            TaxType.OnModelCreating(builder);
            ShippingMethod.BuildModel(builder);
            ShippingTerm.OnModelCreating(builder);
            PaymentMethod.OnModelCreating(builder);
            CompanySetup.OnModelCreating(builder);
            BusinessSector.BuildModel(builder);

            /************************************************************************************************
             * Customer Space
             ************************************************************************************************/
            Customer.OnModelCreating(builder);
            CustomerCategory.BuildModel(builder);
            CustomerNote.OnModelCreating(builder);
            CustomerContact.BuildModel(builder);
            CustomerAccountsContact.OnModelCreating(builder);
            CustomerWriteOff.OnModelCreating(builder);
            CustomerAdjustment.BuildModel(builder);
            CustomerSetup.OnModelCreating(builder);

            /************************************************************************************************
             * Accounting Space
             ************************************************************************************************/
            Account.OnModelCreating(builder);
            AccountCategory.OnModelCreating(builder);
            AccountCashFlowType.OnModelCreating(builder);
            Journal.OnModelCreating(builder);
            JournalNote.OnModelCreating(builder);

            /************************************************************************************************
             * Supplier Space
             ************************************************************************************************/
            Supplier.BuildModel(builder);
            SupplierNote.BuildModel(builder);
            SupplierAdjustment.BuildModel(builder);
            SupplierContact.BuildModel(builder);
            SupplierAccountsContact.BuildModel(builder);
            SupplierCategory.BuildModel(builder);
            SupplierSetup.BuildModel(builder);

            /************************************************************************************************
             * Purchasing Space
             ************************************************************************************************/
            PurchaseDocument.BuildModel(builder);
            PurchaseDocumentLine.BuildModel(builder);
            PurchaseInvoice.BuildModel(builder);
            PurchaseOrder.BuildModel(builder);
            PurchaseOrderInvoice.BuildModel(builder);
            PurchaseReceipt.BuildModel(builder);
            PurchaseInvoiceReceipt.BuildModel(builder);
            PurchaseReturn.BuildModel(builder);
            PurchaseLine.BuildModel(builder);
            PurchaseBuyer.BuildModel(builder);
            PurchaseInvoiceReturn.BuildModel(builder);
            PurchaseDocumentNote.BuildModel(builder);
            PurchaseDocumentSupplierDetails.BuildModel(builder);

            /************************************************************************************************
             * Banking Space
             ************************************************************************************************/
            BankAccount.OnModelCreating(builder);
            BankAccountCategory.OnModelCreating(builder);
            VerificationRequest.OnModelCreating(builder);

            /************************************************************************************************
             * General Space
             ************************************************************************************************/
            Contact.OnModelCreating(builder);
            Note.OnModelCreating(builder);
        }
    }
}
