﻿using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.InventorySpace;

namespace Librebooks.Areas.Inventory.Services
{
    public interface IItemManager
    {
        /***************************************************************************************************
         * ADD FUNCTIONS
         ***************************************************************************************************/
        Task<TransactionResult<ItemAdjustment>> AddAdjustmentAsync (Company company, ItemAdjustment item);
        Task<TransactionResult<Item>> AddItemAsync (Company company, Item item);

        /***************************************************************************************************
         * GET FUNCTIONS
         ***************************************************************************************************/
        Task<Item?> GetItemByIdAsync (Company company, string id);
        Task<Item?> GetItemByCodeAsync (Company company, string itemCode);
        Task<IList<Item>> GetItemsAsync (Company company);
        Task<IList<ItemAdjustment>> GetAdjustmentsAsync (Company company, Item? item = null);
        Task<ItemAdjustment?> GetAdjustmentByIdAsync (Company company, string adjustmentId);

        /***************************************************************************************************
         * DELETE FUNCTIONS
         ***************************************************************************************************/
        Task<TransactionResult> DeleteItemAsync (Company company, Item item);

        /***************************************************************************************************
         * UPDATE FUNCTIONS
         ***************************************************************************************************/
        Task<TransactionResult<Item>> UpdateItemAsync (Item item);
    }
}
