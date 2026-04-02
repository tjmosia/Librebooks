using Librebooks.Core.Constants;
using Librebooks.Core.EFCore;
using Librebooks.CoreLib.Operations;
using Librebooks.Data;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Systems.Services.Stores;

public class ShippingMethodStore (AppDbContext context, ILogger<ShippingMethodStore>? logger) : DbStoreBase(context, logger)
{
	public async Task<Result<ShippingMethod>> CreateAsync (ShippingMethod method, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = await context!.ShippingMethods!.AddAsync(method, cancellationToken);
			await context.SaveChangesAsync(cancellationToken);
			return Result<ShippingMethod>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			IList<Error> errors = [];
			if (IsUniqueKeyConstaint(ex))
				errors.Add(new(nameof(PaymentTerm.Name), "Name is already taken."));

			if (errors.Any())
				errors.Add(GeneralError);

			return Result<ShippingMethod>.Failure();
		}
	}

	public async Task<Result<ShippingMethod>> UpdateAsync (ShippingMethod method, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = context!.ShippingMethods!.Update(method);
			await context.SaveChangesAsync(cancellationToken);
			return Result<ShippingMethod>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			IList<Error> errors = [];
			if (IsUniqueKeyConstaint(ex))
				errors.Add(new(nameof(PaymentTerm.Name), "Shipping method already exists."));

			if (errors.Any())
				errors.Add(GeneralError);

			return Result<ShippingMethod>.Failure();
		}
	}

	public async Task<ShippingMethod?> FindByIdAsync (int id, CancellationToken cancellationToken = default)
		=> await context!.ShippingMethods!.FindAsync([id], cancellationToken);

	public async Task<ShippingMethod?> FindByNameAsync (string name, CancellationToken cancellationToken = default)
		=> await context!.ShippingMethods!
			.Where(p => string.Equals(p.Name, name, StringComparison.CurrentCultureIgnoreCase))
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<Result> DeleteAsync (ShippingMethod[] methods, CancellationToken cancellationToken = default)
	{
		try
		{
			context!.ShippingMethods!.RemoveRange(methods);
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
				errors.Add(new(description: methods.Length > 1 ? "One of more shipping methods are currently in use." : "Shipping method is currently in use."));
			}

			return Result.Failure([.. errors]);
		}
	}

	public async Task<IList<ShippingMethod>> FindAllAsync (CancellationToken cancellationToken = default)
		=> await context!.ShippingMethods!.ToListAsync(cancellationToken);
}
