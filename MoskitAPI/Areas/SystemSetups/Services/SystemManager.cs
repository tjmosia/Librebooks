using Microsoft.Extensions.Caching.Distributed;

using Moskit.CoreLib.Operations;
using Moskit.Models.Entity.SystemSpace;

namespace Moskit.Areas.SystemSetups.Services
{
    public class SystemManager (SystemStore systemStore, IDistributedCache cache) : ISystemManager
    {
        private readonly SystemStore systemStore = systemStore;
        private readonly IDistributedCache cache = cache;

        private readonly string COMP_NUM_PREFIX = nameof(COMP_NUM_PREFIX);
        private readonly string COMP_NUM_FORMAT = nameof(COMP_NUM_FORMAT);

        /******************************************************************
         * SystemCompanyNumber Manager Actions
         ******************************************************************/
        public async Task<string?> GetCompanyNumberParamsFromCacheAsync ()
            => await cache.GetStringAsync(COMP_NUM_PREFIX);

        public async Task<TransactionResult<SystemCompanyNumber>> InitializeAsync (long nextNumber = 1, string? numberPrefix = null, string? numberFormat = null)
        {
            var setup = await systemStore.CompanyNumber.CreateAsync(
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
            var setup = await systemStore.CompanyNumber.CurrentAsync();

            if (setup == null)
                setup = (await InitializeAsync()).Model;

            await systemStore.CompanyNumber.UpdateAsync(setup!);
            return setup!.NumberNext;
        }

        public async Task<TransactionResult> UpdateCompanyNumberParamsAsync (string prefix, string numberFormat)
        {
            var setup = await systemStore.CompanyNumber.CurrentAsync();

            setup!.NumberPrefix = prefix;
            setup.NumberFormat = numberFormat;
            var result = await systemStore.CompanyNumber.UpdateAsync(setup!);
            await SyncCompanyNumberParamsToCacheAsync(result.NumberPrefix!, result.NumberFormat!);

            return TransactionResult.Success;
        }

        private async Task SyncCompanyNumberParamsToCacheAsync (string? numPrefix, string? numFormat)
        {
            await cache.SetStringAsync(COMP_NUM_FORMAT, numFormat!);
            await cache.SetStringAsync(COMP_NUM_PREFIX, numPrefix!);
        }

        private async Task RemoveCompanyNumberParamsFromCacheAsync ()
        {
            await cache.RemoveAsync(COMP_NUM_FORMAT);
            await cache.RemoveAsync(COMP_NUM_PREFIX);
        }

        public async Task<TransactionResult<Country>> AddCountryAsync (Country country)
        {
            ArgumentNullException.ThrowIfNull(country, nameof(country));
            return await systemStore.Countries.CreateAsync(country);
        }

        public Task<TransactionResult> RemoveCountryAsync (params Country[] paymentMethods)
        {
            throw new NotImplementedException();
        }

        public async Task<Country?> GetCountryByCodeAsync (string code)
        {
            ArgumentNullException.ThrowIfNull(code, nameof(code));
            return await systemStore.Countries.FindByCodeAsync(code);
        }

        public Task<TransactionResult<Country>> UpdateCountryAsync (Country country)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<Currency>> AddCurrencyAsync (Currency currency)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> RemoveCurrencyAsync (params Currency[] paymentMethods)
        {
            throw new NotImplementedException();
        }

        public Task<Country?> GetCurrencyByCodeAsync (string code)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<Currency>> UpdateCurrencyAsync (Currency currency)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<DateFormat>> AddDateFormatAsync (DateFormat dateFormat)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> RemoveDateFormatAsync (params DateFormat[] paymentMethods)
        {
            throw new NotImplementedException();
        }

        public Task<DateFormat?> GetDateFormatByIdAsync (string id)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<DateFormat>> UpdateDateFormatAsync (DateFormat dateFormat)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<PaymentMethod>> AddPaymentMethodAsync (PaymentMethod paymentMethod)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> RemovePaymentMethodAsync (params PaymentMethod[] paymentMethods)
        {
            throw new NotImplementedException();
        }

        public Task<Country?> GetPaymentMethodByIdAsync (string id)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<PaymentMethod>> UpdatePaymentMethodAsync (PaymentMethod paymentMethod)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<PaymentTerm>> AddPaymentTermAsync (PaymentTerm paymentTerm)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> RemovePaymentTermAsync (params PaymentTerm[] paymentTerms)
        {
            throw new NotImplementedException();
        }

        public Task<Country?> GetPaymentTermByIdAsync (string id)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<PaymentTerm>> UpdatePaymentTermAsync (PaymentTerm paymentTerm)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<ShippingMethod>> AddShippingMethodAsync (ShippingMethod shippingMethod)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> RemoveShippingMethodAsync (params ShippingMethod[] shippingMethods)
        {
            throw new NotImplementedException();
        }

        public Task<Country?> GetShippingMethodByIdAsync (string id)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<ShippingMethod>> UpdateShippingMethodAsync (ShippingMethod shippingMethod)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<ShippingTerm>> AddShippingTermAsync (ShippingTerm shippingTerm)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> RemoveShippingTermAsync (params ShippingTerm[] shippingTerms)
        {
            throw new NotImplementedException();
        }

        public Task<Country?> GetShippingTermByIdAsync (string id)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<ShippingTerm>> UpdateShippingTermAsync (ShippingTerm shippingTerm)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<ValueAddedTax>> AddVATAsync (ValueAddedTax vat)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult> RemoveVATAsync (params ValueAddedTax[] vat)
        {
            throw new NotImplementedException();
        }

        public Task<ValueAddedTax?> GetVATByIdAsync (string id)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionResult<ValueAddedTax>> UpdateVATAsync (ValueAddedTax vat)
        {
            throw new NotImplementedException();
        }
    }
}
