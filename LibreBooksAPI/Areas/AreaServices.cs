using LibreBooks.Areas.Inventory.Services;
using LibreBooks.Areas.SystemSetups.Services;
using LibreBooks.Areas.SystemSetups.Services.SubStores;
using LibreBooks.Core.EFCore;

using LibreBooksAPI.Areas.Accounting.Services;
using LibreBooksAPI.Areas.Customers.Services;
using LibreBooksAPI.Areas.Suppliers.Services;

namespace LibreBooksAPI.Areas
{
    public static class AreaServices
    {
        public static void ConfigureAll (IServiceCollection services)
        {
            services.AddSingleton<DbErrorDescriber>();
            services.AddScoped<SystemStore>();
            services.AddScoped<ISystemManager, SystemManager>();
            services.AddScoped<ItemStore>();
            services.AddScoped<IItemManager, ItemManager>();
            services.AddScoped<CountryStore>();
            services.AddScoped<CurrencyStore>();
            services.AddScoped<DateFormatStore>();
            services.AddScoped<PaymentMethodStore>();
            services.AddScoped<PaymentTermStore>();
            services.AddScoped<ShippingTermStore>();
            services.AddScoped<ShippingMethodStore>();
            services.AddScoped<TaxTypeStore>();
            services.AddScoped<SystemCompanyNumberStore>();
            services.AddScoped<BusinessSectorStore>();

            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<ISupplierManager, SupplierManager>();
            services.AddScoped<IItemManager, ItemManager>();
            services.AddScoped<ISystemManager, SystemManager>();
            services.AddScoped<IAccountingManager, AccountingManager>();
        }
    }
}
