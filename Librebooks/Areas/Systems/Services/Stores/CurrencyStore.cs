using Librebooks.Core.EFCore;
using Librebooks.CoreLib.Operations;
using Librebooks.Data;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Systems.Services.Stores;

public class CurrencyStore (AppDbContext context, ILogger<CurrencyStore> logger) : DbStoreBase(context, logger)
{
	public async Task<Currency?> FindByCodeAsync (string code, CancellationToken cancellationToken = default)
		=> await context!.Currencies!.FindAsync([code], cancellationToken);

	public async Task<Currency?> FindByNameAsync (string name, CancellationToken cancellationToken = default)
		=> await context!.Currencies!
			.Where(p => string.Equals(p.Name, name, StringComparison.CurrentCultureIgnoreCase))
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<IList<Currency>> FindAllAsync (CancellationToken cancellationToken = default)
		=> await context!.Currencies!.ToListAsync(cancellationToken);


	public async Task<Result<Currency>> CreateAsync (Currency currency, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = await context!.Currencies!.AddAsync(currency, cancellationToken);
			await context.SaveChangesAsync(cancellationToken);

			return Result<Currency>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			IList<Error> errors = [];
			if (IsUniqueKeyConstaint(ex))
				errors.Add(new(nameof(Currency.Name), "Name is already taken."));


			if (errors.Any())
				errors.Add(GeneralError);

			return Result<Currency>.Failure([.. errors]);
		}
	}

	public async Task<Result<Currency>> UpdateAsync (Currency currency, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = context!.Currencies!.Update(currency);
			await context.SaveChangesAsync(cancellationToken);

			return Result<Currency>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			IList<Error> errors = [];
			if (IsUniqueKeyConstaint(ex))
				errors.Add(new(nameof(Currency.Name), "Name is already taken."));

			if (errors.Any())
				errors.Add(GeneralError);

			return Result<Currency>.Failure([.. errors]);
		}
	}

	public async Task<Result> DeleteAsync (Currency[] currencies, CancellationToken cancellationToken = default)
	{
		try
		{
			context!.Currencies!.RemoveRange(currencies);
			await context.SaveChangesAsync(cancellationToken);
			return Result.Success;
		}
		catch (Exception ex)
		{
			IList<Error> errors = [];
			if (IsForeignKeyViolation(ex))
				errors.Add(new("", currencies.Length > 1 ? "One or more currencies are currently in use." : "Currency is currently in use."));

			if (errors.Any())
				errors.Add(GeneralError);

			return Result.Failure([.. errors]);
		}
	}
}
