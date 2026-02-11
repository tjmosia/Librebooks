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
            SalesDocumentLine.OnModelCreating(builder);
            SalesInvoice.OnModelCreating(builder);
            SalesReceipt.OnModelCreating(builder);
            SalesOrder.OnModelCreating(builder);
            SalesOrderInvoice.OnModelCreating(builder);
            SalesQuote.OnModelCreating(builder);
            SalesQuoteOrder.OnModelCreating(builder);
            SalesQuoteInvoice.OnModelCreating(builder);
            SalesInvoiceReceipt.OnModelCreating(builder);
            SalesLine.OnModelCreating(builder);
            SalesDocumentCustomerDetails.OnModelCreating(builder);
            SalesDocumentCompanyDetails.OnModelCreating(builder);
            SalesPerson.OnModelCreating(builder);
            SalesInvoiceCredit.OnModelCreating(builder);
            SalesDocumentNote.OnModelCreating(builder);


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
            ShippingMethod.OnModelCreating(builder);
            ShippingTerm.OnModelCreating(builder);
            PaymentMethod.OnModelCreating(builder);
            CompanySetup.OnModelCreating(builder);
            BusinessSector.BuildModel(builder);

            /************************************************************************************************
             * Customer Space
             ************************************************************************************************/
            Customer.OnModelCreating(builder);
            CustomerCategory.OnModelCreating(builder);
            CustomerNote.OnModelCreating(builder);
            CustomerContact.BuildModel(builder);
            CustomerAccountsContact.OnModelCreating(builder);
            CustomerWriteOff.OnModelCreating(builder);
            CustomerAdjustment.OnModelCreating(builder);
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
            Supplier.OnModelCreating(builder);
            SupplierNote.OnModelCreating(builder);
            SupplierAdjustment.OnModelCreating(builder);
            SupplierContact.OnModelCreating(builder);
            SupplierAccountsContact.BuildModel(builder);
            SupplierCategory.OnModelCreating(builder);
            SupplierSetup.OnModelCreating(builder);

            /************************************************************************************************
             * Purchasing Space
             ************************************************************************************************/
            PurchaseDocument.OnModelCreating(builder);
            PurchaseDocumentLine.OnModelCreating(builder);
            PurchaseInvoice.OnModelCreating(builder);
            PurchaseOrder.OnModelCreating(builder);
            PurchaseOrderInvoice.OnModelCreating(builder);
            PurchaseReceipt.OnModelCreating(builder);
            PurchaseInvoiceReceipt.BuildModel(builder);
            PurchaseReturn.BuildModel(builder);
            PurchaseLine.OnModelCreating(builder);
            PurchaseBuyer.OnModelCreating(builder);
            PurchaseInvoiceReturn.BuildModel(builder);
            PurchaseDocumentNote.OnModelCreating(builder);
            PurchaseDocumentSupplierDetails.OnModelCreating(builder);
            PurchaseRequestForQuote.OnModelCreating(builder);

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
