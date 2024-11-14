using OskitAPI.Areas.SystemSetups.Services.SubStores;
using OskitAPI.Core.EFCore;
using OskitAPI.Data;

namespace OskitAPI.Areas.SystemSetups.Services
{
    public class SystemStore (
        AppDbContext? context,
        ILogger<DbStoreBase>? logger,
        ShippingTermStore? shippingTerms,
        ShippingMethodStore? shippingMethods,
        CountryStore? countries,
        CurrencyStore? currencies,
        DateFormatStore? dateFormats,
        SystemCompanyNumberStore? companyNumber,
        PaymentMethodStore? paymentMethods,
        PaymentTermStore? paymentTerms,
        ValueAddedTaxStore? valueAddedTax)
        : DbStoreBase(context, logger)
    {
        public readonly ShippingTermStore? ShippingTerms = shippingTerms;
        public readonly ShippingMethodStore? ShippingMethods = shippingMethods;
        public readonly CountryStore? Countries = countries;
        public readonly CurrencyStore? Currencies = currencies;
        public readonly DateFormatStore? DateFormats = dateFormats;
        public readonly SystemCompanyNumberStore? CompanyNumber = companyNumber;
        public readonly PaymentMethodStore? PaymentMethods = paymentMethods;
        public readonly PaymentTermStore? PaymentTerms = paymentTerms;
        public readonly ValueAddedTaxStore? ValueAddedTax = valueAddedTax;
    }
}
