using OskitAPI.Data;

namespace OskitAPI.Providers.Stores
{
    public abstract class StoreBase (AppDbContext context)
    {
        protected readonly AppDbContext context = context;
    }
}
