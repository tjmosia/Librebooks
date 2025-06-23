using Librebooks.Areas.Admin.Services.SubStores;
using Librebooks.Core.EFCore;
using Librebooks.Data;

namespace Librebooks.Areas.Admin.Services
{
    public class SystemStore (
        AppDbContext context,
        ILogger<DbStoreBase> logger,
        ShippingTermStore shippingTerms,
        ShippingMethodStore shippingMethods,
        CountryStore countries,
        CurrencyStore currencies,
        DateFormatStore dateFormats,
        SystemCompanyNumberStore companyNumber,
        PaymentMethodStore paymentMethods,
        PaymentTermStore paymentTerms,
        TaxTypeStore taxTypes,
        BusinessSectorStore businessSector)
        : DbStoreBase(context, logger)
    {
        public readonly ShippingTermStore ShippingTerms = shippingTerms;
        public readonly ShippingMethodStore ShippingMethods = shippingMethods;
        public readonly CountryStore Countries = countries;
        public readonly CurrencyStore Currencies = currencies;
        public readonly DateFormatStore DateFormats = dateFormats;
        public readonly SystemCompanyNumberStore CompanyNumber = companyNumber;
        public readonly PaymentMethodStore PaymentMethods = paymentMethods;
        public readonly PaymentTermStore PaymentTerms = paymentTerms;
        public readonly TaxTypeStore TaxTypes = taxTypes;
        public readonly BusinessSectorStore BusinessSector = businessSector;
    }
}
