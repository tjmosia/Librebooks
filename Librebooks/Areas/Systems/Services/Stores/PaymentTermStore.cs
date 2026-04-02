using Librebooks.Core.EFCore;
using Librebooks.Core.Util;
using Librebooks.CoreLib.Operations;
using Librebooks.Data;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Systems.Services.Stores
{
	public class PaymentTermStore : DbStoreBase
	{
		public PaymentTermStore (AppDbContext context, ILogger<PaymentTermStore> logger)
			: base(context, logger) { }

		/// <exception cref="DbUpdateException"/>
		public async Task<Result<PaymentTerm>> CreateAsync (PaymentTerm term, CancellationToken cancellationToken = default)
		{
			try
			{
				var result = await context!.PaymentTerms!.AddAsync(term, cancellationToken);
				await context.SaveChangesAsync(cancellationToken);
				return Result<PaymentTerm>.Success(result.Entity);
			}
			catch (Exception ex)
			{
				IList<Error> errors = [];

				if (DbExceptionUtils.IsUniqueKeyConstaint(ex))
					errors.Add(new(nameof(PaymentTerm.Name), "Name is already taken."));

				if (errors.Any())
					errors.Add(GeneralError);

				return Result<PaymentTerm>.Failure([.. errors]);
			}
		}

		/// <exception cref="DbUpdateException"/>
		public async Task<Result<PaymentTerm>> UpdateAsync (PaymentTerm term, CancellationToken cancellationToken = default)
		{
			try
			{
				var result = context!.PaymentTerms!.Update(term);
				await context.SaveChangesAsync(cancellationToken);
				return Result<PaymentTerm>.Success(result.Entity);
			}
			catch (Exception ex)
			{
				IList<Error> errors = [];

				if (IsUniqueKeyConstaint(ex))
					errors.Add(new(nameof(PaymentTerm.Name), "Payment term already exists."));

				if (errors.Any())
					errors.Add(GeneralError);

				return Result<PaymentTerm>.Failure([.. errors]);
			}
		}

		public async Task<PaymentTerm?> FindByIdAsync (int id, CancellationToken cancellationToken = default)
			=> await context!.PaymentTerms!.FindAsync([id], cancellationToken);

		public async Task<Result> DeleteAsync (PaymentTerm[] terms, CancellationToken cancellationToken = default)
		{
			try
			{
				context!.PaymentTerms!.RemoveRange(terms);
				await context.SaveChangesAsync(cancellationToken);
				return Result.Success;
			}
			catch (Exception ex)
			{
				IList<Error> errors = [];

				if (IsForeignKeyViolation(ex))
					errors.Add(new("", terms.Length > 1 ? "One or more payment terms are currently in use." : "Payment term is currently in use."));

				if (errors.Any())
					errors.Add(GeneralError);

				return Result.Failure([.. errors]);
			}
		}

		public async Task<IList<PaymentTerm>> FindAllAsync (CancellationToken cancellationToken = default)
			=> await context!.PaymentTerms!.ToListAsync(cancellationToken);
	}
}
