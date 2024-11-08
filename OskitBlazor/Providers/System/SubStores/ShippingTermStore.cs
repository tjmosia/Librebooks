using Microsoft.EntityFrameworkCore;

using OskitBlazor.Core.EFCore;
using OskitBlazor.Data;
using OskitBlazor.Models.Entity.SystemSpace;

namespace OskitBlazor.Areas.SystemSetups.Services.SubStores
{
    public class ShippingTermStore : DbStoreBase
    {
        public ShippingTermStore (AppDbContext? context, ILogger? logger)
            : base(context, logger) { }

        public async Task<ShippingTerm> CreateAsync (ShippingTerm term)
        {
            var result = await context!.ShippingTerm.AddAsync(term);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ShippingTerm> UpdateAsync (ShippingTerm term)
        {
            var result = context!.ShippingTerm.Update(term);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ShippingTerm?> FindByIdAsync (string id)
            => await context!.ShippingTerm.FindAsync(id);

        public async Task<ShippingTerm?> FindByNameAsync (string name)
            => await context!.ShippingTerm
                .Where(p => string.Equals(p.Name, name, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefaultAsync();

        public async Task DeleteAsync (params ShippingTerm[] terms)
        {
            context!.ShippingTerm.RemoveRange(terms);
            await context.SaveChangesAsync();
        }

        public async Task<IList<ShippingTerm>> FindAllAsync ()
            => await context!.ShippingTerm.ToListAsync();
    }
}
