using LibreBooks.Data;

namespace LibreBooks.Providers.Stores
{
    public abstract class StoreBase (AppDbContext context)
    {
        protected readonly AppDbContext context = context;
    }
}
