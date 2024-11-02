using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Moskit.Core.EFCore;
using Moskit.CoreLib.Operations;
using Moskit.Data;
using Moskit.Extensions.Identity;
using Moskit.Models.Entity.InventorySpace;

namespace Moskit.Areas.Inventory.Services
{
	public sealed class ItemStore : DbStoreBase
	{
		public  ItemStore(AppDbContext context, ILogger<ItemStore> logger, IdentityErrorDescriberExt errorDescriber)
		:base(context, logger, errorDescriber) {}

		public async Task<TransactionResult<Item>> CreateAsync (Item item)
		{
			ArgumentNullException.ThrowIfNull(nameof(item));

			if (string.IsNullOrEmpty(item.Code))
				if (await FindByCodeAsync(item.Code!) != null)
					return TransactionResult<Item>
						.Failure(TransactionError.FromIE(errorDescriber.Duplicate("Item Code already exist.")));

			if(item.Active == null)
				item.Active = true;

			var result = await context.Item.AddAsync(item);
			await context.SaveChangesAsync();
			return TransactionResult<Item>.Success(result.Entity);
		}

		public Item? FindByIdAsync (string id)
		{
			ArgumentNullException.ThrowIfNull(nameof(id));
			return context.Item.Find(id);
		}

		public async Task<ItemInventory?> FindInventoryByIdASync(string itemId)
		{
			return await context!.ItemInventory.Where(p=>p.ItemId == itemId)
				.Include(p=>p.Item)
				.FirstOrDefaultAsync();
		}

		public async Task<IList<ItemAdjustment>?> FindAdjustmentsByItemIdAsync(string itemId)
		{
			return await context!.ItemAdjustment.Where(p=>p.ItemId == itemId)
				.Include(p=>p.Item)
				.ToListAsync();
		}

		public async Task<Item?> FindByCodeAsync (string code)
		{
			ArgumentNullException.ThrowIfNull(nameof(code));

			return await context.Item
				.Where(p => p.Code == code)
				.FirstOrDefaultAsync();
		}

		public async Task<IList<Item>> FindAllAsync(string companyId)
			=> await context.Item.Where(p=>p.CompanyId == companyId)
				.ToListAsync();
	}
}
