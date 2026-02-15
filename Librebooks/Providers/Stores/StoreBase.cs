using Librebooks.Data;

namespace Librebooks.Providers.Stores
{
	public abstract class StoreBase (AppDbContext context, ILogger<StoreBase>? logger = null)
	{
		protected readonly AppDbContext context = context;
		protected readonly ILogger<StoreBase>? logger = logger;
	}
}
