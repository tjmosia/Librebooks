using Microsoft.EntityFrameworkCore;

using OskitBlazor.Core.EFCore;
using OskitBlazor.Data;
using OskitBlazor.Models.Entity.SystemSpace;

namespace OskitBlazor.Areas.SystemSetups.Services.SubStores
{
    public class ValueAddedTaxStore : DbStoreBase
    {
        public ValueAddedTaxStore (AppDbContext? context, ILogger? logger)
            : base(context, logger) { }

        /// <exception cref="DbUpdateException"/>
        public async Task<ValueAddedTax> CreateAsync (ValueAddedTax vat)
        {
            var result = await context!.ValueAddedTax.AddAsync(vat);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        /// <exception cref="DbUpdateException"/>
        public async Task<ValueAddedTax> UpdateAsync (ValueAddedTax vat)
        {
            var result = context!.ValueAddedTax.Update(vat);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ValueAddedTax?> FindByIdAsync (string id)
            => await context!.ValueAddedTax.FindAsync(id);

        public async Task DeleteAsync (params ValueAddedTax[] vats)
        {
            context!.ValueAddedTax.RemoveRange(vats);
            await context.SaveChangesAsync();
        }

        public async Task<IList<ValueAddedTax>> FindAllAsync ()
            => await context!.ValueAddedTax.ToListAsync();
    }
}
