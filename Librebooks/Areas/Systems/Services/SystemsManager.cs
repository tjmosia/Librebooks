using Librebooks.Core.EFCore;
using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.SystemSpace;
using Microsoft.Extensions.Caching.Distributed;

namespace Librebooks.Areas.Systems.Services;

public class SystemsManager (SystemsStore systemStore, IDistributedCache cache, DbErrorDescriber? dbErrorDescriber, ILogger<SystemsManager> logger)
	: ISystemsManager
{
	private readonly SystemsStore store = systemStore;
	private readonly DbErrorDescriber? dbErrorDescriber = dbErrorDescriber;
	private readonly IDistributedCache cache = cache;
	private readonly ILogger<SystemsManager> logger = logger;

	private const string COMP_NUM_PREFIX = nameof(COMP_NUM_PREFIX);
	private const string COMP_NUM_FORMAT = nameof(COMP_NUM_FORMAT);

	/******************************************************************
    * SystemCompanyNumber Manager Actions
    ******************************************************************/

	public async Task<Result> UpdateCompanyNumberParamsAsync (string prefix, string numberFormat, CancellationToken cancellationToken = default)
	{
		var setup = await store.CompanyNumberStore.FindCurrentAsync(cancellationToken);

		setup!.Prefix = prefix;
		setup.NumberFormat = numberFormat;
		var result = await store.CompanyNumberStore.UpdateAsync(setup!, cancellationToken);

		return Result.Success;
	}

	public async Task<(string? Prefix, string? Surfix)> GetCompanyNumberParamsAsync (CancellationToken cancellationToken = default)
	{
		var setup = await store.GetCompanySetupAsync(cancellationToken);

		return (setup?.Prefix, setup?.Suffix);
	}

	/******************************************************************
    * Countries Store Manager Actions
    ******************************************************************/
	public async Task<IList<Country>> GetCountriesAsync (CancellationToken cancellationToken = default)
		=> await store.CountriesStore.FindAllAsync(cancellationToken);

	public async Task<Result<Country>> AddCountryAsync (Country country, CancellationToken cancellationToken = default)
		=> await store.CountriesStore.CreateAsync(country, cancellationToken);

	public async Task<Result> DeleteCountryAsync (Country[] countries, CancellationToken cancellationToken = default)
		=> await store.CountriesStore.DeleteAsync(countries, cancellationToken);

	public async Task<Country?> FindCountryByIdAsync (int countryId, CancellationToken cancellationToken = default)
		=> await store.CountriesStore.FindByIdAsync(countryId, cancellationToken);

	public async Task<Result<Country>> UpdateCountryAsync (Country country, CancellationToken cancellationToken = default)
		=> await store.CountriesStore.UpdateAsync(country, cancellationToken);
	public async Task<IList<Country>> GetCountriesByIdsAsync (int[] countryIds, CancellationToken cancellationToken = default)
		=> await store.CountriesStore.FindByIdsAsync(countryIds, cancellationToken);


	/******************************************************************
         * CURRENCY Store Manager Actions
         ******************************************************************/
	public async Task<IList<Currency>> GetCurrenciesAsync (CancellationToken cancellationToken = default)
		=> await store.CurrenciesStore.FindAllAsync(cancellationToken);

	public async Task<Result<Currency>> AddCurrencyAsync (Currency currency, CancellationToken cancellationToken = default)
		=> await store.CurrenciesStore.CreateAsync(currency, cancellationToken);

	public async Task<Result> DeleteCurrencyAsync (Currency[] currencies, CancellationToken cancellationToken = default)
		=> await store.CurrenciesStore.DeleteAsync(currencies, cancellationToken);

	public async Task<Currency?> FindCurrencyByCodeAsync (string code, CancellationToken cancellationToken = default)
		=> await store.CurrenciesStore.FindByCodeAsync(code, cancellationToken);

	public async Task<Result<Currency>> UpdateCurrencyAsync (Currency currency, CancellationToken cancellationToken = default)
		=> await store.CurrenciesStore.UpdateAsync(currency, cancellationToken);


	/******************************************************************
    * DATE_FORMAT Store Manager Actions
    ******************************************************************/

	public async Task<IList<DateFormat>> GetDateFormatsAsync (CancellationToken cancellationToken = default)
		=> await store.DateFormatsStore.FindAllAsync(cancellationToken);

	public async Task<Result<DateFormat>> AddDateFormatAsync (DateFormat dateFormat, CancellationToken cancellationToken = default)
		=> await store.DateFormatsStore.CreateAsync(dateFormat, cancellationToken);

	public async Task<Result> DeleteDateFormatAsync (DateFormat[] dateFormats, CancellationToken cancellationToken = default)
		=> await store.DateFormatsStore.DeleteAsync(dateFormats, cancellationToken);

	public async Task<DateFormat?> FindDateFormatByIdAsync (int id, CancellationToken cancellationToken)
		=> await store.DateFormatsStore.FindByIdAsync(id, cancellationToken);

	public async Task<Result<DateFormat>> UpdateDateFormatAsync (DateFormat dateFormat, CancellationToken cancellationToken = default)
		=> await store.DateFormatsStore.UpdateAsync(dateFormat, cancellationToken);

	/******************************************************************
    * PAYMENT_METHOD Store Manager Actions
    ******************************************************************/
	public async Task<Result<PaymentMethod>> AddPaymentMethodAsync (PaymentMethod paymentMethod, CancellationToken cancellationToken = default)
		=> await store.PaymentMethodsStore.CreateAsync(paymentMethod, cancellationToken);

	public async Task<Result> DeletePaymentMethodAsync (PaymentMethod[] paymentMethods, CancellationToken cancellationToken = default)
		=> await store.PaymentMethodsStore.DeleteAsync(paymentMethods, cancellationToken);

	public Task<IList<PaymentMethod>> GetPaymentMethodsAsync (CancellationToken cancellationToken = default)
		=> store.PaymentMethodsStore.FindAllAsync(cancellationToken);

	public Task<PaymentMethod?> FindPaymentMethodByIdAsync (int id, CancellationToken cancellationToken = default)
		=> store.PaymentMethodsStore.FindByIdAsync(id, cancellationToken);

	public async Task<Result<PaymentMethod>> UpdatePaymentMethodAsync (PaymentMethod paymentMethod, CancellationToken cancellationToken = default)
		=> await store.PaymentMethodsStore.UpdateAsync(paymentMethod, cancellationToken);


	/******************************************************************
    * PAYMENT_TERM Store Manager Actions
    ******************************************************************/

	public async Task<Result<PaymentTerm>> AddPaymentTermAsync (PaymentTerm paymentTerm, CancellationToken cancellationToken = default)
		=> await store.PaymentTermsStore.CreateAsync(paymentTerm, cancellationToken);

	public async Task<Result> DeletePaymentTermAsync (PaymentTerm[] paymentTerms, CancellationToken cancellationToken = default)
		=> await store.PaymentTermsStore.DeleteAsync(paymentTerms, cancellationToken);

	public async Task<PaymentTerm?> FindPaymentTermByIdAsync (int id, CancellationToken cancellationToken = default)
		=> await store.PaymentTermsStore.FindByIdAsync(id, cancellationToken);

	public async Task<Result<PaymentTerm>> UpdatePaymentTermAsync (PaymentTerm paymentTerm, CancellationToken cancellationToken = default)
		=> await store.PaymentTermsStore.UpdateAsync(paymentTerm, cancellationToken);

	/******************************************************************
    * SHIPPING_METHOD Store Manager Actions
    ******************************************************************/

	public async Task<Result<ShippingMethod>> AddShippingMethodAsync (ShippingMethod shippingMethod, CancellationToken cancellationToken = default)
		=> await store.ShippingMethodsStore.CreateAsync(shippingMethod, cancellationToken);

	public async Task<Result> DeleteShippingMethodAsync (ShippingMethod[] shippingMethods, CancellationToken cancellationToken = default)
		=> await store.ShippingMethodsStore.DeleteAsync(shippingMethods, cancellationToken);

	public async Task<ShippingMethod?> FindShippingMethodByIdAsync (int id, CancellationToken cancellationToken = default)
		=> await store.ShippingMethodsStore.FindByIdAsync(id, cancellationToken);

	public async Task<Result<ShippingMethod>> UpdateShippingMethodAsync (ShippingMethod shippingMethod, CancellationToken cancellationToken = default)
		=> await store.ShippingMethodsStore.UpdateAsync(shippingMethod, cancellationToken);

	/******************************************************************
    * SHIPPING_TERM Store Manager Actions
    ******************************************************************/
	public async Task<Result<ShippingTerm>> AddShippingTermAsync (ShippingTerm shippingTerm, CancellationToken cancellationToken = default)
		=> await store.ShippingTermsStore.CreateAsync(shippingTerm, cancellationToken);

	public async Task<Result> DeleteShippingTermAsync (ShippingTerm[] shippingTerms, CancellationToken cancellationToken = default)
		=> await store.ShippingTermsStore.DeleteAsync(shippingTerms, cancellationToken);

	public async Task<ShippingTerm?> FindShippingTermByIdAsync (int id, CancellationToken cancellationToken = default)
		=> await store.ShippingTermsStore.FindByIdAsync(id, cancellationToken);


	public async Task<Result<ShippingTerm>> UpdateShippingTermAsync (ShippingTerm shippingTerm, CancellationToken cancellationToken = default)
		=> await store.ShippingTermsStore.UpdateAsync(shippingTerm, cancellationToken);

	/******************************************************************
    * TAX_TYPES Store Manager Actions
    ******************************************************************/
	public async Task<Result<Tax>> AddTaxAsync (Tax tax, CancellationToken cancellationToken = default)
		=> await store.TaxTypesStore.CreateAsync(tax, cancellationToken);
	public async Task<Result> DeleteTaxesAsync (Tax[] tax, CancellationToken cancellationToken = default)
		=> await store.TaxTypesStore.DeleteAsync(tax, cancellationToken);
	public async Task<Tax?> FindTaxByIdAsync (int id, CancellationToken cancellationToken = default)
		=> await store.TaxTypesStore.FindByIdAsync(id, cancellationToken);
	public async Task<Result<Tax>> UpdateTaxAsync (Tax tax, CancellationToken cancellationToken = default)
		=> await store.TaxTypesStore.UpdateAsync(tax, cancellationToken);
	public async Task<IList<Tax>> GetTaxesAsync (CancellationToken cancellationToken = default)
		=> await store.TaxTypesStore.FindAllAsync();


	/******************************************************************
    * BUSINESS_SECTOR Store Manager Actions
    ******************************************************************/

	public async Task<IList<BusinessSector>> GetBusinessSectorsAsync (CancellationToken cancellationToken = default)
		=> await store.BusinessSectorStore.FindAllAsync(cancellationToken);

	public async Task<Result<BusinessSector>> AddBusinessSectorAsync (BusinessSector sector, CancellationToken cancellationToken = default)
		=> await store.BusinessSectorStore.CreateAsync(sector, cancellationToken);

	public async Task<IList<BusinessSector>> FindBusinessSectorsByIdsAsync (int[] sectorIds, CancellationToken cancellationToken = default)
		=> await store.BusinessSectorStore.FindByIdsAsync(sectorIds, cancellationToken);

	public async Task<Result> DeleteBusinessSectorsAsync (BusinessSector[] sectors, CancellationToken cancellationToken = default)
		=> await store.BusinessSectorStore.DeleteAsync(sectors, cancellationToken);

	public async Task<BusinessSector?> FindBusinessSectorByIdAsync (int id, CancellationToken cancellationToken = default)
	 => await store.BusinessSectorStore.FindByIdAsync(id, cancellationToken);

	public async Task<Result<BusinessSector>> UpdateBusinessSectorAsync (BusinessSector sector, CancellationToken cancellationToken = default)
		=> await store.BusinessSectorStore.UpdateAsync(sector, cancellationToken);

	public async Task<IList<BusinessSector>> FindBusinessSectorsAsync (CancellationToken cancellationToken = default)
		=> await store.BusinessSectorStore.FindAllAsync(cancellationToken);

}
