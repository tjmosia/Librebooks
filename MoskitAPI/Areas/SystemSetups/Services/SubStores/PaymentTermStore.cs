using Microsoft.EntityFrameworkCore;

using Moskit.Data;
using Moskit.Models.Entity.SystemSpace;

namespace Moskit.Areas.SystemSetups.Services.SubStores
{
    public class PaymentTermStore (AppDbContext? context)
    {
        private readonly AppDbContext? context = context;

        public async Task<PaymentTerm> CreateAsync (PaymentTerm term)
        {
            var result = await context!.PaymentTerm.AddAsync(term);
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
