using Librebooks.Core.EFCore;
using Librebooks.Data;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Admin.Services.SubStores
{
    public class TaxTypeStore : DbStoreBase
    {
        public TaxTypeStore (AppDbContext context, ILogger<TaxTypeStore>? logger)
            : base(context, logger) { }

        public async Task<TaxType> CreateAsync (TaxType vat)
        {
            var result = await context!.TaxType!.AddAsync(vat);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<TaxType> UpdateAsync (TaxType vat)
        {
            var result = context!.TaxType!.Update(vat);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<TaxType?> FindByIdAsync (string id)
            => await context!.TaxType!.FindAsync(id);

        public async Task DeleteAsync (params TaxType[] vats)
        {
            context!.TaxType!.RemoveRange(vats);
            await context.SaveChangesAsync();
        }

        public async Task<IList<TaxType>> FindAllAsync ()
            => await context!.TaxType!.ToListAsync();
    }
}
