using OskitBlazor.Data;

namespace OskitBlazor.Core.EFCore
{
    public abstract class DbStoreBase
        (AppDbContext? context = null, ILogger? logger = null, DbErrorDescriber? dbErrorDescriber = null)
    {
        protected readonly AppDbContext? context = context;
        protected readonly ILogger? logger = logger;
        protected readonly DbErrorDescriber? dbErrorDescriber = dbErrorDescriber;

        protected bool disposed = false;

        protected virtual void ThrowIfDisposed ()
        {
            if (context == null)
                throw new ObjectDisposedException(nameof(context));
        }
    }
}