using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.DocumentSpace;
using Librebooks.Models.Entity.GeneralSpace;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Models.Entity.InventorySpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SupplierSpace;
using Librebooks.Models.Entity.SystemSpace;

namespace Librebooks.Areas.Companies.Services
{
	public interface ICompanyStore
	{
		/***********************************************************************************************************************************
         ****** SELECT TRANSACTIONS
         ***********************************************************************************************************************************/
		Task<Company?> FindByIdAsync (int companyId, CancellationToken cancellationToken = default);
		Task<Company?> FindByIdAsync (int companyId, int userId, CancellationToken cancellationToken = default);
		Task<IList<Company?>> FindByUserIdAsync (int userId, CancellationToken cancellationToken = default);
		Task<CompanyRegionalSetup?> FindRegionalSettingsAsync (int companyId, CancellationToken cancellationToken = default);
		Task<CompanyImage?> FindLogoAsync (int companyId, CancellationToken cancellationToken = default);
		Task<IList<Tax>> FindTaxTypesAsync (int companyId, CancellationToken cancellationToken = default);
		Task<Tax?> FindTaxByIdAsync (int companyId, int taxTypeId, CancellationToken cancellationToken = default);
		Task<Tax?> FindDefaultTaxAsync (int companyId, CancellationToken cancellationToken = default);
		Task<CompanyMailSetup?> FindMailSettingsAsync (int companyId, CancellationToken cancellationToken = default);
		Task<BankAccount?> FindDefaultBankAccountAsync (int companyId, CancellationToken cancellationToken = default);
		Task<BankAccount?> FindBankAccountByIdAsync (int companyId, int bankAccountId, CancellationToken cancellationToken = default);
		Task<Contact?> FindSalesPersonByIdAsync (int companyId, int salesPersonId, CancellationToken cancellationToken = default);
		Task<Contact?> FindSalesPersonByUserIdAsync (int companyId, int userId, CancellationToken cancellationToken = default);
		Task<IList<User>> FindUsersAsync (int companyId, CancellationToken cancellationToken = default);

		/***********************************************************************************************************************************
         ****** INSERT TRANSACTIONS
         ***********************************************************************************************************************************/
		Task<Result<Tax>> CreateTaxTypeAsync (Company company, Tax taxType);
		Task<Result<Contact>> CreateSalesPersonAsync (Company company, Contact contact);
		Task<Result<BankAccount>> CreateBankAccountAsync (Company company, BankAccount bankAccount);
		Task<Result<CompanyImage>> CreateLogoAsync (Company company, CompanyImage image);
		Task<Result<Company>> CreateAsync (Company company);

		/***********************************************************************************************************************************
         ****** UPDATE TRANSACTIONS
         ***********************************************************************************************************************************/
		Task<Result<CompanyRegionalSetup>> UpdateRegionalSettingsAsync (CompanyRegionalSetup regionalSettings, CancellationToken cancellationToken = default);
		Task<Result<Tax>> UpdateTaxAsync (Tax tax, CancellationToken cancellationToken = default);
		Task<Result<CompanyImage>> UpdateLogoAsync (CompanyImage companyImage, CancellationToken cancellationToken = default);
		Task<Result> UpdateMailSettingsAsync (CompanyMailSetup mailSettings, CancellationToken cancellationToken = default);
		Task<Result<SupplierSetup>> UpdateSupplierSetupAsync (SupplierSetup supplierSetup, CancellationToken cancellationToken = default);
		Task<Result<CustomerSetup>> UpdateCustomerSetupAsync (CustomerSetup customerSetup, CancellationToken cancellationToken = default);
		Task<Result<ItemSetup>> UpdateItemSetupAsync (ItemSetup itemSetup, CancellationToken cancellationToken = default);
		Task<Result<DocumentSetup>> UpdateDocumentSetupAsync (DocumentSetup documentSetup, CancellationToken cancellationToken = default);
		Task<Result<BankAccount>> UpdateBankAccountAsync (BankAccount bankAccount, CancellationToken cancellationToken = default);
		Task<Result<Company>> UpdateAsync (Company company, CancellationToken cancellation = default);

		/***********************************************************************************************************************************
         ****** DELETE TRANSACTIONS
         ***********************************************************************************************************************************/
		Task<Result> DeleteSalesPersonAsync (SalesPerson salesPerson);
		Task<Result> DeleteAsync (Company company);
		Task<Result> DeleteTaxTypeAsync (CompanyTax companyTaxType);
		Task<Result> DeleteBankAccountAsync (BankAccount bankAccount);
		Task<Result> DeleteLogoAsync (CompanyLogo companyLogo);
	}
}
