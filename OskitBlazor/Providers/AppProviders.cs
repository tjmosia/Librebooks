using OskitBlazor.Areas.SystemSetups.Services;
using OskitBlazor.Core.EFCore;

namespace OskitBlazor.Providers
{
    public static class AppProviders
    {
        public static void ConfigureServices (IServiceCollection services)
        {
            services.AddScoped<DbErrorDescriber>();
            services.AddScoped<SystemStore>();
            services.AddScoped<ISystemManager, SystemManager>();
        }
    }
}
