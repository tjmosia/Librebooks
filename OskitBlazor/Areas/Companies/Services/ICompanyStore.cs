using OskitBlazor.Models.Entity.CompanySpace;
using OskitBlazor.Models.Entity.GeneralSpace;
using OskitBlazor.Models.Entity.IdentitySpace;
using OskitBlazor.Models.Entity.SalesSpace;

namespace OskitBlazor.Areas.Companies.Services
{
    public interface ICompanyStore
    {
        /***********************************************************************************
         * COMPANY CRUD Transactions
         **********************************************************************************/
        Task<Company?> FindByIdAsync (string companyId, User user);
        Task<Company?> FindByNumberAsync (string companyNumber, User user);
        Task<IList<Company>> FindAllAsync (User user);
        Task<Company?> CreateAsync (Company company, User user);
        Task<Company?> UpdateAsync (Company company);
        Task RemoveAsync (Company company, User user);

        /***********************************************************************************
         * COMPANY USER Transactions
         **********************************************************************************/
        Task<CompanyUser?> CreateUserAsync (Company company, User user);
        Task RemoveUserAsync (Company company, User user);
        Task<IList<CompanyUser>> FindUsersByCompanyIdAsync (string companyId);

        /***********************************************************************************
         * COMPANY SALES PERSON Transactions
         **********************************************************************************/
        Task<SalesPerson?> CreateSalesPersonAsync (Company company, Contact contact, CompanyUser? companyUser = null);
        Task<SalesPerson?> FindSalesPersonByIdAsync (string salesPersonId, Company company);
        Task<SalesPerson?> UpdateSalesPersonAsync (Company company, Contact contact, CompanyUser? companyUser = null);
        Task RemoveSalesPersonAsync (SalesPerson salesPerson);

        /***********************************************************************************
         * COMPANY REGIONAL_SETTINGS Transactions
         **********************************************************************************/
        Task<CompanyRegionalSettings?> CreateRegionalSettingsAsync (Company company, CompanyRegionalSettings regionalSettings);
        Task<CompanyRegionalSettings?> FindRegionalSettingsByCompanyIdAsync (string companyId);
        Task<CompanyRegionalSettings?> UpdateRegionalSettingsAsync (CompanyRegionalSettings regionalSettings);
        Task RemoveRegionalSettingsAsync (CompanyRegionalSettings regionalSettings);

        /***********************************************************************************
         * COMPANY EMAIL_SETTINGS Transactions
         **********************************************************************************/
        Task<CompanyMailSettings?> CreateMailSettingsAsync (Company company, CompanyMailSettings mailSettings);
        Task<CompanyMailSettings?> UpdateMailSettingsAsync (CompanyMailSettings mailSettings);
        Task<CompanyMailSettings?> FindMailSettingsByIdAsync (string mailSettingsId);
        Task<IList<CompanyMailSettings>> FindAllMailSettingsByCompanyIdAsync (string companyId);
        Task RemoveMailSettingsAsync (CompanyMailSettings mailSettings);

    }
}
