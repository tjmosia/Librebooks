using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.SystemSpace;

namespace Librebooks.Areas.Systems.Services;

public interface ISystemsManager
{

	/******************************************************************
         * CURRENCY Store Manager Actions
         ******************************************************************/
	Task<IList<Currency>> GetCurrenciesAsync (CancellationToken cancellationToken = default);
	Task<Currency?> FindCurrencyByCodeAsync (string code, CancellationToken cancellationToken = default);
	Task<Result<Currency>> AddCurrencyAsync (Currency currency, CancellationToken cancellationToken = default);
	Task<Result> DeleteCurrencyAsync (Currency[] paymentMethods, CancellationToken cancellationToken = default);
	Task<Result<Currency>> UpdateCurrencyAsync (Currency currency, CancellationToken cancellationToken = default);

	/******************************************************************
         * BUSINESS_SECTOR Store Manager Actions
         ******************************************************************/
	Task<Result<BusinessSector>> AddBusinessSectorAsync (BusinessSector sector, CancellationToken cancellationToken = default);
	Task<Result> DeleteBusinessSectorsAsync (BusinessSector[] sectors, CancellationToken cancellationToken = default);
	Task<BusinessSector?> FindBusinessSectorByIdAsync (int id, CancellationToken cancellationToken = default);
	Task<IList<BusinessSector>> FindBusinessSectorsByIdsAsync (int[] sectorIds, CancellationToken cancellationToken = default);
	Task<Result<BusinessSector>> UpdateBusinessSectorAsync (BusinessSector sector, CancellationToken cancellationToken = default);
	Task<IList<BusinessSector>> GetBusinessSectorsAsync (CancellationToken cancellationToken = default);


	/******************************************************************
         * DATE_FORMAT Store Manager Actions
         ******************************************************************/
	Task<Result<DateFormat>> AddDateFormatAsync (DateFormat dateFormat, CancellationToken cancellationToken = default);
	Task<Result> DeleteDateFormatAsync (DateFormat[] paymentMethods, CancellationToken cancellationToken = default);
	Task<DateFormat?> FindDateFormatByIdAsync (int id, CancellationToken cancellationToken);
	Task<Result<DateFormat>> UpdateDateFormatAsync (DateFormat dateFormat, CancellationToken cancellationToken = default);
	Task<IList<DateFormat>> GetDateFormatsAsync (CancellationToken cancellationToken = default);

	/******************************************************************
         * PAYMENT_METHOD Store Manager Actions
         ******************************************************************/
	Task<Result<PaymentMethod>> AddPaymentMethodAsync (PaymentMethod paymentMethod, CancellationToken cancellationToken = default);
	Task<Result> DeletePaymentMethodAsync (PaymentMethod[] paymentMethods, CancellationToken cancellationToken = default);
	Task<PaymentMethod?> FindPaymentMethodByIdAsync (int id, CancellationToken cancellationToken = default);
	Task<Result<PaymentMethod>> UpdatePaymentMethodAsync (PaymentMethod paymentMethod, CancellationToken cancellationToken = default);
	Task<IList<PaymentMethod>> GetPaymentMethodsAsync (CancellationToken cancellationToken = default);

	/******************************************************************
         * PAYMENT_TERM Store Manager Actions
         ******************************************************************/
	Task<Result<PaymentTerm>> AddPaymentTermAsync (PaymentTerm paymentTerm, CancellationToken cancellationToken = default);
	Task<Result> DeletePaymentTermAsync (PaymentTerm[] paymentTerms, CancellationToken cancellationToken = default);
	Task<PaymentTerm?> FindPaymentTermByIdAsync (int id, CancellationToken cancellationToken = default);
	Task<Result<PaymentTerm>> UpdatePaymentTermAsync (PaymentTerm paymentTerm, CancellationToken cancellationToken = default);

	/******************************************************************
         * SHIPPING_METHOD Store Manager Actions
         ******************************************************************/
	Task<Result<ShippingMethod>> AddShippingMethodAsync (ShippingMethod shippingMethod, CancellationToken cancellationToken = default);
	Task<Result> DeleteShippingMethodAsync (ShippingMethod[] shippingMethods, CancellationToken cancellationToken = default);
	Task<ShippingMethod?> FindShippingMethodByIdAsync (int id, CancellationToken cancellationToken = default);
	Task<Result<ShippingMethod>> UpdateShippingMethodAsync (ShippingMethod shippingMethod, CancellationToken cancellationToken = default);

	/******************************************************************
         * SHIPPING_TERM Store Manager Actions
         ******************************************************************/
	Task<Result<ShippingTerm>> AddShippingTermAsync (ShippingTerm shippingTerm, CancellationToken cancellationToken = default);
	Task<Result> DeleteShippingTermAsync (ShippingTerm[] shippingTerms, CancellationToken cancellationToken = default);
	Task<ShippingTerm?> FindShippingTermByIdAsync (int id, CancellationToken cancellationToken = default);
	Task<Result<ShippingTerm>> UpdateShippingTermAsync (ShippingTerm shippingTerm, CancellationToken cancellationToken = default);

	/******************************************************************
         * tax Store Manager Actions
         ******************************************************************/
	Task<Result<Tax>> AddTaxAsync (Tax tax, CancellationToken cancellationToken = default);
	Task<Result> DeleteTaxesAsync (Tax[] tax, CancellationToken cancellationToken = default);
	Task<Tax?> FindTaxByIdAsync (int id, CancellationToken cancellationToken = default);
	Task<Result<Tax>> UpdateTaxAsync (Tax tax, CancellationToken cancellationToken = default);
	Task<IList<Tax>> GetTaxesAsync (CancellationToken cancellationToken = default);

	/******************************************************************
         * COUNTRIES
         ******************************************************************/

	Task<IList<Country>> GetCountriesAsync (CancellationToken cancellationToken = default);
	Task<IList<Country>> GetCountriesByIdsAsync (int[] countryIds, CancellationToken cancellationToken = default);
	Task<Result<Country>> AddCountryAsync (Country country, CancellationToken cancellationToken = default);
	Task<Result> DeleteCountryAsync (Country[] countries, CancellationToken cancellationToken = default);
	Task<Country?> FindCountryByIdAsync (int countryId, CancellationToken cancellationToken = default);
	Task<Result<Country>> UpdateCountryAsync (Country country, CancellationToken cancellationToken = default);

	/******************************************************************
         * SYSTEM_COMPANY_NUMBER Store Manager Actions
         ******************************************************************/
	Task<Result> UpdateCompanyNumberParamsAsync (string prefix, string numberFormat, CancellationToken cancellationToken = default);
	Task<(string? Prefix, string? Surfix)> GetCompanyNumberParamsAsync (CancellationToken cancellationToken = default);
}
