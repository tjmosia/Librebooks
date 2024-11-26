﻿using Microsoft.EntityFrameworkCore;

using MacbooksAPI.Core.EFCore;
using MacbooksAPI.Data;
using MacbooksAPI.Models.Entity.SystemSpace;

namespace MacbooksAPI.Areas.SystemSetups.Services.SubStores
{
    public class PaymentMethodStore : DbStoreBase
    {
        public PaymentMethodStore (AppDbContext? context, ILogger<PaymentMethodStore>? logger)
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
