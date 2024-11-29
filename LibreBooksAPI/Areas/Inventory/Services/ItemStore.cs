using LibreBooks.Core.EFCore;
using LibreBooks.Data;
using LibreBooks.Models.Entity.InventorySpace;

using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Areas.Inventory.Services
{
    public sealed class ItemStore : DbStoreBase
    {
        public ItemStore (AppDbContext? context, ILogger<ItemStore>? logger, DbErrorDescriber? errorDescriber)
        : base(context, logger, errorDescriber) { }

        public async Task<Item?> CreateAsync (string companyId, Item item)
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

        public async Task<Item?> FindByIdAsync (string companyId, string itemId)
        {
            return await context!.Item!.Where(p => p.Id == itemId)
                .Include(p => p.Inventory)
                .FirstOrDefaultAsync();
        }

        public async Task<IList<ItemAdjustment>> FindAdjustmentsByItemIdAsync (string companyId, string itemId)
        {
            return await context!.ItemAdjustment!
                .Where(p => p.ItemId == itemId && p.CompanyId == companyId)
                .Include(p => p.Item)
                .ToListAsync();
        }

        public async Task<IList<ItemAdjustment>> FindAdjustmentsAsync (string companyId)
        {
            return await context!.ItemAdjustment!
                .Where(p => p.CompanyId == companyId)
                .Include(p => p.Item)
                .ToListAsync();
        }

        public async Task<ItemAdjustment?> FindAdjustmentByIdAsync (string companyId, string adjustmentId)
        {
            return await context!.ItemAdjustment!
                .Where(p => p.Id == adjustmentId && p.CompanyId == companyId)
                .Include(p => p.Item)
                .FirstOrDefaultAsync();
        }

        public async Task<Item?> FindByCodeAsync (string companyId, string itemCode)
        {
            return await context!.Item!
                .Where(p => p.Code == itemCode && p.CompanyId == companyId)
                .FirstOrDefaultAsync();
        }

        public async Task<IList<Item>> FindAllAsync (string companyId)
        {
            return await context!.Item!
                .Where(p => p.CompanyId == companyId)
                .ToListAsync();
        }

        public async Task<ItemAdjustment?> CreateAdjustmentAsync (string companyId, ItemAdjustment adjustment)
        {
            adjustment.CompanyId = companyId;
            var result = await context!.ItemAdjustment!.AddAsync(adjustment);
            await context!.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteItemAsync (string companyId, Item item)
        {
            var adjustments = await FindAdjustmentsByItemIdAsync(companyId, item.Id!);
            if (adjustments.Count > 0)
                context!.RemoveRange(adjustments);
            context!.RemoveRange(item.Inventory!, item);
            await context!.SaveChangesAsync();
        }
    }
}
