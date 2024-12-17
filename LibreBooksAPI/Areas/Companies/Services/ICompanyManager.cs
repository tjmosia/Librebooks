using LibreBooks.CoreLib.Operations;
using LibreBooks.Models.Entity.BankingSpace;
using LibreBooks.Models.Entity.CompanySpace;
using LibreBooks.Models.Entity.GeneralSpace;
using LibreBooks.Models.Entity.IdentitySpace;
using LibreBooks.Models.Entity.SalesSpace;
using LibreBooks.Models.Entity.SystemSpace;

using LibreBooksAPI.Models.Entity.CustomerSpace;
using LibreBooksAPI.Models.Entity.SupplierSpace;

namespace LibreBooks.Areas.Companies.Services
{
    public interface ICompanyManager
    {
        /********************************************************************
         ** COMPANY GET TRANSACTIONS
         ********************************************************************/
        Task<Company?> FindByIdAsync (string companyId);
        Task<Company?> FindByNumberAsync (string companyNumber);
        Task<CompanyRegionalSettings?> GetRegionalSettingsAsync (Company company);
        Task<TaxType?> GetSalesTaxTypeAsync (Company company);
        Task<CompanyMailSettings?> GetMailSettingsAsync (Company company);
        Task<IList<User>> GetUsersAsync (Company company);
        Task<IList<TaxType>> GetTaxTypesAsync (Company company);
        Task<IList<BankAccount>> GetBankAccountsAsync (Company company);
        Task<BankAccount?> GetDefaultBankAccountAsync (Company company);
        Task<TaxType?> FindTaxTypeByIdAsync (Company company, string id);
        Task<BankAccount?> FindBankAccountByIdAsync (Company company, string id);
        Task<Contact?> FindSalesPersonByIdAsync (Company company, string id);
        Task<Contact?> FindSalesPersonByUserIdAsync (Company company, string userId);

        /********************************************************************
         ** COMPANY CREATE TRANSACTIONS
         ********************************************************************/
        Task<TransactionResult<Company>> CreateAsync (Company company, User user, CompanyRegionalSettings regionalSettings, TaxType[] taxTypes,
            TaxType defaultTaxType, CustomerSetup customerSetup, SupplierSetup supplierSetup);
        Task<TransactionResult<CompanyUser>> AddUserAsync (Company company, User user, bool isSalesPerson = false);
        Task<TransactionResult<CompanyLogo>> AddLogoAsync (Company company, CompanyLogo logo);
        Task<TransactionResult<CompanyTaxType>> AddTaxTypeAsync (Company company, TaxType taxTypes);
        Task<TransactionResult<BankAccount>> AddBankAccountAsync (Company company, BankAccount bankAccount);
        Task<TransactionResult<CompanyDefaultBankAccount>> AddDefaultBankAccountAsync (Company company, BankAccount bankAccount);
        Task<TransactionResult<Contact>> AddSalesPersonAsync (Company company, Contact contact, CompanyUser? companyUser = null);

        /********************************************************************
         ** COMPANY UPDATE TRANSACTIONS
         ********************************************************************/
        Task<TransactionResult<Company>> UpdateAsync (Company company);
        Task<TransactionResult<CompanyRegionalSettings>> UpdateRegionalSettingsAsync (CompanyRegionalSettings regionalSettings);
        Task<TransactionResult<CompanyMailSettings>> UpdateMailSettingsAsync (CompanyMailSettings mailSettings);
        Task<TransactionResult<CompanyDefaultTaxType>> UpdateDefaultSalesTaxTypeAsync (CompanyTaxType companyTaxType);
        Task<TransactionResult<BankAccount>> UpdateBankAccountAsync (BankAccount bankAccount);
        Task<TransactionResult<BankAccount>> UpdateDefaultBankAccountAsync (Company company, BankAccount bankAccount);
        Task<TransactionResult<SalesPerson>> UpdateSalesPersonAsync (SalesPerson salesPerson, Contact updatedContactInfo);

        /********************************************************************
         ** COMPANY DELETE TRANSACTIONS
         ********************************************************************/
        Task<TransactionResult<Company>> DeleteAsync (Company company);
        Task<TransactionResult> DeleteUserAsync (CompanyUser companyUser);
        Task<TransactionResult> DeleteTaxTypeAsync (CompanyTaxType companyTaxType);
        Task<TransactionResult> DeleteBankAccountAsync (BankAccount bankAccount);
        Task<TransactionResult> DeleteSalesPersonAsync (BankAccount bankAccount);
    }
}
