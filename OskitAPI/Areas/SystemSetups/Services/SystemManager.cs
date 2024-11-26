using Microsoft.Extensions.Caching.Distributed;

using MacbooksAPI.Core.EFCore;
using MacbooksAPI.CoreLib.Operations;
using MacbooksAPI.Models.Entity.SystemSpace;

namespace MacbooksAPI.Areas.SystemSetups.Services
{
    public class SystemManager (SystemStore? systemStore, IDistributedCache? cache, DbErrorDescriber? dbErrorDescriber, ILogger<SystemManager>? logger)
        : ISystemManager
    {
        private readonly SystemStore? systemStore = systemStore;
        private readonly DbErrorDescriber? dbErrorDescriber = dbErrorDescriber;
        private readonly IDistributedCache? cache = cache;
        private readonly ILogger<SystemManager>? logger = logger;

        private readonly string COMP_NUM_PREFIX = nameof(COMP_NUM_PREFIX);
        private readonly string COMP_NUM_FORMAT = nameof(COMP_NUM_FORMAT);

        /******************************************************************
         * SystemCompanyNumber Manager Actions
         ******************************************************************/
        public async Task<string?> GetCompanyNumberParamsFromCacheAsync ()
            => await cache!.GetStringAsync(COMP_NUM_PREFIX);

        public async Task<TransactionResult<SystemCompanyNumber>> InitializeAsync (long nextNumber = 1, string? numberPrefix = null, string? numberFormat = null)
        {
            var setup = await systemStore!.CompanyNumber!.CreateAsync(
                new SystemCompanyNumber
                {
                    NumberFormat = numberFormat,
                    NumberPrefix = numberPrefix,
                    NumberNext = nextNumber
                });

            await SyncCompanyNumberParamsToCacheAsync(setup.NumberPrefix!, setup.NumberFormat!);

            return TransactionResult<SystemCompanyNumber>
                    .Success(setup);
        }

        public async Task<long> GenerateNewCompanyNumberAsync ()
        {
            var setup = await systemStore!.CompanyNumber!.CurrentAsync();

            if (setup == null)
                setup = (await InitializeAsync()).Model;

            await systemStore!.CompanyNumber.UpdateAsync(setup!);
            return setup!.NumberNext;
        }

        public async Task<TransactionResult> UpdateCompanyNumberParamsAsync (string prefix, string numberFormat)
        {
            var setup = await systemStore!.CompanyNumber!.CurrentAsync();

            setup!.NumberPrefix = prefix;
            setup.NumberFormat = numberFormat;
            var result = await systemStore!.CompanyNumber.UpdateAsync(setup!);
            await SyncCompanyNumberParamsToCacheAsync(result.NumberPrefix!, result.NumberFormat!);

            return TransactionResult.Success;
        }

        private async Task SyncCompanyNumberParamsToCacheAsync (string? numPrefix, string? numFormat)
        {
            await cache!.SetStringAsync(COMP_NUM_FORMAT, numFormat!);
            await cache!.SetStringAsync(COMP_NUM_PREFIX, numPrefix!);
        }

        private async Task DeleteCompanyNumberParamsFromCacheAsync ()
        {
            await cache.RemoveAsync(COMP_NUM_FORMAT);
            await cache.RemoveAsync(COMP_NUM_PREFIX);
        }

        /******************************************************************
         * COUNTRY Store Manager Actions
         ******************************************************************/
        public async Task<TransactionResult<Country>> AddCountryAsync (Country country)
        {
            var result = await systemStore!.Countries!.CreateAsync(country);

            if (result != null)
                return TransactionResult<Country>.Success(result);
            return TransactionResult<Country>.Failure();
        }

        public async Task<TransactionResult> DeleteCountryAsync (params Country[] countries)
        {
            await systemStore!.Countries!.DeleteAsync(countries);
            return TransactionResult.Success;
        }

        public async Task<Country?> GetCountryByCodeAsync (string code)
        {
            ArgumentNullException.ThrowIfNull(code, nameof(code));
            return await systemStore!.Countries!.FindByCodeAsync(code);
        }

        public async Task<TransactionResult<Country>> UpdateCountryAsync (Country country)
        {
            var result = await systemStore!.Countries!.UpdateAsync(country);

            if (result != null)
                return TransactionResult<Country>.Success(result);
            else
                return TransactionResult<Country>.Failure();
        }

        /******************************************************************
         * CURRENCY Store Manager Actions
         ******************************************************************/
        public async Task<TransactionResult<Currency>> AddCurrencyAsync (Currency currency)
        {
            var result = await systemStore!.Currencies!.CreateAsync(currency);

            if (result != null)
                return TransactionResult<Currency>.Success(result!);
            return TransactionResult<Currency>.Failure();
        }

        public async Task<TransactionResult> DeleteCurrencyAsync (params Currency[] currencies)
        {
            await systemStore!.Currencies!.DeleteAsync(currencies);
            return TransactionResult.Success;
        }

        public async Task<Currency?> GetCurrencyByCodeAsync (string code)
            => await systemStore!.Currencies!.FindByCodeAsync(code);

        public async Task<TransactionResult<Currency>> UpdateCurrencyAsync (Currency currency)
        {
            var result = await systemStore!.Currencies!.UpdateAsync(currency);

            if (result != null)
                return TransactionResult<Currency>.Success(result);
            else
                return TransactionResult<Currency>.Failure();
        }

        /******************************************************************
         * DATE_FORMAT Store Manager Actions
         ******************************************************************/
        public async Task<TransactionResult<DateFormat>> AddDateFormatAsync (DateFormat dateFormat)
        {
            var result = await systemStore!.DateFormats!.CreateAsync(dateFormat);

            if (result != null)
                return TransactionResult<DateFormat>.Success(result);
            return TransactionResult<DateFormat>.Failure();
        }

        public async Task<TransactionResult> DeleteDateFormatAsync (params DateFormat[] dateFormats)
        {
            await systemStore!.DateFormats!.DeleteAsync(dateFormats);
            return TransactionResult.Success;
        }

        public async Task<DateFormat?> GetDateFormatByIdAsync (string id)
            => await systemStore!.DateFormats!.FindByIdAsync(id);

        public async Task<TransactionResult<DateFormat>> UpdateDateFormatAsync (DateFormat dateFormat)
        {
            var result = await systemStore!.DateFormats!.UpdateAsync(dateFormat);

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
            var result = await systemStore!.PaymentMethods!.CreateAsync(paymentMethod);

            if (result != null)
                return TransactionResult<PaymentMethod>.Success(result);
            return TransactionResult<PaymentMethod>.Failure();
        }

        public async Task<TransactionResult> DeletePaymentMethodAsync (params PaymentMethod[] paymentMethods)
        {
            await systemStore!.PaymentMethods!.DeleteAsync(paymentMethods);
            return TransactionResult.Success;
        }

        public Task<PaymentMethod?> GetPaymentMethodByIdAsync (string id)
            => systemStore!.PaymentMethods!.FindByIdAsync(id);

        public async Task<TransactionResult<PaymentMethod>> UpdatePaymentMethodAsync (PaymentMethod paymentMethod)
        {
            var result = await systemStore!.PaymentMethods!.UpdateAsync(paymentMethod);

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
            var result = await systemStore!.PaymentTerms!.CreateAsync(paymentTerm);

            if (result != null)
                return TransactionResult<PaymentTerm>.Success(result);
            return TransactionResult<PaymentTerm>.Failure();
        }

        public async Task<TransactionResult> DeletePaymentTermAsync (params PaymentTerm[] paymentTerms)
        {
            await systemStore!.PaymentTerms!.DeleteAsync(paymentTerms);
            return TransactionResult.Success;
        }

        public async Task<PaymentTerm?> GetPaymentTermByIdAsync (string id)
            => await systemStore!.PaymentTerms!.FindByIdAsync(id);

        public async Task<TransactionResult<PaymentTerm>> UpdatePaymentTermAsync (PaymentTerm paymentTerm)
        {
            var result = await systemStore!.PaymentTerms!.UpdateAsync(paymentTerm);

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
            var result = await systemStore!.ShippingMethods!.CreateAsync(shippingMethod);

            if (result != null)
                return TransactionResult<ShippingMethod>.Success(result);
            return TransactionResult<ShippingMethod>.Failure();
        }

        public async Task<TransactionResult> DeleteShippingMethodAsync (params ShippingMethod[] shippingMethods)
        {
            await systemStore!.ShippingMethods!.DeleteAsync(shippingMethods);
            return TransactionResult.Success;
        }

        public async Task<ShippingMethod?> GetShippingMethodByIdAsync (string id)
            => await systemStore!.ShippingMethods!.FindByIdAsync(id);

        public async Task<TransactionResult<ShippingMethod>> UpdateShippingMethodAsync (ShippingMethod shippingMethod)
        {
            var result = await systemStore!.ShippingMethods!.UpdateAsync(shippingMethod);

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
            await systemStore!.ShippingTerms!.DeleteAsync(shippingTerms);
            return TransactionResult.Success;
        }

        public async Task<ShippingTerm?> GetShippingTermByIdAsync (string id)
            => await systemStore!.ShippingTerms!.FindByIdAsync(id);


        public async Task<TransactionResult<ShippingTerm>> UpdateShippingTermAsync (ShippingTerm shippingTerm)
        {
            var result = await systemStore!.ShippingTerms!.UpdateAsync(shippingTerm);

            if (result != null)
                return TransactionResult<ShippingTerm>.Success(result);

            return TransactionResult<ShippingTerm>.Failure();
        }

        /******************************************************************
         * VAT Store Manager Actions
         ******************************************************************/
        public async Task<TransactionResult<ValueAddedTax>> AddVATAsync (ValueAddedTax vat)
        {
            var result = await systemStore!.ValueAddedTax!.CreateAsync(vat);

            if (result != null)
                return TransactionResult<ValueAddedTax>.Success(result);

            return TransactionResult<ValueAddedTax>.Failure();
        }

        public async Task<TransactionResult> DeleteVATAsync (params ValueAddedTax[] vats)
        {
            await systemStore!.ValueAddedTax!.DeleteAsync(vats);
            return TransactionResult.Success;
        }

        public async Task<ValueAddedTax?> GetVATByIdAsync (string id)
            => await systemStore!.ValueAddedTax!.FindByIdAsync(id);

        public async Task<TransactionResult<ValueAddedTax>> UpdateVATAsync (ValueAddedTax vat)
        {
            var result = await systemStore!.ValueAddedTax!.UpdateAsync(vat);

            if (result != null)
                return TransactionResult<ValueAddedTax>.Success(result);

            return TransactionResult<ValueAddedTax>.Failure();
        }
    }
}
