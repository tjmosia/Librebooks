using Librebooks.Core.EFCore;
using Librebooks.Data;
using Librebooks.Models.Entity.InventorySpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Inventory.Services
{
    public sealed class ItemStore : DbStoreBase
    {
        public ItemStore (AppDbContext context, ILogger<ItemStore> logger, DbErrorDescriber errorDescriber)
        : base(context, logger, errorDescriber) { }

        public async Task<Item?> CreateAsync (int companyId, Item item)
        {
            item.CompanyId = companyId;
            var result = await context!.Item!.AddAsync(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Item> UpdateAsync (Item item)
        {
            var result = context!.Item!.Update(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Item?> FindByIdAsync (int companyId, int itemId, CancellationToken cancellationToken = default)
        {
            return await context!.Item!.Where(p => p.Id == itemId)
                .Include(p => p.Inventory)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IList<ItemAdjustment>> FindAdjustmentsByItemIdAsync (int companyId, int itemId, CancellationToken cancellationToken = default)
        {
            return await context!.ItemAdjustment!
                .Where(p => p.ItemId == itemId && p.CompanyId == companyId)
                .Include(p => p.Item)
                .ToListAsync(cancellationToken);
        }

        public async Task<IList<ItemAdjustment>> FindAdjustmentsAsync (int companyId, CancellationToken cancellationToken = default)
        {
            return await context!.ItemAdjustment!
                .Where(p => p.CompanyId == companyId)
                .Include(p => p.Item)
                .ToListAsync(cancellationToken);
        }

        public async Task<ItemAdjustment?> FindAdjustmentByIdAsync (int companyId, int adjustmentId, CancellationToken cancellationToken = default)
        {
            return await context!.ItemAdjustment!
                .Where(p => p.Id == adjustmentId && p.CompanyId == companyId)
                .Include(p => p.Item)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Item?> FindByCodeAsync (int companyId, string itemCode, CancellationToken cancellationToken = default)
        {
            return await context!.Item!
                .Where(p => p.Code == itemCode && p.CompanyId == companyId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IList<Item>> FindAllAsync (int companyId, CancellationToken cancellationToken = default)
        {
            return await context!.Item!
                .Where(p => p.CompanyId == companyId)
                .ToListAsync(cancellationToken);
        }

        public async Task<ItemAdjustment?> CreateAdjustmentAsync (int companyId, ItemAdjustment adjustment)
        {
            adjustment.CompanyId = companyId;
            var result = await context!.ItemAdjustment!.AddAsync(adjustment);
            await context!.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteItemAsync (int companyId, Item item)
        {
            var adjustments = await FindAdjustmentsByItemIdAsync(companyId, item.Id!);
            if (adjustments.Count > 0)
                context!.RemoveRange(adjustments);
            context!.RemoveRange(item.Inventory!, item);
            await context!.SaveChangesAsync();
        }
    }
}
