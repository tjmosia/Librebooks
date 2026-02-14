using Librebooks.Areas.Admin.Services;
using Librebooks.Areas.Admin.Services.SubStores;
using Librebooks.Areas.Inventory.Services;
//using Librebooks.Areas.Accounting.Services;
//using Librebooks.Areas.Customers.Services;
using Librebooks.Areas.Suppliers.Services;
using Librebooks.Core.EFCore;

namespace Librebooks.Areas;

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
		services.AddScoped<CompanyNumberStore>();
		services.AddScoped<BusinessSectorStore>();

		//services.AddScoped<ICustomerManager, CustomerManager>();
		services.AddScoped<ISupplierManager, SupplierManager>();
		services.AddScoped<IItemManager, ItemManager>();
		services.AddScoped<ISystemManager, SystemManager>();
		//services.AddScoped<IAccountingManager, AccountingManager>();
	}
}
