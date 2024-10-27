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

        public async Task<TransactionResult<Item>> CreateAsync (Item item)
        {
            ArgumentNullException.ThrowIfNull(nameof(item));

            if (string.IsNullOrEmpty(item.Code))
                if (FindByCode(item.Code!) != null)
                    return TransactionResult<Item>.Failure(TransactionError.FromIE(errorDescriber.Duplicate("Item Code already exist.")));

            var result = await context.Item.AddAsync(item);
            await context.SaveChangesAsync();
            return TransactionResult<Item>.Success(result.Entity);
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
