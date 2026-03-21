using Librebooks.Core.EFCore;
using Librebooks.Data;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Admin.Services.SubStores
{
	public class TaxStore : DbStoreBase
	{
		public TaxStore (AppDbContext context, ILogger<TaxStore>? logger)
			: base(context, logger) { }

		public async Task<Tax> CreateAsync (Tax vat)
		{
			var result = await context!.TaxType!.AddAsync(vat);
			await context.SaveChangesAsync();
			return result.Entity;
		}

		public async Task<Tax> UpdateAsync (Tax vat)
		{
			var result = context!.TaxType!.Update(vat);
			await context.SaveChangesAsync();
			return result.Entity;
		}

		public async Task<Tax?> FindByIdAsync (int id)
			=> await context!.TaxType!.FindAsync(id);

		public async Task DeleteAsync (params Tax[] taxes)
		{
			context!.TaxType!.RemoveRange(taxes);
			await context.SaveChangesAsync();
		}

		public async Task<IList<Tax>> FindAllAsync ()
			=> await context!.TaxType!.ToListAsync();
	}
}
