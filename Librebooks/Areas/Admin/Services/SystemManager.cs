using Librebooks.Core.EFCore;
using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.Extensions.Caching.Distributed;

namespace Librebooks.Areas.Admin.Services
{
    public class SystemManager (SystemStore systemStore, IDistributedCache cache, DbErrorDescriber? dbErrorDescriber, ILogger<SystemManager> logger)
        : ISystemManager
    {
        private readonly SystemStore store = systemStore;
        private readonly DbErrorDescriber? dbErrorDescriber = dbErrorDescriber;
        private readonly IDistributedCache cache = cache;
        private readonly ILogger<SystemManager> logger = logger;

        private readonly string COMP_NUM_PREFIX = nameof(COMP_NUM_PREFIX);
        private readonly string COMP_NUM_FORMAT = nameof(COMP_NUM_FORMAT);

        /******************************************************************
         * SystemCompanyNumber Manager Actions
         ******************************************************************/
        public async Task<string?> FindCompanyNumberParamsFromCacheAsync ()
            => await cache.GetStringAsync(COMP_NUM_PREFIX);

        public async Task<TransactionResult<CompanySetup>> InitializeAsync (long nextNumber = 1, string? numberPrefix = null, string? numberFormat = null)
        {
            var setup = await store.CompanyNumber.CreateAsync(
                new CompanySetup
                {
                    NumberFormat = numberFormat,
                    Prefix = numberPrefix,
                    NextNumber = nextNumber
                });

            await CacheCompanySetupParamsAsync(setup.Prefix!, setup.NumberFormat!);

            return TransactionResult<CompanySetup>
                    .Success(setup);
        }

        public async Task<long> GenerateNewCompanyNumberAsync ()
        {
            var setup = await store.CompanyNumber.GetCurrentAsync();

            if (setup == null)
                setup = (await InitializeAsync()).Model;
            else
                setup.NextNumber += 1;

            await store.CompanyNumber.UpdateAsync(setup!);
            return setup!.NextNumber;
        }

        public async Task<TransactionResult> UpdateCompanyNumberParamsAsync (string prefix, string numberFormat)
        {
            var setup = await store.CompanyNumber.GetCurrentAsync();

            setup!.Prefix = prefix;
            setup.NumberFormat = numberFormat;
            var result = await store.CompanyNumber.UpdateAsync(setup!);
            await CacheCompanySetupParamsAsync(result.Prefix!, result.NumberFormat!);

            return TransactionResult.Success;
        }

        private async Task CacheCompanySetupParamsAsync (string? numPrefix, string? numFormat)
        {
            await cache.SetStringAsync(COMP_NUM_FORMAT, numFormat!);
            await cache.SetStringAsync(COMP_NUM_PREFIX, numPrefix!);
        }

        private async Task UnCacheCompanySetupParamsAsync ()
        {
            await cache.RemoveAsync(COMP_NUM_FORMAT);
            await cache.RemoveAsync(COMP_NUM_PREFIX);
        }

        /******************************************************************
         * COUNTRY Store Manager Actions
         ******************************************************************/

        public async Task<Country?> FindCountryByCodeAsync (string countryCode)
            => await store.Countries.FindByCodeAsync(countryCode);

        public async Task<IList<Country>> FindCountriesByCodesAsync (params string[] countryCodes)
            => await store.Countries.FindByCodesAsync(countryCodes);

        public async Task<IList<Country>> GetCountriesAsync ()
            => await store.Countries.FindAllAsync();

        public async Task<TransactionResult<Country>> AddCountryAsync (Country country)
        {
            country.Code = country.Code!.ToUpper();

            var result = await store.Countries.CreateAsync(country);

            if (result != null)
                return TransactionResult<Country>.Success(result);
            return TransactionResult<Country>.Failure();
        }

        public async Task<TransactionResult<Country>> UpdateCountryAsync (Country country)
        {
            var result = await store.Countries.UpdateAsync(country);

            if (result != null)
                return TransactionResult<Country>.Success(result);
            else
                return TransactionResult<Country>.Failure();
        }

        public async Task<TransactionResult> DeleteCountryAsync (params Country[] countries)
            => await store.Countries.DeleteAsync(countries) ? TransactionResult.Success : TransactionResult.Failure();


        /******************************************************************
         * CURRENCY Store Manager Actions
         ******************************************************************/
        public async Task<IList<Currency>> GetCurrencriesAsync ()
            => await store.Currencies.FindAllAsync();

        public async Task<TransactionResult<Currency>> AddCurrencyAsync (Currency currency)
        {
            var result = await store.Currencies.CreateAsync(currency);

            if (result != null)
                return TransactionResult<Currency>.Success(result!);
            return TransactionResult<Currency>.Failure();
        }

        public async Task<TransactionResult> DeleteCurrencyAsync (params Currency[] currencies)
        {
            await store.Currencies.DeleteAsync(currencies);
            return TransactionResult.Success;
        }

        public async Task<Currency?> FindCurrencyByCodeAsync (string code)
            => await store.Currencies.FindByCodeAsync(code);

        public async Task<TransactionResult<Currency>> UpdateCurrencyAsync (Currency currency)
        {
            var result = await store.Currencies.UpdateAsync(currency);

            if (result != null)
                return TransactionResult<Currency>.Success(result);
            else
                return TransactionResult<Currency>.Failure();
        }

        /******************************************************************
         * DATE_FORMAT Store Manager Actions
         ******************************************************************/

        public async Task<IList<DateFormat>> GetDateFormatsAsync ()
            => await store.DateFormats.FindAllAsync();

        public async Task<TransactionResult<DateFormat>> AddDateFormatAsync (DateFormat dateFormat)
        {
            var result = await store.DateFormats.CreateAsync(dateFormat);

            if (result != null)
                return TransactionResult<DateFormat>.Success(result);
            return TransactionResult<DateFormat>.Failure();
        }

        public async Task<TransactionResult> DeleteDateFormatAsync (params DateFormat[] dateFormats)
        {
            await store.DateFormats.DeleteAsync(dateFormats);
            return TransactionResult.Success;
        }

        public async Task<DateFormat?> FindDateFormatByIdAsync (string id)
            => await store.DateFormats.FindByIdAsync(id);

        public async Task<TransactionResult<DateFormat>> UpdateDateFormatAsync (DateFormat dateFormat)
        {
            var result = await store.DateFormats.UpdateAsync(dateFormat);

            if (result != null)
                return TransactionResult<DateFormat>.Success(result);
            else
                return TransactionResult<DateFormat>.Failure(TransactionError.FromDbError(dbErrorDescriber!.IndexConstraint()));
        }

        /******************************************************************
         * PAYMENT_METHOD Store Manager Actions
         ******************************************************************/
        public async Task<TransactionResult<PaymentMethod>> AddPaymentMethodAsync (PaymentMethod paymentMethod)
        {
            var result = await store.PaymentMethods.CreateAsync(paymentMethod);

            if (result != null)
                return TransactionResult<PaymentMethod>.Success(result);
            return TransactionResult<PaymentMethod>.Failure();
        }

        public async Task<TransactionResult> DeletePaymentMethodAsync (params PaymentMethod[] paymentMethods)
        {
            await store.PaymentMethods.DeleteAsync(paymentMethods);
            return TransactionResult.Success;
        }

        public Task<PaymentMethod?> FindPaymentMethodByIdAsync (string id)
            => store.PaymentMethods.FindByIdAsync(id);

        public async Task<TransactionResult<PaymentMethod>> UpdatePaymentMethodAsync (PaymentMethod paymentMethod)
        {
            var result = await store.PaymentMethods.UpdateAsync(paymentMethod);

            if (result != null)
                return TransactionResult<PaymentMethod>.Success(result);
            else
                return TransactionResult<PaymentMethod>.Failure(TransactionError.FromDbError(dbErrorDescriber!.IndexConstraint()));
        }

        /******************************************************************
         * PAYMENT_TERM Store Manager Actions
         ******************************************************************/

        public async Task<TransactionResult<PaymentTerm>> AddPaymentTermAsync (PaymentTerm paymentTerm)
        {
            var result = await store.PaymentTerms.CreateAsync(paymentTerm);

            if (result != null)
                return TransactionResult<PaymentTerm>.Success(result);
            return TransactionResult<PaymentTerm>.Failure();
        }

        public async Task<TransactionResult> DeletePaymentTermAsync (params PaymentTerm[] paymentTerms)
        {
            await store.PaymentTerms.DeleteAsync(paymentTerms);
            return TransactionResult.Success;
        }

        public async Task<PaymentTerm?> FindPaymentTermByIdAsync (string id)
            => await store.PaymentTerms.FindByIdAsync(id);

        public async Task<TransactionResult<PaymentTerm>> UpdatePaymentTermAsync (PaymentTerm paymentTerm)
        {
            var result = await store.PaymentTerms.UpdateAsync(paymentTerm);

            if (result != null)
                return TransactionResult<PaymentTerm>.Success(result);
            else
                return TransactionResult<PaymentTerm>.Failure(TransactionError.FromDbError(dbErrorDescriber!.IndexConstraint()));
        }

        /******************************************************************
         * SHIPPING_METHOD Store Manager Actions
         ******************************************************************/

        public async Task<TransactionResult<ShippingMethod>> AddShippingMethodAsync (ShippingMethod shippingMethod)
        {
            var result = await store.ShippingMethods.CreateAsync(shippingMethod);

            if (result != null)
                return TransactionResult<ShippingMethod>.Success(result);
            return TransactionResult<ShippingMethod>.Failure();
        }

        public async Task<TransactionResult> DeleteShippingMethodAsync (params ShippingMethod[] shippingMethods)
        {
            await store.ShippingMethods.DeleteAsync(shippingMethods);
            return TransactionResult.Success;
        }

        public async Task<ShippingMethod?> FindShippingMethodByIdAsync (string id)
            => await store.ShippingMethods.FindByIdAsync(id);

        public async Task<TransactionResult<ShippingMethod>> UpdateShippingMethodAsync (ShippingMethod shippingMethod)
        {
            var result = await store.ShippingMethods.UpdateAsync(shippingMethod);

            if (result != null)
                return TransactionResult<ShippingMethod>.Success(result);
            else
                return TransactionResult<ShippingMethod>.Failure();
        }

        /******************************************************************
         * SHIPPING_TERM Store Manager Actions
         ******************************************************************/
        public Task<TransactionResult<ShippingTerm>> AddShippingTermAsync (ShippingTerm shippingTerm)
        {
            throw new NotImplementedException();
        }

        public async Task<TransactionResult> DeleteShippingTermAsync (params ShippingTerm[] shippingTerms)
        {
            await store.ShippingTerms.DeleteAsync(shippingTerms);
            return TransactionResult.Success;
        }

        public async Task<ShippingTerm?> FindShippingTermByIdAsync (string id)
            => await store.ShippingTerms.FindByIdAsync(id);


        public async Task<TransactionResult<ShippingTerm>> UpdateShippingTermAsync (ShippingTerm shippingTerm)
        {
            var result = await store.ShippingTerms.UpdateAsync(shippingTerm);

            if (result != null)
                return TransactionResult<ShippingTerm>.Success(result);

            return TransactionResult<ShippingTerm>.Failure();
        }

        /******************************************************************
         * TAX_TYPES Store Manager Actions
         ******************************************************************/
        public async Task<TransactionResult<TaxType>> AddTaxTypeAsync (TaxType taxType)
        {
            var result = await store.ValueAddedTax.CreateAsync(taxType);

            if (result != null)
                return TransactionResult<TaxType>.Success(result);

            return TransactionResult<TaxType>.Failure();
        }

        public async Task<TransactionResult> DeleteTaxTypeAsync (params TaxType[] taxType)
        {
            await store.ValueAddedTax.DeleteAsync(taxType);
            return TransactionResult.Success;
        }

        public async Task<TaxType?> FindTaxTypeByIdAsync (string id)
            => await store.ValueAddedTax.FindByIdAsync(id);

        public async Task<TransactionResult<TaxType>> UpdateTaxTypeAsync (TaxType taxType)
        {
            var result = await store.ValueAddedTax.UpdateAsync(taxType);

            if (result != null)
                return TransactionResult<TaxType>.Success(result);

            return TransactionResult<TaxType>.Failure();
        }

        public async Task<IList<TaxType>> GetTaxTypesAsync ()
        {
            return await store.ValueAddedTax.FindAllAsync();
        }


        /******************************************************************
         * BUSINESS_SECTOR Store Manager Actions
         ******************************************************************/

        public async Task<IList<BusinessSector>> GetBusinessSectorsAsync ()
            => await store.BusinessSector.FindAllAsync();

        public async Task<TransactionResult<BusinessSector>> AddBusinessSectorAsync (BusinessSector sector)
        {
            var businessSector = await store.BusinessSector.CreateAsync(sector);

            if (businessSector == null)
                return TransactionResult<BusinessSector>.Failure(TransactionError.Create("", "Sector with the same name already exists."));

            return TransactionResult<BusinessSector>.Success(businessSector);

        }

        public async Task<IList<BusinessSector>> FindBusinessSectorsByIdsAsync (params string[] sectorIds)
            => await store.BusinessSector.FindByIdsAsync(sectorIds);

        public async Task<TransactionResult> DeleteBusinessSectorsAsync (params BusinessSector[] sectors)
        {
            if (sectors.Length == 0)
                return TransactionResult.Success;

            return await store.BusinessSector.DeleteAsync(sectors)
                ? TransactionResult.Success
                : TransactionResult.Failure();
        }

        public async Task<BusinessSector?> FindBusinessSectorByIdAsync (string id)
         => await store.BusinessSector.FindByIdAsync(id);

        public async Task<TransactionResult<BusinessSector>> UpdateBusinessSectorAsync (BusinessSector sector)
        {
            var result = await store.BusinessSector.UpdateAsync(sector);

            if (result == null)
                return TransactionResult<BusinessSector>.Failure();
            else
                return TransactionResult<BusinessSector>.Success(result);
        }

        public async Task<IList<BusinessSector>> FindBusinessSectorsAsync ()
            => await store.BusinessSector.FindAllAsync();

    }
}
