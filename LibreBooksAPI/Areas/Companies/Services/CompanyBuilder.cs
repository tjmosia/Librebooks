using LibreBooks.Models.Entity.CompanySpace;
using LibreBooks.Models.Entity.DocumentSpace;
using LibreBooks.Models.Entity.IdentitySpace;
using LibreBooks.Models.Entity.SystemSpace;

using LibreBooksAPI.Models.Entity.CustomerSpace;
using LibreBooksAPI.Models.Entity.InventorySpace;
using LibreBooksAPI.Models.Entity.SupplierSpace;

namespace LibreBooksAPI.Areas.Companies.Services
{
    public class CompanyBuilder (Company company)
    {
        private Company company = company;

        public static CompanyBuilder Begin (Company company)
            => new CompanyBuilder(company);

        public CompanyBuilder AddRegionalSettings (CompanyRegionalSettings regionalSettings)
        {
            company.RegionalSettings = regionalSettings;
            return this;
        }

        public CompanyBuilder AddSupplerSetup (SupplierSetup supplierSetup)
        {
            company.SupplierSetup = supplierSetup;
            return this;
        }

        public CompanyBuilder AddCustomerSetup (CustomerSetup customerSetup)
        {
            company.CustomerSetup = customerSetup;
            return this;
        }

        public CompanyBuilder AddDocumentSetup (DocumentSetup[] documentSetups)
        {
            company.DocumentSetups = documentSetups;
            return this;
        }

        public CompanyBuilder AddTaxTypes (TaxType[] taxTypes)
        {
            company.TaxTypes = taxTypes.Select(p => new CompanyTaxType(company.Id, p.Id)).ToArray();
            return this;
        }

        public CompanyBuilder AddDefaultTaxType (TaxType taxType)
        {
            company.DefaultTaxType = new CompanyDefaultTaxType(company.Id, taxType.Id);
            return this;
        }

        public CompanyBuilder AddItemSetup (ItemSetup itemSetup)
        {
            company.ItemSetup = itemSetup;
            return this;
        }

        public CompanyBuilder AddUser (User user)
        {
            company.Users = [new CompanyUser(company.Id, user.Id)];
            return this;
        }

        public CompanyBuilder AddLogo (CompanyImage companyImage)
        {
            company.Logo = new CompanyLogo
            {
                Image = companyImage,
                CompanyId = company.Id
            };

            return this;
        }

        public Company Build () => company;
    }
}
