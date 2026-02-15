using Librebooks.Core.EFCore;
using Librebooks.CoreLib.Operations;
using Librebooks.Data;
using Librebooks.Models.Entity.SystemSpace;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Admin.Services.SubStores
{
	public class CountryStore : DbStoreBase
	{
		public CountryStore (AppDbContext context, ILogger<CountryStore> logger)
			: base(context, logger) { }

		/// <exception cref="DbUpdateException"/>
		public async Task<TransactionResult<Country>> AddAsync (Country country)
		{
			try
			{
				var result = await context!.Country!.AddAsync(country);
				await context.SaveChangesAsync();
				return TransactionResult<Country>.Success(result.Entity);
			}
			catch (Exception ex)
			{
				IList<TransactionError> errors = [];

				if (ex is DbUpdateException && ex.InnerException is SqlException && ex.InnerException!.Message.Contains("Country.Name"))
					errors.Add(new(nameof(Country.Name), "Name is already taken."));

				if (!errors.Any())
					errors.Add(new("", "Failed to create country. Please try again."));

				return TransactionResult<Country>.Failure([.. errors]);
			}
		}

		/// <exception cref="DbUpdateException"/>
		public async Task<TransactionResult<Country>> UpdateAsync (Country country)
		{
			try
			{
				country.RowVersion = GenerateRowVersion();
				var result = context!.Country!.Update(country);
				await context.SaveChangesAsync();

				return TransactionResult<Country>.Success(result.Entity);
			}
			catch (DbUpdateException ex)
			{
				IList<TransactionError> errors = [];

				if (ex.InnerException is SqlException exception && exception.Message.Contains("Country.Name"))
					errors.Add(new(nameof(Country.Name), "Name is already taken."));

				if (!errors.Any())
					errors.Add(new("", "Failed to update the country. Please try again."));

				return TransactionResult<Country>.Failure([.. errors]);
			}
		}

		public async Task<Country?> FindByIdAsync (int id, CancellationToken cancellationToken = default)
			=> await context!.Country!.FindAsync([id], cancellationToken);

		public async Task<IList<Country>> FindAllAsync (CancellationToken cancellationToken = default)
			=> await context!.Country!.ToListAsync(cancellationToken);

		public async Task<IList<Country>> FindAllByIdAsync (int[] ids, CancellationToken cancellationToken = default)
			=> [.. ((await FindAllAsync(cancellationToken)).Where(c => ids.Contains(c.Id)))];

		public async Task<TransactionResult> DeleteAsync (params Country[] countries)
		{
			try
			{
				context!.Country!.RemoveRange(countries);
				await context.SaveChangesAsync();
				return TransactionResult.Success;
			}
			catch (DbUpdateException ex)
			{
				IList<TransactionError> errors = [];

				if (ex.InnerException is SqlException exception && exception.Message.Contains("Country.Id"))
					errors.Add(new(nameof(Country.Name), "Unable to delete country. Country is currently in use."));

				if (!errors.Any())
					errors.Add(new("", "Failed to delete country. Please try again."));

				return TransactionResult.Failure([.. errors]);
			}
		}

		public async Task<IList<Country>> FindAllAsync ()
			=> await context!.Country!.ToListAsync();

	}
}
