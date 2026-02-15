using Librebooks.Data;
using Librebooks.Models.Entity.GeneralSpace;
using Librebooks.Providers.Stores;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Providers;

public class VerificationStore (AppDbContext context)
	: StoreBase(context)
{
	public async Task<VerificationRequest?> FindAsync (string email, string reason, CancellationToken cancellationToken = default)
	{
		return await context.VerificationRequest!
			.Where(p => p.Email == email && p.Reason == reason)
			.FirstOrDefaultAsync(cancellationToken);
	}

	public async Task<VerificationRequest?> UpdateAsync (VerificationRequest request)
	{
		try
		{
			var result = context.VerificationRequest!.Update(request);
			await context.SaveChangesAsync();

			return result.Entity;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public async Task<bool> DeleteAsync (VerificationRequest request)
	{
		try
		{
			context.VerificationRequest!.Remove(request);
			await context.SaveChangesAsync();
			return true;
		}
		catch
		{
			return false;
		}
	}

	public async Task<VerificationRequest?> CreateAsync (VerificationRequest request)
	{
		var oldRequest = await FindAsync(request.Email!, request.Reason!);

		if (oldRequest != null)
			await DeleteAsync(oldRequest);

		try
		{
			var result = await context.VerificationRequest!.AddAsync(request);
			await context.SaveChangesAsync();
			return result.Entity;
		}
		catch
		{
			return null;
		}
	}
}
