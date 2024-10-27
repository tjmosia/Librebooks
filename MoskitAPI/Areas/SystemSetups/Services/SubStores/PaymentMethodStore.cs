using Microsoft.EntityFrameworkCore;

using Moskit.Data;
using Moskit.Models.Entity.SystemSpace;

namespace Moskit.Areas.SystemSetups.Services.SubStores
{
    public class PaymentMethodStore (AppDbContext? context)
    {
        private readonly AppDbContext? context = context;

        public async Task<PaymentMethod> CreateAsync (PaymentMethod method)
        {
            var result = await context!.PaymentMethod.AddAsync(method);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<PaymentMethod?> FindByIdAsync (string id)
            => await context!.PaymentMethod.FindAsync(id);

        public async Task DeleteAsync (params PaymentMethod[] methods)
        {
            context!.PaymentMethod.RemoveRange(methods);
            await context.SaveChangesAsync();
        }

        public async Task<IList<PaymentMethod>> FindAllAsync ()
            => await context!.PaymentMethod.ToListAsync();
    }
}
