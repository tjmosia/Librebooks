using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.SystemSpace;

namespace Librebooks.Areas.Admin.Services
{
    public interface ISystemManager
    {
        /******************************************************************
         * COUNTRY Store Manager Actions
         ******************************************************************/
        Task<IList<Country>> GetCountriesAsync ();
        Task<Country?> FindCountryByCodeAsync (string countryCode);
        Task<IList<Country>> FindCountriesByCodesAsync (params string[] countryCodes);
        Task<TransactionResult<Country>> AddCountryAsync (Country country);
        Task<TransactionResult> DeleteCountryAsync (params Country[] countries);
        Task<TransactionResult<Country>> UpdateCountryAsync (Country country);

        /******************************************************************
         * CURRENCY Store Manager Actions
         ******************************************************************/
        Task<IList<Currency>> GetCurrencriesAsync ();
        Task<Currency?> FindCurrencyByCodeAsync (string code);
        Task<TransactionResult<Currency>> AddCurrencyAsync (Currency currency);
        Task<TransactionResult> DeleteCurrencyAsync (params Currency[] paymentMethods);
        Task<TransactionResult<Currency>> UpdateCurrencyAsync (Currency currency);

        /******************************************************************
         * BUSINESS_SECTOR Store Manager Actions
         ******************************************************************/
        Task<TransactionResult<BusinessSector>> AddBusinessSectorAsync (BusinessSector sector);
        Task<TransactionResult> DeleteBusinessSectorsAsync (params BusinessSector[] sectors);
        Task<BusinessSector?> FindBusinessSectorByIdAsync (string id);
        Task<IList<BusinessSector>> FindBusinessSectorsByIdsAsync (params string[] sectorIds);
        Task<TransactionResult<BusinessSector>> UpdateBusinessSectorAsync (BusinessSector sector);
        Task<IList<BusinessSector>> GetBusinessSectorsAsync ();

        /******************************************************************
         * DATE_FORMAT Store Manager Actions
         ******************************************************************/
        Task<TransactionResult<DateFormat>> AddDateFormatAsync (DateFormat dateFormat);
        Task<TransactionResult> DeleteDateFormatAsync (params DateFormat[] paymentMethods);
        Task<DateFormat?> FindDateFormatByIdAsync (string id);
        Task<TransactionResult<DateFormat>> UpdateDateFormatAsync (DateFormat dateFormat);
        Task<IList<DateFormat>> GetDateFormatsAsync ();

        /******************************************************************
         * PAYMENT_METHOD Store Manager Actions
         ******************************************************************/
        Task<TransactionResult<PaymentMethod>> AddPaymentMethodAsync (PaymentMethod paymentMethod);
        Task<TransactionResult> DeletePaymentMethodAsync (params PaymentMethod[] paymentMethods);
        Task<PaymentMethod?> FindPaymentMethodByIdAsync (string id);
        Task<TransactionResult<PaymentMethod>> UpdatePaymentMethodAsync (PaymentMethod paymentMethod);
        //Task<IList<PaymentMethod>> GetPaymentMethodsAsync ();

        /******************************************************************
         * PAYMENT_TERM Store Manager Actions
         ******************************************************************/
        Task<TransactionResult<PaymentTerm>> AddPaymentTermAsync (PaymentTerm paymentTerm);
        Task<TransactionResult> DeletePaymentTermAsync (params PaymentTerm[] paymentTerms);
        Task<PaymentTerm?> FindPaymentTermByIdAsync (string id);
        Task<TransactionResult<PaymentTerm>> UpdatePaymentTermAsync (PaymentTerm paymentTerm);

        /******************************************************************
         * SHIPPING_METHOD Store Manager Actions
         ******************************************************************/
        Task<TransactionResult<ShippingMethod>> AddShippingMethodAsync (ShippingMethod shippingMethod);
        Task<TransactionResult> DeleteShippingMethodAsync (params ShippingMethod[] shippingMethods);
        Task<ShippingMethod?> FindShippingMethodByIdAsync (string id);
        Task<TransactionResult<ShippingMethod>> UpdateShippingMethodAsync (ShippingMethod shippingMethod);

        /******************************************************************
         * SHIPPING_TERM Store Manager Actions
         ******************************************************************/
        Task<TransactionResult<ShippingTerm>> AddShippingTermAsync (ShippingTerm shippingTerm);
        Task<TransactionResult> DeleteShippingTermAsync (params ShippingTerm[] shippingTerms);
        Task<ShippingTerm?> FindShippingTermByIdAsync (string id);
        Task<TransactionResult<ShippingTerm>> UpdateShippingTermAsync (ShippingTerm shippingTerm);

        /******************************************************************
         * TaxType Store Manager Actions
         ******************************************************************/
        Task<TransactionResult<TaxType>> AddTaxTypeAsync (TaxType taxType);
        Task<TransactionResult> DeleteTaxTypeAsync (params TaxType[] taxType);
        Task<TaxType?> FindTaxTypeByIdAsync (string id);
        Task<TransactionResult<TaxType>> UpdateTaxTypeAsync (TaxType taxType);
        Task<IList<TaxType>> GetTaxTypesAsync ();

        /******************************************************************
         * SYSTEM_COMPANY_NUMBER Store Manager Actions
         ******************************************************************/
        Task<TransactionResult<CompanySetup>> InitializeAsync (
            long nextNumber = 1,
            string? numberPrefix = null,
            string? numberFormat = null
        );
        Task<string?> FindCompanyNumberParamsFromCacheAsync ();
        Task<long> GenerateNewCompanyNumberAsync ();
        Task<TransactionResult> UpdateCompanyNumberParamsAsync (string prefix, string numberFormat);

        //Task CacheCompanySetupParamsAsync (string? numPrefix, string? numFormat);
        //Task DeCacheCompanySetupParamsAsync ();
    }
}
