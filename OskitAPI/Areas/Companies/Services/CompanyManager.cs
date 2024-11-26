namespace MacbooksAPI.Areas.Companies.Services
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
