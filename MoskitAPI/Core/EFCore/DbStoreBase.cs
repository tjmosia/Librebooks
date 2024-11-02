using OskitAPI.Data;

namespace OskitAPI.Core.EFCore
{
    public abstract class DbStoreBase
        (AppDbContext? context = null, ILogger? logger = null, DBErrorDescriber? dbErrorDescriber = null)
    {
        protected readonly AppDbContext? context = context;
        protected readonly ILogger? logger = logger;
        protected readonly DBErrorDescriber? dbErrorDescriber = dbErrorDescriber;

        // protected bool disposed { get; set; }
        // protected abstract void ThrowIfDisposed();
    }
}