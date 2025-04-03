using Librebooks.Data;

namespace Librebooks.Providers.Stores
{
    public abstract class StoreBase (AppDbContext context)
    {
        protected readonly AppDbContext context = context;
    }
}
