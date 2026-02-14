using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.GeneralSpace;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SystemSpace;

namespace Librebooks.Areas.Companies.Services
{
    public interface ICompanyManager
    {
        /********************************************************************
         ** COMPANY GET TRANSACTIONS
         ********************************************************************/
        Task<Company[]> FindAllByUserAsync (int userId);
        Task<Company?> FindByIdAsync (int companyId);
        Task<Company?> FindByNumberAsync (string companyNumber);
        Task<CompanyRegionalSetup?> GetRegionalSettingsAsync (Company company);
        Task<TaxType?> GetSalesTaxTypeAsync (Company company);
        Task<CompanyMailSetup?> GetMailSettingsAsync (Company company);
        Task<IList<User>> GetUsersAsync (Company company);
        Task<IList<TaxType>> GetTaxTypesAsync (Company company);
        Task<IList<BankAccount>> GetBankAccountsAsync (Company company);
        Task<BankAccount?> GetDefaultBankAccountAsync (Company company);
        Task<TaxType?> FindTaxTypeByIdAsync (Company company, int id);
        Task<BankAccount?> FindBankAccountByIdAsync (Company company, int id);
        Task<Contact?> FindSalesPersonByIdAsync (Company company, int id);
        Task<Contact?> FindSalesPersonByUserIdAsync (Company company, int userId);

        /********************************************************************
         ** COMPANY CREATE TRANSACTIONS
         ********************************************************************/
        Task<TransactionResult<Company>> CreateAsync (Company company);
        Task<TransactionResult<CompanyUser>> AddUserAsync (Company company, User user, bool isSalesPerson = false);
        Task<TransactionResult<CompanyTaxType>> AddTaxTypeAsync (Company company, TaxType taxTypes);
        Task<TransactionResult<BankAccount>> AddBankAccountAsync (Company company, BankAccount bankAccount);
        Task<TransactionResult<BankAccount>> AddDefaultBankAccountAsync (Company company, BankAccount bankAccount);
        Task<TransactionResult<Contact>> AddSalesPersonAsync (Company company, Contact contact, CompanyUser? companyUser = null);
        Task<TransactionResult<CompanyImage>> AddLogoAsync (Company company, CompanyImage image);

        /********************************************************************
         ** COMPANY UPDATE TRANSACTIONS
         ********************************************************************/
        Task<TransactionResult<Company>> UpdateAsync (Company company);
        Task<TransactionResult<CompanyRegionalSetup>> UpdateRegionalSettingsAsync (CompanyRegionalSetup regionalSettings);
        Task<TransactionResult<CompanyMailSetup>> UpdateMailSettingsAsync (CompanyMailSetup mailSettings);
        Task<TransactionResult<CompanyDefaultTaxType>> UpdateDefaultSalesTaxTypeAsync (CompanyTaxType companyTaxType);
        Task<TransactionResult<BankAccount>> UpdateBankAccountAsync (BankAccount bankAccount);
        Task<TransactionResult<BankAccount>> UpdateDefaultBankAccountAsync (Company company, BankAccount bankAccount);
        Task<TransactionResult<SalesPerson>> UpdateSalesPersonAsync (SalesPerson salesPerson, Contact updatedContactInfo);
        Task<TransactionResult<CompanyLogo>> UpdateLogoAsync (CompanyLogo oldLogo, CompanyLogo newLogo);

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
