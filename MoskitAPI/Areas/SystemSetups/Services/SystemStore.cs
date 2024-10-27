using Moskit.Areas.SystemSetups.Services.SubStores;
using Moskit.Data;

namespace Moskit.Areas.SystemSetups.Services
{
    public class SystemStore (AppDbContext context, ILogger logger)
    {
        public readonly ShippingTermStore ShippingTerms = new(context);
        public readonly ShippingMethodStore ShippingMethods = new(context);
        public readonly CountryStore Countries = new(context, logger);
        public readonly CurrencyStore Currencies = new(context);
        public readonly DateFormatStore DateFormats = new(context);
        public readonly SystemCompanyNumberStore CompanyNumber = new(context);
        public readonly PaymentMethodStore PaymentMethods = new(context);
        public readonly PaymentTermStore PaymentTerms = new(context);
        public readonly ValueAddedTaxStore ValueAddedTax = new(context, logger);
    }
}
