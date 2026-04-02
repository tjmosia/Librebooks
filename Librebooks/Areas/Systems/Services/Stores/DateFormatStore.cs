using Librebooks.Core.EFCore;
using Librebooks.CoreLib.Operations;
using Librebooks.Data;
using Librebooks.Models.Entity.SystemSpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Systems.Services.Stores
{
	public class DateFormatStore (AppDbContext context, ILogger<DateFormatStore> logger) : DbStoreBase(context, logger)
	{
		public async Task<Result<DateFormat>> CreateAsync (DateFormat dateFormat, CancellationToken cancellationToken = default)
		{
			try
			{
				var result = await context!.DateFormats!.AddAsync(dateFormat, cancellationToken);
				await context.SaveChangesAsync(cancellationToken);
				return Result<DateFormat>.Success(result.Entity);
			}
			catch (Exception ex)
			{
				IList<Error> errors = [];
				if (IsUniqueKeyConstaint(ex))
					errors.Add(Error.Create(nameof(DateFormat.Format), "Format is already taken."));

				if (errors.Any())
					errors.Add(GeneralError);

				return Result<DateFormat>.Failure([.. errors]);
			}
		}

		public async Task<Result<DateFormat>> UpdateAsync (DateFormat dateFormat, CancellationToken cancellationToken = default)
		{
			try
			{
				var result = context!.DateFormats!.Update(dateFormat);
				await context.SaveChangesAsync(cancellationToken);
				return Result<DateFormat>.Success(result.Entity);
			}
			catch (Exception ex)
			{
				IList<Error> errors = [];
				if (IsUniqueKeyConstaint(ex))
					errors.Add(Error.Create(nameof(DateFormat.Format), "Format is already taken."));

				if (errors.Any())
					errors.Add(GeneralError);

				return Result<DateFormat>.Failure([.. errors]);
			}
		}

		public async Task<DateFormat?> FindByIdAsync (int id, CancellationToken cancellationToken = default)
			=> await context!.DateFormats!.FindAsync([id], cancellationToken);

		public async Task<Result> DeleteAsync (DateFormat[] dateFormats, CancellationToken cancellationToken = default)
		{
			try
			{
				context!.DateFormats!.RemoveRange(dateFormats);
				var result = await context.SaveChangesAsync(cancellationToken);
				return Result.Success;
			}
			catch (Exception ex)
			{
				IList<Error> errors = [];

				if (IsForeignKeyViolation(ex))
					errors.Add(Error.Create("", dateFormats.Length > 1 ? "One or more date formats are currently in use." : "The date format is curently in use."));

				if (errors.Any())
					errors.Add(GeneralError);

				return Result.Failure([.. errors]);
			}
		}

		public async Task<IList<DateFormat>> FindAllAsync (CancellationToken cancellationToken = default)
			=> await context!.DateFormats!.ToListAsync(cancellationToken);
	}
}
