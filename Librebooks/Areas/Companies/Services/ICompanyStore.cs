using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.DocumentSpace;
using Librebooks.Models.Entity.GeneralSpace;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SystemSpace;

using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.InventorySpace;
using Librebooks.Models.Entity.SupplierSpace;

namespace Librebooks.Areas.Companies.Services
{
    public interface ICompanyStore
    {
        /***********************************************************************************************************************************
         ****** SELECT TRANSACTIONS
         ***********************************************************************************************************************************/
        Task<Company?> FindByIdAsync (string companyId);
        Task<Company?> FindByNumberAsync (string companyNumber);
        Task<CompanyRegionalSettings?> FindRegionalSettingsAsync (string companyId);
        Task<CompanyLogo?> FindLogoAsync (string companyId);
        Task<IList<TaxType>> FindTaxTypesAsync (string companyId);
        Task<TaxType?> FindTaxTypeByIdAsync (string companyId, string taxTypeId);
        Task<TaxType?> FindDefaultTaxTypeAsync (string companyId);
        Task<CompanyMailSettings?> FindMailSettingsAsync (string companyId);
        Task<BankAccount?> FindDefaultBankAccountAsync (string companyId);
        Task<BankAccount?> FindBankAccountByIdAsync (string companyId, string bankAccountId);
        Task<Contact?> FindSalesPersonByIdAsync (string companyId, string salesPersonId);
        Task<Contact?> FindSalesPersonByUserIdAsync (string companyId, string userId);
        Task<IList<User>> FindUsersAsync (string companyId);

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
        Task<TransactionResult<CompanyRegionalSettings>> UpdateRegionalSettingsAsync (CompanyRegionalSettings regionalSettings);
        Task<TransactionResult<CompanyDefaultBankAccount>> UpdateDefaultTaxTypeAsync (CompanyDefaultTaxType defaultTaxType);
        Task<TransactionResult<TaxType>> UpdateTaxTypeAsync (TaxType taxType);
        Task<TransactionResult<CompanyImage>> UpdateLogoAsync (CompanyImage companyImage);
        Task<TransactionResult> UpdateMailSettingsAsync (CompanyMailSettings mailSettings);
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
