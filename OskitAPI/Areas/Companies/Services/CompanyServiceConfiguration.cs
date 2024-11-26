namespace MacbooksAPI.Areas.Companies.Services
{
    public class CompanyServiceConfiguration
    {
        public static void Configure (IServiceCollection services)
        {
            services.AddScoped<CompanyStore>()
                .AddScoped<ICompanyManager, CompanyManager>();
        }
    }
}
