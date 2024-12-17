using LibreBooks.Data;

namespace LibreBooks.Core.EFCore
{
    public abstract class DbStoreBase
        (AppDbContext context, ILogger? logger = null, DbErrorDescriber? dbErrorDescriber = null)
    {
        protected readonly AppDbContext context = context;
        protected readonly ILogger? logger = logger;
        protected readonly DbErrorDescriber? dbErrorDescriber = dbErrorDescriber;

        // protected bool disposed { get; set; }
        // protected abstract void ThrowIfDisposed();
    }
}