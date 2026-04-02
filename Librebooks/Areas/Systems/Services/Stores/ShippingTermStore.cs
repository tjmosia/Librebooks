using Librebooks.Core.Constants;
using Librebooks.Core.EFCore;
using Librebooks.CoreLib.Operations;
using Librebooks.Data;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Systems.Services.Stores;

public class ShippingTermStore (AppDbContext context, ILogger<ShippingTermStore> logger) : DbStoreBase(context, logger)
{
	public async Task<Result<ShippingTerm>> CreateAsync (ShippingTerm term, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = await context!.ShippingTerms!.AddAsync(term, cancellationToken);
			await context.SaveChangesAsync(cancellationToken);
			return Result<ShippingTerm>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			IList<Error> errors = [];
			if (ex is DbUpdateException && ex.InnerException != null &&
				ex.InnerException.Message.Contains(DbUpdateErrors.UniqueIndex, StringComparison.InvariantCultureIgnoreCase))
				errors.Add(new(nameof(PaymentTerm.Name), "Name is already used."));

			return Result<ShippingTerm>.Failure([.. errors]);
		}
	}

	public async Task<Result<ShippingTerm>> UpdateAsync (ShippingTerm term, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = context!.ShippingTerms!.Update(term);
			await context.SaveChangesAsync(cancellationToken);
			return Result<ShippingTerm>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			IList<Error> errors = [];
			if (ex is DbUpdateException && ex.InnerException != null &&
				ex.InnerException.Message.Contains(DbUpdateErrors.UniqueIndex, StringComparison.InvariantCultureIgnoreCase))
				errors.Add(new(nameof(PaymentTerm.Name), "Shipping term already exists."));
			if (ex is DbUpdateConcurrencyException)
				errors.Add(new(description: "Something went wrong. Please try again."));

			return Result<ShippingTerm>.Failure([.. errors]);
		}
	}

	public async Task<ShippingTerm?> FindByIdAsync (int id, CancellationToken cancellationToken = default)
		=> await context!.ShippingTerms!.FindAsync([id], cancellationToken);

	public async Task<ShippingTerm?> FindByNameAsync (string name, CancellationToken cancellationToken = default)
		=> await context!.ShippingTerms!
			.Where(p => string.Equals(p.Name, name, StringComparison.CurrentCultureIgnoreCase))
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<Result> DeleteAsync (ShippingTerm[] terms, CancellationToken cancellationToken = default)
	{
		try
		{
			context!.ShippingTerms!.RemoveRange(terms);
			await context.SaveChangesAsync(cancellationToken);
			return Result.Success;
		}
		catch (Exception ex)
		{
			IList<Error> errors = [];
			if (ex is DbUpdateException &&
				ex.InnerException != null &&
				ex.InnerException.Message.Contains(DbUpdateErrors.ForeignKeyConstaint, StringComparison.InvariantCultureIgnoreCase))
			{
				errors.Add(new(description: terms.Length > 1 ? "One of more shipping terms are currently in use." : "Shipping term is currently in use."));
			}

			return Result.Failure([.. errors]);
		}
	}

	public async Task<IList<ShippingTerm>> FindAllAsync (CancellationToken cancellationToken = default)
		=> await context!.ShippingTerms!.ToListAsync(cancellationToken);
}
