using OskitBlazor.CoreLib.Operations;
using OskitBlazor.Models.Entity.CompanySpace;
using OskitBlazor.Models.Entity.InventorySpace;

namespace OskitBlazor.Areas.Inventory.Services
{
    public class ItemManager
        (ItemStore? store, ILogger<ItemManager> logger) : IItemManager
    {
        private readonly ItemStore? store = store;
        private readonly ILogger<ItemManager>? logger = logger;

        public void ThrowIfDsposed ()
        {
            if (store == null)
                ArgumentException.ThrowIfNullOrEmpty(nameof(store));
        }

        /***************************************************************************************************
         * ADD FUNCTIONS
         ***************************************************************************************************/
        public async Task<TransactionResult<Item>> AddItemAsync (Company company, Item item)
        {
            var result = await store!.CreateAsync(company.Id!, item);

            if (result != null)
                return TransactionResult<Item>.Success(result);

            return TransactionResult<Item>.Failure();
        }

        public async Task<TransactionResult<ItemAdjustment>> AddAdjustmentAsync (Company company, ItemAdjustment adjustment)
        {
            // DO TRY-CATCH
            var result = await store!.CreateAdjustmentAsync(company.Id!, adjustment);

            if (result != null)
                return TransactionResult<ItemAdjustment>.Success(result);

            return TransactionResult<ItemAdjustment>.Failure();
        }

        /***************************************************************************************************
         * UPDATE FUNCTIONS
         ***************************************************************************************************/
        public async Task<TransactionResult<Item>> UpdateItemAsync (Item item)
        {
            // DO TRY-CATCH
            var result = await store!.UpdateAsync(item);

            if (result != null)
                return TransactionResult<Item>.Success(result);
            return TransactionResult<Item>.Failure();
        }

        /***************************************************************************************************
         * DELETE FUNCTIONS
         * 
         * 
         * 
         ***************************************************************************************************/
        public async Task<TransactionResult> DeleteItemAsync (Company company, Item item)
        {
            // DO TRY-CATCH
            await store!.DeleteItemAsync(company.Id!, item);

            return TransactionResult.Success;
        }


        /***************************************************************************************************
         * GET FUNCTIONS
         ***************************************************************************************************/
        public Task<Item?> GetItemByIdAsync (Company company, string itemId)
            => store!.FindByIdAsync(company.Id!, itemId);

        public async Task<ItemAdjustment?> GetAdjustmentByIdAsync (Company company, string adjustmentId)
            => await store!.FindAdjustmentByIdAsync(company.Id!, adjustmentId);

        public async Task<Item?> GetItemByCodeAsync (Company company, string itemCode)
            => await store!.FindByCodeAsync(company.Id!, itemCode);

        public async Task<IList<Item>> GetItemsAsync (Company company)
            => await store!.FindAllAsync(company.Id!);

        public async Task<IList<ItemAdjustment>> GetAdjustmentsAsync (Company company, Item? item = null)
            => (item == null)
                ? await store!.FindAdjustmentsByItemIdAsync(company.Id!, item!.Id!)
                : await store!.FindAdjustmentsAsync(company.Id!);

    }
}
