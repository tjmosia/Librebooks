using Moskit.Data;

namespace Moskit.Providers.Stores
{
    public abstract class StoreBase (AppDbContext context)
    {
        protected readonly AppDbContext context = context;
    }
}
