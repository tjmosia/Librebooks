using Librebooks.Data;

namespace Librebooks.Core.EFCore
{
	public abstract class DbStoreBase
		(AppDbContext context, ILogger<DbStoreBase>? logger = null, DbErrorDescriber? dbErrorDescriber = null)
	{
		protected readonly AppDbContext context = context;
		protected readonly ILogger? logger = logger;
		protected readonly DbErrorDescriber? dbErrorDescriber = dbErrorDescriber;
		protected string GenerateRowVersion ()
			=> Guid.NewGuid().ToString("N").ToUpper();
	}
}