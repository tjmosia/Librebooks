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
		Task<BusinessSector?> FindBusinessSectorByIdAsync (int id);
		Task<IList<BusinessSector>> FindBusinessSectorsByIdsAsync (params int[] sectorIds);
		Task<TransactionResult<BusinessSector>> UpdateBusinessSectorAsync (BusinessSector sector);
		Task<IList<BusinessSector>> GetBusinessSectorsAsync ();

		/******************************************************************
         * DATE_FORMAT Store Manager Actions
         ******************************************************************/
		Task<TransactionResult<DateFormat>> AddDateFormatAsync (DateFormat dateFormat);
		Task<TransactionResult> DeleteDateFormatAsync (params DateFormat[] paymentMethods);
		Task<DateFormat?> FindDateFormatByIdAsync (int id);
		Task<TransactionResult<DateFormat>> UpdateDateFormatAsync (DateFormat dateFormat);
		Task<IList<DateFormat>> GetDateFormatsAsync ();

		/******************************************************************
         * PAYMENT_METHOD Store Manager Actions
         ******************************************************************/
		Task<TransactionResult<PaymentMethod>> AddPaymentMethodAsync (PaymentMethod paymentMethod);
		Task<TransactionResult> DeletePaymentMethodAsync (params PaymentMethod[] paymentMethods);
		Task<PaymentMethod?> FindPaymentMethodByIdAsync (int id);
		Task<TransactionResult<PaymentMethod>> UpdatePaymentMethodAsync (PaymentMethod paymentMethod);
		//Task<IList<PaymentMethod>> GetPaymentMethodsAsync ();

		/******************************************************************
         * PAYMENT_TERM Store Manager Actions
         ******************************************************************/
		Task<TransactionResult<PaymentTerm>> AddPaymentTermAsync (PaymentTerm paymentTerm);
		Task<TransactionResult> DeletePaymentTermAsync (params PaymentTerm[] paymentTerms);
		Task<PaymentTerm?> FindPaymentTermByIdAsync (int id);
		Task<TransactionResult<PaymentTerm>> UpdatePaymentTermAsync (PaymentTerm paymentTerm);

		/******************************************************************
         * SHIPPING_METHOD Store Manager Actions
         ******************************************************************/
		Task<TransactionResult<ShippingMethod>> AddShippingMethodAsync (ShippingMethod shippingMethod);
		Task<TransactionResult> DeleteShippingMethodAsync (params ShippingMethod[] shippingMethods);
		Task<ShippingMethod?> FindShippingMethodByIdAsync (int id);
		Task<TransactionResult<ShippingMethod>> UpdateShippingMethodAsync (ShippingMethod shippingMethod);

		/******************************************************************
         * SHIPPING_TERM Store Manager Actions
         ******************************************************************/
		Task<TransactionResult<ShippingTerm>> AddShippingTermAsync (ShippingTerm shippingTerm);
		Task<TransactionResult> DeleteShippingTermAsync (params ShippingTerm[] shippingTerms);
		Task<ShippingTerm?> FindShippingTermByIdAsync (int id);
		Task<TransactionResult<ShippingTerm>> UpdateShippingTermAsync (ShippingTerm shippingTerm);

		/******************************************************************
         * TaxType Store Manager Actions
         ******************************************************************/
		Task<TransactionResult<TaxType>> AddTaxTypeAsync (TaxType taxType);
		Task<TransactionResult> DeleteTaxTypeAsync (params TaxType[] taxType);
		Task<TaxType?> FindTaxTypeByIdAsync (int id);
		Task<TransactionResult<TaxType>> UpdateTaxTypeAsync (TaxType taxType);
		Task<IList<TaxType>> GetTaxTypesAsync ();

		/******************************************************************
         * SYSTEM_COMPANY_NUMBER Store Manager Actions
         ******************************************************************/
		Task<TransactionResult<CompanySetup>> InitializeAsync (int nextNumber = 1, string? numberPrefix = null, string? numberFormat = null);
		Task<string?> FindCompanyNumberParamsFromCacheAsync ();
		Task<long> GenerateNewCompanyNumberAsync ();
		Task<TransactionResult> UpdateCompanyNumberParamsAsync (string prefix, string numberFormat);

		//Task CacheCompanySetupParamsAsync (string? numPrefix, string? numFormat);
		//Task DeCacheCompanySetupParamsAsync ();
	}
}
