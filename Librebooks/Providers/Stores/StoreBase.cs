using Librebooks.Core.Constants;
using Librebooks.CoreLib.Operations;
using Librebooks.Data;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Providers.Stores;

public abstract class StoreBase (AppDbContext context, ILogger<StoreBase>? logger = null)
{
	protected readonly AppDbContext context = context;
	protected readonly ILogger<StoreBase>? logger = logger;

	private readonly Error ConcurrencyError = new(description: "Something went wrong. Please try agian.");
	public static bool IsForeignKeyViolation (Exception ex)
		=> ex is DbUpdateException &&
			ex.InnerException != null &&
			ex.InnerException.Message.Contains(DbUpdateErrors.ForeignKeyConstaint, StringComparison.InvariantCultureIgnoreCase);

	public static bool IsUniqueKeyConstaint (Exception ex)
		=> ex is DbUpdateException && ex.InnerException != null &&
			ex.InnerException.Message.Contains(DbUpdateErrors.UniqueIndex, StringComparison.InvariantCultureIgnoreCase);
}
