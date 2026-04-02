using Librebooks.Areas.Companies.Services;
using Librebooks.Areas.Inventory.Services;
using Librebooks.Areas.Systems.Services;
using Librebooks.Areas.Systems.Services.Stores;


//using Librebooks.Areas.Accounting.Services;
//using Librebooks.Areas.Customers.Services;
using Librebooks.Core.EFCore;

namespace Librebooks.Areas;

public static class AreaServices
{
	public static void ConfigureAll (IServiceCollection services)
	{
		services.AddSingleton<DbErrorDescriber>();
		services.AddScoped<SystemsStore>();
		services.AddScoped<ISystemsManager, SystemsManager>();
		services.AddScoped<ItemStore>();
		services.AddScoped<IItemManager, ItemManager>();
		services.AddScoped<CountryStore>();
		services.AddScoped<CurrencyStore>();
		services.AddScoped<DateFormatStore>();
		services.AddScoped<PaymentMethodStore>();
		services.AddScoped<PaymentTermStore>();
		services.AddScoped<ShippingTermStore>();
		services.AddScoped<ShippingMethodStore>();
		services.AddScoped<TaxStore>();
		services.AddScoped<CompanyNumberStore>();
		services.AddScoped<BusinessSectorStore>();
		services.AddScoped<ICompanyStore, CompanyStore>();
		services.AddScoped<ICompanyManager, CompanyManager>();

	}
}
