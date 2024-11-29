namespace OskitBlazor.Providers.Companies
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
