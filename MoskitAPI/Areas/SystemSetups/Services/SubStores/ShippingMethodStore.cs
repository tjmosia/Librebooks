using Microsoft.EntityFrameworkCore;

using Moskit.Data;
using Moskit.Models.Entity.SystemSpace;

namespace Moskit.Areas.SystemSetups.Services.SubStores
{
    public class ShippingMethodStore (AppDbContext? context)
    {
        private readonly AppDbContext? context = context;

        public async Task<ShippingMethod> CreateAsync (ShippingMethod method)
        {
            var result = await context!.ShippingMethod.AddAsync(method);
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
