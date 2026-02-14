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
             * User Space
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
			ItemInfo.OnModelCreating(builder);

			/************************************************************************************************
             * Document Space
             ************************************************************************************************/
			DocumentCompanyInfo.OnModelCreating(builder);
			DocumentPrintTemplate.OnModelCreating(builder);
			DocumentSetup.OnModelCreating(builder);
			DocumentStatus.OnModelCreating(builder);

			/************************************************************************************************
             * Sales Space
             ************************************************************************************************/
			DocumentCustomerInfo.OnModelCreating(builder);
			SalesCredit.OnModelCreating(builder);
			SalesDocument.OnModelCreating(builder);
			SalesDocumentLine.OnModelCreating(builder);
			SalesDocumentNote.OnModelCreating(builder);
			SalesInvoice.OnModelCreating(builder);
			SalesInvoiceCredit.OnModelCreating(builder);
			SalesInvoiceReceipt.OnModelCreating(builder);
			SalesInvoiceWriteoff.OnModelCreating(builder);
			SalesLine.OnModelCreating(builder);
			SalesOrder.OnModelCreating(builder);
			SalesOrderInvoice.OnModelCreating(builder);
			SalesPerson.OnModelCreating(builder);
			SalesQuote.OnModelCreating(builder);
			SalesQuoteOrder.OnModelCreating(builder);
			SalesReceipt.OnModelCreating(builder);


			/************************************************************************************************
             * Company Space
             ************************************************************************************************/
			Company.OnModelCreating(builder);
			CompanyDefaultBankAccount.OnModelCreating(builder);
			CompanyDefaultTaxType.OnModelCreating(builder);
			CompanyImage.OnModelCreating(builder);
			CompanyLogo.OnModelCreating(builder);
			CompanyMailSetup.OnModelCreating(builder);
			CompanyRegionalSetup.OnModelCreating(builder);
			CompanySetup.OnModelCreating(builder);
			CompanyTaxType.BuildModel(builder);
			CompanyUser.OnModelCreating(builder);

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
			BusinessSector.OnModelCreating(builder);

			/************************************************************************************************
             * Customer Space
             ************************************************************************************************/
			Customer.OnModelCreating(builder);
			CustomerAccountsContact.OnModelCreating(builder);
			CustomerAdjustment.OnModelCreating(builder);
			CustomerCategory.OnModelCreating(builder);
			CustomerContact.BuildModel(builder);
			CustomerNote.OnModelCreating(builder);
			CustomerSetup.OnModelCreating(builder);
			CustomerWriteOff.OnModelCreating(builder);

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
			SupplierAccountsContact.OnModelCreating(builder);
			SupplierCategory.OnModelCreating(builder);
			SupplierSetup.OnModelCreating(builder);

			/************************************************************************************************
             * Purchasing Space
             ************************************************************************************************/
			DocumentSupplierInfo.OnModelCreating(builder);
			PurchaseBuyer.OnModelCreating(builder);
			PurchaseDocument.OnModelCreating(builder);
			PurchaseDocumentLine.OnModelCreating(builder);
			PurchaseDocumentNote.OnModelCreating(builder);
			PurchaseInvoice.OnModelCreating(builder);
			PurchaseInvoiceReceipt.OnModelCreating(builder);
			PurchaseInvoiceReturn.OnModelCreating(builder);
			PurchaseLine.OnModelCreating(builder);
			PurchaseOrder.OnModelCreating(builder);
			PurchaseOrderInvoice.OnModelCreating(builder);
			PurchaseReceipt.OnModelCreating(builder);
			PurchaseRequestForQuote.OnModelCreating(builder);
			PurchaseReturn.OnModelCreating(builder);

			/************************************************************************************************
             * Banking Space
             ************************************************************************************************/
			BankAccount.OnModelCreating(builder);
			BankAccountCategory.OnModelCreating(builder);

			/************************************************************************************************
             * General Space
             ************************************************************************************************/
			Contact.OnModelCreating(builder);
			Note.OnModelCreating(builder);
			VerificationRequest.OnModelCreating(builder);
		}
	}
}
