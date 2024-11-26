using MacbooksAPI.Data;

namespace MacbooksAPI.Providers.Stores
{
    public abstract class StoreBase (AppDbContext context)
    {
        protected readonly AppDbContext context = context;
    }
}
