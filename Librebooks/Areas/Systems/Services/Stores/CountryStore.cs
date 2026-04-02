using Librebooks.Core.EFCore;
using Librebooks.CoreLib.Operations;
using Librebooks.Data;
using Librebooks.Models.Entity.SystemSpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Systems.Services.Stores;

public class CountryStore (AppDbContext context, ILogger<CountryStore> logger) : DbStoreBase(context, logger)
{
	public async Task<Country?> FindByIdAsync (int id, CancellationToken cancellationToken = default)
		=> await context!.Countries!.FindAsync([id], cancellationToken);

	public async Task<IList<Country>> FindAllAsync (CancellationToken cancellationToken = default)
		=> await context!.Countries!.ToListAsync(cancellationToken);

	public async Task<IList<Country>> FindByIdsAsync (int[] ids, CancellationToken cancellationToken = default)
		=> await context!.Countries!.Where(p => ids.Contains(p.Id)).ToListAsync(cancellationToken);

	public async Task<IList<Country>> FindAllByIdAsync (int[] ids, CancellationToken cancellationToken = default)
		=> [.. (await FindAllAsync(cancellationToken)).Where(c => ids.Contains(c.Id))];

	public async Task<Result<Country>> CreateAsync (Country country, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = await context!.Countries!.AddAsync(country, cancellationToken);
			await context.SaveChangesAsync(cancellationToken);
			return Result<Country>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			IList<Error> errors = [];

			if (IsUniqueKeyConstaint(ex))
				errors.Add(new(nameof(Country.Name), "Name is already taken."));

			if (!errors.Any())
				errors.Add(GeneralError);

			return Result<Country>.Failure([.. errors]);
		}
	}

	public async Task<Result<Country>> UpdateAsync (Country country, CancellationToken cancellationToken = default)
	{
		try
		{
			country.RowVersion = GenerateRowVersion();
			var result = context!.Countries!.Update(country);
			await context.SaveChangesAsync(cancellationToken);

			return Result<Country>.Success(result.Entity);
		}
		catch (DbUpdateException ex)
		{
			IList<Error> errors = [];

			if (IsUniqueKeyConstaint(ex))
				errors.Add(new(nameof(Country.Name), "Name is already taken."));

			if (!errors.Any())
				errors.Add(GeneralError);

			return Result<Country>.Failure([.. errors]);
		}
	}

	public async Task<Result> DeleteAsync (Country[] countries, CancellationToken cancellationToken = default)
	{
		try
		{
			context!.Countries!.RemoveRange(countries);
			await context.SaveChangesAsync(cancellationToken);
			return Result.Success;
		}
		catch (DbUpdateException ex)
		{
			IList<Error> errors = [];

			if (IsForeignKeyViolation(ex))
				errors.Add(new(description: countries.Length > 1 ? "One or more countries are currently in use." : "Country is currently in use."));

			if (!errors.Any())
				errors.Add(GeneralError);

			return Result.Failure([.. errors]);
		}
	}


}
