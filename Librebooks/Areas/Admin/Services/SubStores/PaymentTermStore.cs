using Librebooks.Core.EFCore;
using Librebooks.Data;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Admin.Services.SubStores
{
    public class PaymentTermStore : DbStoreBase
    {
        public PaymentTermStore (AppDbContext context, ILogger<PaymentTermStore> logger)
            : base(context, logger) { }

        /// <exception cref="DbUpdateException"/>
        public async Task<PaymentTerm> CreateAsync (PaymentTerm term)
        {
            var result = await context!.PaymentTerm!.AddAsync(term);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        /// <exception cref="DbUpdateException"/>
        public async Task<PaymentTerm?> UpdateAsync (PaymentTerm term)
        {
            var result = context!.PaymentTerm!.Update(term);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<PaymentTerm?> FindByIdAsync (string id)
            => await context!.PaymentTerm!.FindAsync(id);

        public async Task DeleteAsync (params PaymentTerm[] terms)
        {
            context!.PaymentTerm!.RemoveRange(terms);
            await context.SaveChangesAsync();
        }

        public async Task<IList<PaymentTerm>> FindAllAsync ()
            => await context!.PaymentTerm!.ToListAsync();
    }
}
