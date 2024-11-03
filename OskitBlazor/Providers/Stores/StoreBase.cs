using OskitBlazor.Data;

namespace OskitBlazor.Providers.Stores
{
    public abstract class StoreBase (AppDbContext context)
    {
        protected readonly AppDbContext context = context;
    }
}
