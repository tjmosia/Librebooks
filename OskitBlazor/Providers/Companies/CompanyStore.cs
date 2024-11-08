using OskitBlazor.Core.EFCore;
using OskitBlazor.Data;
using OskitBlazor.Models.Entity.CompanySpace;
using OskitBlazor.Models.Entity.GeneralSpace;
using OskitBlazor.Models.Entity.IdentitySpace;
using OskitBlazor.Models.Entity.SalesSpace;

namespace OskitBlazor.Providers.Companies
{
    public class CompanyStore
        (AppDbContext? context, ILogger<CompanyStore>? logger)
        : DbStoreBase(context, logger), ICompanyStore
    {

        private void ThrowIfDisposed()
        {
            if (context != null)
                throw new ObjectDisposedException(nameof(context));
        }

        public Task<Company?> FindByIdAsync(string companyId, User user)
        {
            throw new NotImplementedException();
        }

        public Task<Company?> FindByNumberAsync(string companyNumber, User user)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Company>> FindAllAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<Company?> CreateAsync(Company company, User user)
        {
            throw new NotImplementedException();
        }

        public Task<Company?> UpdateAsync(Company company)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Company company, User user)
        {
            throw new NotImplementedException();
        }

        public Task<CompanyUser?> CreateUserAsync(Company company, User user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveUserAsync(Company company, User user)
        {
            throw new NotImplementedException();
        }

        public Task<IList<CompanyUser>> FindUsersByCompanyIdAsync(string companyId)
        {
            throw new NotImplementedException();
        }

        public Task<SalesPerson?> CreateSalesPersonAsync(Company company, Contact contact, CompanyUser? companyUser = null)
        {
            throw new NotImplementedException();
        }

        public Task<SalesPerson?> FindSalesPersonByIdAsync(string salesPersonId, Company company)
        {
            throw new NotImplementedException();
        }

        public Task<SalesPerson?> UpdateSalesPersonAsync(Company company, Contact contact, CompanyUser? companyUser = null)
        {
            throw new NotImplementedException();
        }

        public Task RemoveSalesPersonAsync(SalesPerson salesPerson)
        {
            throw new NotImplementedException();
        }

        public Task<CompanyRegionalSettings?> CreateRegionalSettingsAsync(Company company, CompanyRegionalSettings regionalSettings)
        {
            throw new NotImplementedException();
        }

        public Task<CompanyRegionalSettings?> FindRegionalSettingsByCompanyIdAsync(string companyId)
        {
            throw new NotImplementedException();
        }

        public Task<CompanyRegionalSettings?> UpdateRegionalSettingsAsync(CompanyRegionalSettings regionalSettings)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRegionalSettingsAsync(CompanyRegionalSettings regionalSettings)
        {
            throw new NotImplementedException();
        }

        public Task<CompanyMailSettings?> CreateMailSettingsAsync(Company company, CompanyMailSettings mailSettings)
        {
            throw new NotImplementedException();
        }

        public Task<CompanyMailSettings?> UpdateMailSettingsAsync(CompanyMailSettings mailSettings)
        {
            throw new NotImplementedException();
        }

        public Task<CompanyMailSettings?> FindMailSettingsByIdAsync(string mailSettingsId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<CompanyMailSettings>> FindAllMailSettingsByCompanyIdAsync(string companyId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveMailSettingsAsync(CompanyMailSettings mailSettings)
        {
            throw new NotImplementedException();
        }
    }
}
