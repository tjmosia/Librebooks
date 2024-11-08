using Microsoft.EntityFrameworkCore;

using OskitBlazor.Core.EFCore;
using OskitBlazor.Data;
using OskitBlazor.Models.Entity.SystemSpace;

namespace OskitBlazor.Areas.SystemSetups.Services.SubStores
{
    public class ShippingMethodStore : DbStoreBase
    {
        public ShippingMethodStore (AppDbContext? context, ILogger? logger)
            : base(context, logger) { }

        public async Task<ShippingMethod> CreateAsync (ShippingMethod method)
        {
            var result = await context!.ShippingMethod.AddAsync(method);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ShippingMethod> UpdateAsync (ShippingMethod method)
        {
            var result = context!.ShippingMethod.Update(method);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ShippingMethod?> FindByIdAsync (string id)
            => await context!.ShippingMethod.FindAsync(id);

        public async Task<ShippingMethod?> FindByNameAsync (string name)
            => await context!.ShippingMethod
                .Where(p => string.Equals(p.Name, name, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefaultAsync();

        public async Task DeleteAsync (params ShippingMethod[] methods)
        {
            context!.ShippingMethod.RemoveRange(methods);
            await context.SaveChangesAsync();
        }

        public async Task<IList<ShippingMethod>> FindAllAsync ()
            => await context!.ShippingMethod.ToListAsync();
    }
}
