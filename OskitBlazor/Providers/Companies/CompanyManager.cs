namespace OskitBlazor.Providers.Companies
{
    public class CompanyManager : ICompanyManager
    {
        private readonly CompanyStore store;

        public CompanyManager (CompanyStore store)
        {
            this.store = store;
        }
    }
}
