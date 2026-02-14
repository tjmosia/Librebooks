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
        Task<Company?> FindByIdAsync (int companyId);
        Task<Company?> FindByNumberAsync (string companyNumber);
        Task<CompanyRegionalSetup?> FindRegionalSettingsAsync (int companyId);
        Task<CompanyLogo?> FindLogoAsync (int companyId);
        Task<IList<TaxType>> FindTaxTypesAsync (int companyId);
        Task<TaxType?> FindTaxTypeByIdAsync (int companyId, int taxTypeId);
        Task<TaxType?> FindDefaultTaxTypeAsync (int companyId);
        Task<CompanyMailSetup?> FindMailSettingsAsync (int companyId);
        Task<BankAccount?> FindDefaultBankAccountAsync (int companyId);
        Task<BankAccount?> FindBankAccountByIdAsync (int companyId, int bankAccountId);
        Task<Contact?> FindSalesPersonByIdAsync (int companyId, int salesPersonId);
        Task<Contact?> FindSalesPersonByUserIdAsync (int companyId, int userId);
        Task<IList<User>> FindUsersAsync (int companyId);

        /***********************************************************************************************************************************
         ****** INSERT TRANSACTIONS
         ***********************************************************************************************************************************/
        Task<TransactionResult<Company>> CreateAsync (Company company);
        Task<TransactionResult<TaxType>> CreateTaxTypeAsync (Company company, TaxType taxType);
        Task<TransactionResult<Contact>> CreateSalesPersonAsync (Company company, Contact contact);
        Task<TransactionResult<BankAccount>> CreateBankAccountAsync (Company company, BankAccount bankAccount);
        Task<TransactionResult<CompanyImage>> CreateLogoAsync (Company company, CompanyImage image);

        /***********************************************************************************************************************************
         ****** UPDATE TRANSACTIONS
         ***********************************************************************************************************************************/
        Task<TransactionResult<CompanyRegionalSetup>> UpdateRegionalSettingsAsync (CompanyRegionalSetup regionalSettings);
        Task<TransactionResult<CompanyDefaultBankAccount>> UpdateDefaultTaxTypeAsync (CompanyDefaultTaxType defaultTaxType);
        Task<TransactionResult<TaxType>> UpdateTaxTypeAsync (TaxType taxType);
        Task<TransactionResult<CompanyImage>> UpdateLogoAsync (CompanyImage companyImage);
        Task<TransactionResult> UpdateMailSettingsAsync (CompanyMailSetup mailSettings);
        Task<TransactionResult<SupplierSetup>> UpdateSupplierSetupAsync (SupplierSetup supplierSetup);
        Task<TransactionResult<CustomerSetup>> UpdateCustomerSetupAsync (CustomerSetup customerSetup);
        Task<TransactionResult<ItemSetup>> UpdateItemSetupAsync (ItemSetup itemSetup);
        Task<TransactionResult<DocumentSetup>> UpdateDocumentSetupAsync (DocumentSetup documentSetup);
        Task<TransactionResult<BankAccount>> UpdateBankAccountAsync (BankAccount bankAccount);

        /***********************************************************************************************************************************
         ****** DELETE TRANSACTIONS
         ***********************************************************************************************************************************/
        Task<TransactionResult> DeleteSalesPersonAsync (SalesPerson salesPerson);
        Task<TransactionResult> DeleteAsync (Company company);
        Task<TransactionResult> DeleteTaxTypeAsync (CompanyTaxType companyTaxType);
        Task<TransactionResult> DeleteBankAccountAsync (BankAccount bankAccount);
        Task<TransactionResult> DeleteLogoAsync (CompanyLogo companyLogo);
    }
}
