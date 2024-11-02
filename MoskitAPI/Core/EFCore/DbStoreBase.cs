using Moskit.Data;
using Moskit.Extensions.Identity;

namespace Moskit.Core.EFCore
{
	public abstract class DbStoreBase (AppDbContext context, ILogger<DbStoreBase> logger, IdentityErrorDescriberExt errorDescriber)
	{
		protected readonly AppDbContext context = context;
		protected readonly ILogger<DbStoreBase> logger = logger;
		protected readonly IdentityErrorDescriberExt errorDescriber = errorDescriber;

		// protected bool disposed { get; set; }
		// protected abstract void ThrowIfDisposed();
	}
}