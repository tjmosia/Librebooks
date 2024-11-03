using OskitAPI.Areas.SystemSetups.Services.SubStores;
using OskitAPI.Core.EFCore;
using OskitAPI.Data;

namespace OskitAPI.Areas.SystemSetups.Services
{
    public class SystemStore (AppDbContext? context, ILogger? logger)
        : DbStoreBase(context, logger)
    {
        public readonly ShippingTermStore ShippingTerms = new(context, logger);
        public readonly ShippingMethodStore ShippingMethods = new(context, logger);
        public readonly CountryStore Countries = new(context, logger);
        public readonly CurrencyStore Currencies = new(context, logger);
        public readonly DateFormatStore DateFormats = new(context, logger);
        public readonly SystemCompanyNumberStore CompanyNumber = new(context, logger);
        public readonly PaymentMethodStore PaymentMethods = new(context, logger);
        public readonly PaymentTermStore PaymentTerms = new(context, logger);
        public readonly ValueAddedTaxStore ValueAddedTax = new(context, logger);
    }
}
