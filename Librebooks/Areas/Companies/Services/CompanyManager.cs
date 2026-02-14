using Librebooks.Core.EFCore;
using Librebooks.CoreLib.Operations;
using Librebooks.Data;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.GeneralSpace;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Companies.Services
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

        public async Task<Company[]> FindAllByUserAsync (int userId)
        => await context.CompanyUser!
                .Where(p => p.UserId == userId)
                .Include(p => p.Company)
                .Select(p => p.Company!)
                .ToArrayAsync();

        public async Task<Company?> FindByIdAsync (int id)
           => await store.FindByIdAsync(id);

        public async Task<Company?> FindByNumberAsync (string number)
            => await store.FindByNumberAsync(number);

        public async Task<CompanyRegionalSetup?> GetRegionalSettingsAsync (Company company)
            => await store.FindRegionalSettingsAsync(company.Id!);

        public async Task<TaxType?> GetSalesTaxTypeAsync (Company company)
            => await store.FindDefaultTaxTypeAsync(company.Id!);

        public async Task<CompanyMailSetup?> GetMailSettingsAsync (Company company)
            => await store.FindMailSettingsAsync(company.Id!);

        public async Task<IList<User>> GetUsersAsync (Company company)
            => await store.FindUsersAsync(company.Id!);

        public async Task<IList<TaxType>> GetTaxTypesAsync (Company company)
            => await store.FindTaxTypesAsync(company.Id!);

        public async Task<IList<BankAccount>> GetBankAccountsAsync (Company company)
            => await store.FindBankAccountsAsync(company.Id!);

        public async Task<TaxType?> FindTaxTypeByIdAsync (Company company, int taxTypeId)
            => await store.FindTaxTypeByIdAsync(company.Id!, taxTypeId);

        public async Task<Contact?> FindSalesPersonByIdAsync (Company company, int salesPersonId)
            => await store.FindSalesPersonByIdAsync(company.Id!, salesPersonId);

        public async Task<Contact?> FindSalesPersonByUserIdAsync (Company company, int userId)
            => await store.FindSalesPersonByUserIdAsync(company.Id!, userId);

        public async Task<BankAccount?> GetDefaultBankAccountAsync (Company company)
            => await store.FindDefaultBankAccountAsync(company.Id!);

        public async Task<BankAccount?> FindBankAccountByIdAsync (Company company, int bankAccountId)
            => await store.FindBankAccountByIdAsync(company.Id!, bankAccountId);

        /********************************************************************
         ** COMPANY CREATE TRANSACTIONS
         ********************************************************************/
        public async Task<TransactionResult<Company>> CreateAsync (Company company)
        {
            return await store.CreateAsync(company);
        }

        public async Task<TransactionResult> AddUserAsync (Company company, User user, bool isSalesPerson = false)
        {
            try
            {
                var result = await context.CompanyUser!.AddAsync(new CompanyUser(company.Id, user.Id));
                await context.SaveChangesAsync();

                if (isSalesPerson)
                {
                    var result2 = await AddSalesPersonAsync(company, new Contact
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email
                    }, result.Entity);

                    if (!result2.Succeeded)
                        return TransactionResult.Failure(TransactionError.Create(nameof(SalesPerson)));
                }

                return TransactionResult.Success;
            }
            catch (Exception) { }

            return TransactionResult.Failure(TransactionError.Create(nameof(CompanyUser)));
        }

        public async Task<TransactionResult<CompanyImage>> AddLogoAsync (Company company, CompanyImage image)
        {

            var result = await store.CreateLogoAsync(company, image);

            if (result.Succeeded)
                return TransactionResult<CompanyImage>.Success(result.Model);
            else
                return TransactionResult<CompanyImage>.Failure();
        }

        public Task<TransactionResult<CompanyTaxType>> AddTaxTypeAsync (Company company, TaxType taxTypes)
        {
            throw new NotImplementedException();
        }

        public async Task<TransactionResult<BankAccount>> AddBankAccountAsync (Company company, BankAccount bankAccount)
        {
            var result = await store.CreateBankAccountAsync(company, bankAccount);

            if (result.Succeeded)
                return TransactionResult<BankAccount>.Success(result.Model);
            else
                return TransactionResult<BankAccount>.Failure();
        }

        public async Task<TransactionResult<BankAccount>> AddDefaultBankAccountAsync (Company company, BankAccount bankAccount)
        {
            var result = await store.CreateDefaultBankAccountAsync(company, bankAccount);
            if (result.Succeeded)
                return TransactionResult<BankAccount>.Success(result.Model);
            else
                return TransactionResult<BankAccount>.Failure();
        }

        public async Task<TransactionResult<Contact>> AddSalesPersonAsync (Company company, Contact contact, CompanyUser? companyUser = null)
        {
            try
            {
                var result = await context.Contact!.AddAsync(contact);

                await context.SalesPerson!
                    .AddAsync(new SalesPerson(company.Id, contact.Id, companyUser?.Id));

                await context.SaveChangesAsync();

                return TransactionResult<Contact>.Success(result.Entity);
            }
            catch (Exception)
            {
                return TransactionResult<Contact>.Failure();
            }
        }

        /********************************************************************
         ** COMPANY UPDATE TRANSACTIONS
         ********************************************************************/
        public Task<TransactionResult<Company>> UpdateAsync (Company company)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<CompanyRegionalSetup>> UpdateRegionalSettingsAsync (CompanyRegionalSetup regionalSettings)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<CompanyMailSetup>> UpdateMailSettingsAsync (CompanyMailSetup mailSettings)
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

        public Task<TransactionResult<CompanyLogo>> UpdateLogoAsync (CompanyLogo oldLogo, CompanyLogo newLogo)
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

        Task<TransactionResult<CompanyUser>> ICompanyManager.AddUserAsync (Company company, User user, bool isSalesPerson)
        {
            throw new NotImplementedException();
        }
    }
}
