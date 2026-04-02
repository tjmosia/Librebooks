using Librebooks.Core.Constants;
using Librebooks.CoreLib.Operations;
using Librebooks.Data;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Core.EFCore
{
	public abstract class DbStoreBase
		(AppDbContext context, ILogger<DbStoreBase>? logger = null, DbErrorDescriber? dbErrorDescriber = null)
	{
		protected readonly AppDbContext context = context;
		protected readonly ILogger? logger = logger;
		protected readonly DbErrorDescriber? dbErrorDescriber = dbErrorDescriber;

		protected readonly Error GeneralError = new(description: "Something went wrong. Please try agian.");
		protected static bool IsForeignKeyViolation (Exception ex)
			=> ex is DbUpdateException &&
				ex.InnerException != null &&
				ex.InnerException.Message.Contains(DbUpdateErrors.ForeignKeyConstaint, StringComparison.InvariantCultureIgnoreCase);

		protected static bool IsUniqueKeyConstaint (Exception ex)
			=> ex is DbUpdateException && ex.InnerException != null &&
				ex.InnerException.Message.Contains(DbUpdateErrors.UniqueIndex, StringComparison.InvariantCultureIgnoreCase);
		protected string GenerateRowVersion ()
			=> Guid.NewGuid().ToString("N").ToUpper();
	}
}