using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Moskit.CoreLib.Operations;
using Moskit.Data;
using Moskit.Extensions.Identity;
using Moskit.Models.Entity.InventorySpace;

namespace Moskit.Areas.Inventory.Services
{
    public sealed class ItemStore (AppDbContext context, ILogger<ItemStore> logger, IdentityErrorDescriberExt errorDescriber)
    {
        private readonly AppDbContext context = context;
        private readonly ILogger<ItemStore> logger = logger;
        private readonly IdentityErrorDescriberExt errorDescriber = errorDescriber;

        public async Task<Item> CreateAsync (Item item)
        {
            ArgumentNullException.ThrowIfNull(nameof(item));

            if (string.IsNullOrEmpty(item.Code))
                if (FindByCode(item.Code!) != null)
                    return TransactionResult<Item>.Failure(TransactionError.FromIE(errorDescriber.Duplicate("Item Code already exist.")));

            try
            {
                var result = await context.Item.AddAsync(item);
                await context.SaveChangesAsync();
                return result.Entity;
            }
            catch (OperationCanceledException ex)
            {
                logger.LogError("Item CreateAsync Operation Cancelled.");

            }
            catch (DbUpdateException ex)
            {

            }
        }

        public Item? FindById (string id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));
            return context.Item.Find(id);
        }

        public Item? FindByCode (string code)
        {
            ArgumentNullException.ThrowIfNull(nameof(code));
            return context.Item.Where(p => p.Code == code).FirstOrDefault();
        }
    }
}
