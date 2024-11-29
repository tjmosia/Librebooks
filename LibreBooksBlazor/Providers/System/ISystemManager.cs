using OskitBlazor.CoreLib.Operations;
using OskitBlazor.Models.Entity.SystemSpace;

namespace OskitBlazor.Areas.SystemSetups.Services
{
    public interface ISystemManager
    {
        /******************************************************************
         * COUNTRY Store Manager Actions
         ******************************************************************/
        Task<TransactionResult<Country>> AddCountryAsync (Country country);
        Task<TransactionResult> DeleteCountryAsync (params Country[] paymentMethods);
        Task<Country?> GetCountryByCodeAsync (string code);
        Task<TransactionResult<Country>> UpdateCountryAsync (Country country);

        /******************************************************************
         * CURRENCY Store Manager Actions
         ******************************************************************/
        Task<TransactionResult<Currency>> AddCurrencyAsync (Currency currency);
        Task<TransactionResult> DeleteCurrencyAsync (params Currency[] paymentMethods);
        Task<Currency?> GetCurrencyByCodeAsync (string code);
        Task<TransactionResult<Currency>> UpdateCurrencyAsync (Currency currency);

        /******************************************************************
         * DATE_FORMAT Store Manager Actions
         ******************************************************************/
        Task<TransactionResult<DateFormat>> AddDateFormatAsync (DateFormat dateFormat);
        Task<TransactionResult> DeleteDateFormatAsync (params DateFormat[] dateFormats);
        Task<DateFormat?> GetDateFormatByIdAsync (string dateFormatId);
        Task<TransactionResult<DateFormat>> UpdateDateFormatAsync (DateFormat dateFormat);

        /******************************************************************
         * PAYMENT_METHOD Store Manager Actions
         ******************************************************************/
        Task<TransactionResult<PaymentMethod>> AddPaymentMethodAsync (PaymentMethod paymentMethod);
        Task<TransactionResult> DeletePaymentMethodAsync (params PaymentMethod[] paymentMethods);
        Task<PaymentMethod?> GetPaymentMethodByIdAsync (string paymentMethodId);
        Task<TransactionResult<PaymentMethod>> UpdatePaymentMethodAsync (PaymentMethod paymentMethod);

        /******************************************************************
         * PAYMENT_TERM Store Manager Actions
         ******************************************************************/
        Task<TransactionResult<PaymentTerm>> AddPaymentTermAsync (PaymentTerm paymentTerm);
        Task<TransactionResult> DeletePaymentTermAsync (params PaymentTerm[] paymentTerms);
        Task<PaymentTerm?> GetPaymentTermByIdAsync (string paymentTermId);
        Task<TransactionResult<PaymentTerm>> UpdatePaymentTermAsync (PaymentTerm paymentTerm);

        /******************************************************************
         * SHIPPING_METHOD Store Manager Actions
         ******************************************************************/
        Task<TransactionResult<ShippingMethod>> AddShippingMethodAsync (ShippingMethod shippingMethod);
        Task<TransactionResult> DeleteShippingMethodAsync (params ShippingMethod[] shippingMethods);
        Task<ShippingMethod?> GetShippingMethodByIdAsync (string shippingMethodId);
        Task<TransactionResult<ShippingMethod>> UpdateShippingMethodAsync (ShippingMethod shippingMethod);

        /******************************************************************
         * SHIPPING_TERM Store Manager Actions
         ******************************************************************/
        Task<TransactionResult<ShippingTerm>> AddShippingTermAsync (ShippingTerm shippingTerm);
        Task<TransactionResult> DeleteShippingTermAsync (params ShippingTerm[] shippingTerms);
        Task<ShippingTerm?> GetShippingTermByIdAsync (string shippingTermId);
        Task<TransactionResult<ShippingTerm>> UpdateShippingTermAsync (ShippingTerm shippingTerm);

        /******************************************************************
         * VAT Store Manager Actions
         ******************************************************************/
        Task<TransactionResult<ValueAddedTax>> AddVATAsync (ValueAddedTax vat);
        Task<TransactionResult> DeleteVATAsync (params ValueAddedTax[] vat);
        Task<ValueAddedTax?> GetVATByIdAsync (string vatId);
        Task<TransactionResult<ValueAddedTax>> UpdateVATAsync (ValueAddedTax vat);

        /******************************************************************
         * BUSINESS_SECTOR Store Manager Actions
         ******************************************************************/
        Task<TransactionResult<BusinessSector>> AddBusinessSectorAsync (BusinessSector businessSector);
        Task<TransactionResult> DeleteBusinessSectorAsync (params BusinessSector[] businessSector);
        Task<BusinessSector?> GetBusinessSectorByIdAsync (string businessSectorId);
        Task<TransactionResult<BusinessSector>> UpdateBusinessSectorAsync (BusinessSector businessSector);

        /******************************************************************
         * SYSTEM_COMPANY_NUMBER Store Manager Actions
         ******************************************************************/
        Task<TransactionResult<SystemCompanyNumber>> InitializeAsync (
            long nextNumber = 1,
            string? numberPrefix = null,
            string? numberFormat = null
        );
        Task<string?> GetCompanyNumberParamsFromCacheAsync ();
        Task<long> GenerateNewCompanyNumberAsync ();
        Task<TransactionResult> UpdateCompanyNumberParamsAsync (string prefix, string numberFormat);

        //Task SyncCompanyNumberParamsToCacheAsync (string? numPrefix, string? numFormat);
        //Task DeleteCompanyNumberParamsFromCacheAsync ();
    }
}
