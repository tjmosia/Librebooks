using Librebooks.Core.EFCore;
using Librebooks.CoreLib.Operations;
using Librebooks.Data;
using Librebooks.Models.Entity.CompanySpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Systems.Services.Stores;

public class CompanyNumberStore (AppDbContext context, ILogger<CompanyNumberStore> logger)
	: DbStoreBase(context, logger)
{
	public async Task<CompanySetup?> FindCurrentAsync (CancellationToken cancellationToken = default)
		=> await context!.CompanySetup!.FirstOrDefaultAsync(cancellationToken);

	public async Task<Result<CompanySetup>> CreateAsync (CompanySetup setup, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = await context!.CompanySetup!.AddAsync(setup, cancellationToken);
			await context.SaveChangesAsync(cancellationToken);
			return Result<CompanySetup>.Success(result.Entity);
		}
		catch (Exception)
		{
			return Result<CompanySetup>.Failure(GeneralError);
		}
	}

	public async Task<Result<CompanySetup>> UpdateAsync (CompanySetup setup, CancellationToken cancellationToken = default)
	{
		try
		{
			setup.RefreshConcurrencyToken();
			var result = context!.CompanySetup!.Update(setup);
			await context.SaveChangesAsync(cancellationToken);
			return Result<CompanySetup>.Success(result.Entity);
		}
		catch (Exception)
		{
			return Result<CompanySetup>.Failure(GeneralError);
		}
	}
}
