using Microsoft.EntityFrameworkCore;

using OskitBlazor.Core.EFCore;
using OskitBlazor.Data;
using OskitBlazor.Models.Entity.SystemSpace;

namespace OskitBlazor.Areas.SystemSetups.Services.SubStores
{
    public class PaymentMethodStore : DbStoreBase
    {
        public PaymentMethodStore (AppDbContext? context, ILogger? logger)
            : base(context, logger) { }

        /// <exception cref="DbUpdateException"/>
        public async Task<PaymentMethod> CreateAsync (PaymentMethod method)
        {
            var result = await context!.PaymentMethod.AddAsync(method);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        /// <exception cref="DbUpdateException"/>
        public async Task<PaymentMethod> UpdateAsync (PaymentMethod method)
        {
            var result = context!.PaymentMethod.Update(method);
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
