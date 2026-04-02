using Librebooks.Core.EFCore;
using Librebooks.Core.Util;
using Librebooks.CoreLib.Operations;
using Librebooks.Data;
using Librebooks.Models.Entity.SystemSpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Systems.Services.Stores
{
	public class PaymentMethodStore (AppDbContext context, ILogger<PaymentMethodStore> logger) : DbStoreBase(context, logger)
	{
		public async Task<PaymentMethod?> FindByIdAsync (int id, CancellationToken cancellationToken = default)
			=> await context!.PaymentMethods!.FindAsync([id], cancellationToken);

		public async Task<IList<PaymentMethod>> FindAllAsync (CancellationToken cancellationToken)
			=> await context!.PaymentMethods!.ToListAsync(cancellationToken);

		public async Task<Result<PaymentMethod>> CreateAsync (PaymentMethod method, CancellationToken cancellationToken = default)
		{
			try
			{
				var result = await context!.PaymentMethods!.AddAsync(method, cancellationToken);
				await context.SaveChangesAsync(cancellationToken);
				return Result<PaymentMethod>.Success(result.Entity);
			}
			catch (Exception ex)
			{
				IList<Error> errors = [];

				if (DbExceptionUtils.IsUniqueKeyConstaint(ex))
					errors.Add(Error.Create(nameof(PaymentMethod.Name), "Name is alreay in use."));

				if (errors.Any())
					errors.Add(GeneralError);

				return Result<PaymentMethod>.Failure([.. errors]);
			}
		}

		public async Task<Result<PaymentMethod>> UpdateAsync (PaymentMethod method, CancellationToken cancellationToken = default)
		{
			try
			{
				var result = context!.PaymentMethods!.Update(method);
				await context.SaveChangesAsync(cancellationToken);
				return Result<PaymentMethod>.Success(result.Entity);
			}
			catch (Exception ex)
			{
				IList<Error> errors = [];

				if (IsUniqueKeyConstaint(ex))
					errors.Add(Error.Create(nameof(PaymentMethod.Name), "Name is alreay in use."));

				if (errors.Any())
					errors.Add(GeneralError);

				return Result<PaymentMethod>.Failure([.. errors]);
			}
		}

		public async Task<Result> DeleteAsync (PaymentMethod[] methods, CancellationToken cancellationToken = default)
		{
			try
			{
				context!.PaymentMethods!.RemoveRange(methods);
				await context.SaveChangesAsync(cancellationToken);
				return Result.Success;
			}
			catch (Exception ex)
			{
				IList<Error> errors = [];

				if (IsForeignKeyViolation(ex))
					errors.Add(Error.Create("", methods.Length > 1 ? "Unable to delete methods that are currently in use." : "Method is currently in use."));

				if (errors.Any())
					errors.Add(GeneralError);

				return Result.Failure([.. errors]);
			}
		}
	}
}
