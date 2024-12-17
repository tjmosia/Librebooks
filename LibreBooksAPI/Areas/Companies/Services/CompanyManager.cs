using LibreBooks.CoreLib.Operations;
using LibreBooks.Data;
using LibreBooks.Models.Entity.BankingSpace;
using LibreBooks.Models.Entity.CompanySpace;
using LibreBooks.Models.Entity.GeneralSpace;
using LibreBooks.Models.Entity.IdentitySpace;
using LibreBooks.Models.Entity.SalesSpace;
using LibreBooks.Models.Entity.SystemSpace;

using LibreBooksAPI.Areas.Companies.Services;
using LibreBooksAPI.Core.EFCore;
using LibreBooksAPI.Models.Entity.CustomerSpace;
using LibreBooksAPI.Models.Entity.SupplierSpace;

namespace LibreBooks.Areas.Companies.Services
{
    public class CompanyManager : ICompanyManager
    {
        private readonly CompanyStore store;
        private readonly AppDbContext context;
        private readonly AppErrorDescriber errorDescriber;
        private readonly ILogger<CompanyManager> logger;

        public CompanyManager (CompanyStore store, AppDbContext context, AppErrorDescriber errorDescriber, ILogger<CompanyManager> logger)
        {
            this.store = store;
            this.errorDescriber = errorDescriber;
            this.context = context;
            this.logger = logger;
        }

        /********************************************************************
         ** COMPANY GET TRANSACTIONS
         ********************************************************************/
        public Task<Company?> FindByIdAsync (string id)
           => store.FindByIdAsync(id);

        public Task<Company?> FindByNumberAsync (string number)
            => store.FindByNumberAsync(number);

        public async Task<CompanyRegionalSettings?> GetRegionalSettingsAsync (Company company)
            => await store.FindCompanyRegionalSettingsByIdAsync(company.Id!);

        public async Task<TaxType?> GetSalesTaxTypeAsync (Company company)
            => await store.FindDefaultTaxTypeByIdAsync(company.Id!);

        public async Task<CompanyMailSettings?> GetMailSettingsAsync (Company company)
            => await store.FindMailSettingsByIdAsync(company.Id!);

        public async Task<IList<User>> GetUsersAsync (Company company)
            => await store.FindUsersAsync(company.Id!);

        public async Task<IList<TaxType>> GetTaxTypesAsync (Company company)
            => await store.FindTaxTypesAsync(company.Id!);

        public async Task<IList<BankAccount>> GetBankAccountsAsync (Company company)
            => await store.FindBankAccountsAsync(company.Id!);

        public async Task<BankAccount?> GetDefaultBankAccountAsync (Company company)
            => await store.FindDefaultBankAccountByIdAsync(company.Id!);

        public async Task<TaxType?> FindTaxTypeByIdAsync (Company company, string taxTypeId)
            => await store.FindTaxTypeByIdAsync(company.Id!, taxTypeId);

        public async Task<BankAccount?> FindBankAccountByIdAsync (Company company, string bankAccountId)
            => await store.FindBankAccountByIdAsync(company.Id!, bankAccountId);

        public async Task<Contact?> FindSalesPersonByIdAsync (Company company, string salesPersonId)
            => await store.FindSalesPersonByIdAsync(company.Id!, salesPersonId);

        public async Task<Contact?> FindSalesPersonByUserIdAsync (Company company, string userId)
            => await store.FindSalesPersonByUserIdAsync(company.Id!, userId);

        /********************************************************************
         ** COMPANY CREATE TRANSACTIONS
         ********************************************************************/
        public async Task<TransactionResult<Company>> CreateAsync (Company company,
            User user,
            CompanyRegionalSettings regionalSettings,
            TaxType[] taxTypes,
            TaxType defaultTaxType,
            CustomerSetup customerSetup,
            SupplierSetup supplierSetup)
        {
            var currentCompany = await store.FindByIdAsync(company.Id);

            if (currentCompany != null)
                return TransactionResult<Company>.Failure(errorDescriber.DuplicateKey());

            var newCompany = CompanyBuilder
                .Begin(company)
                .AddUser(user)
                .AddRegionalSettings(regionalSettings)
                .AddTaxTypes(taxTypes)
                .AddDefaultTaxType(defaultTaxType)
                .AddCustomerSetup(customerSetup)
                .AddSupplerSetup(supplierSetup)
                .Build();

            return await store.CreateAsync(newCompany);
        }

        public Task<TransactionResult<CompanyUser>> AddUserAsync (Company company, User user, bool isSalesPerson = false)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<CompanyLogo>> AddLogoAsync (Company company, CompanyLogo logo)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<CompanyTaxType>> AddTaxTypeAsync (Company company, TaxType taxTypes)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<BankAccount>> AddBankAccountAsync (Company company, BankAccount bankAccount)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<CompanyDefaultBankAccount>> AddDefaultBankAccountAsync (Company company, BankAccount bankAccount)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<Contact>> AddSalesPersonAsync (Company company, Contact contact, CompanyUser? companyUser = null)
        {
            throw new NotImplementedException();
        }

        /********************************************************************
         ** COMPANY UPDATE TRANSACTIONS
         ********************************************************************/
        public Task<TransactionResult<Company>> UpdateAsync (Company company)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<CompanyRegionalSettings>> UpdateRegionalSettingsAsync (CompanyRegionalSettings regionalSettings)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<CompanyMailSettings>> UpdateMailSettingsAsync (CompanyMailSettings mailSettings)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<CompanyDefaultTaxType>> UpdateDefaultSalesTaxTypeAsync (CompanyTaxType companyTaxType)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<BankAccount>> UpdateBankAccountAsync (BankAccount bankAccount)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<BankAccount>> UpdateDefaultBankAccountAsync (Company company, BankAccount bankAccount)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<SalesPerson>> UpdateSalesPersonAsync (SalesPerson salesPerson, Contact updatedContactInfo)
        {
            throw new NotImplementedException();
        }

        /********************************************************************
         ** COMPANY DELETE TRANSACTIONS
         ********************************************************************/
        public Task<TransactionResult<Company>> DeleteAsync (Company company)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> DeleteUserAsync (CompanyUser companyUser)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> DeleteTaxTypeAsync (CompanyTaxType companyTaxType)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> DeleteBankAccountAsync (BankAccount bankAccount)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> DeleteSalesPersonAsync (BankAccount bankAccount)
        {
            throw new NotImplementedException();
        }
    }
}
