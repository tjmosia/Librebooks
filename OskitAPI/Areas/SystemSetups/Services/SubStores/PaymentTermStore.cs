using Microsoft.EntityFrameworkCore;

using OskitAPI.Core.EFCore;
using OskitAPI.Data;
using OskitAPI.Models.Entity.SystemSpace;

namespace OskitAPI.Areas.SystemSetups.Services.SubStores
{
    public class PaymentTermStore : DbStoreBase
    {
        public PaymentTermStore (AppDbContext? context, ILogger? logger)
            : base(context, logger) { }

        /// <exception cref="DbUpdateException"/>
        public async Task<PaymentTerm> CreateAsync (PaymentTerm term)
        {
            var result = await context!.PaymentTerm.AddAsync(term);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        /// <exception cref="DbUpdateException"/>
        public async Task<PaymentTerm?> UpdateAsync (PaymentTerm term)
        {
            var result = context!.PaymentTerm.Update(term);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<PaymentTerm?> FindByIdAsync (string id)
            => await context!.PaymentTerm.FindAsync(id);

        public async Task DeleteAsync (params PaymentTerm[] terms)
        {
            context!.PaymentTerm.RemoveRange(terms);
            await context.SaveChangesAsync();
        }

        public async Task<IList<PaymentTerm>> FindAllAsync ()
            => await context!.PaymentTerm.ToListAsync();
    }
}
