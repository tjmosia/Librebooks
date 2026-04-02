using Librebooks.Core.Constants;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Core.Util;

public class DbExceptionUtils
{
	public static bool IsForeignKeyViolation (Exception ex)
		=> ex is DbUpdateException &&
			ex.InnerException != null &&
			ex.InnerException.Message.Contains(DbUpdateErrors.ForeignKeyConstaint, StringComparison.InvariantCultureIgnoreCase);

	public static bool IsUniqueKeyConstaint (Exception ex)
		=> ex is DbUpdateException && ex.InnerException != null &&
			ex.InnerException.Message.Contains(DbUpdateErrors.UniqueIndex, StringComparison.InvariantCultureIgnoreCase);
}
