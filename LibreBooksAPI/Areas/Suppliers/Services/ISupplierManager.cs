using LibreBooks.CoreLib.Operations;
using LibreBooks.Models.Entity.CompanySpace;
using LibreBooks.Models.Entity.PurchasesSpace;
using LibreBooks.Models.Entity.SupplierSpace;

namespace LibreBooksAPI.Areas.Suppliers.Services
{
    public interface ISupplierManager
    {
        Task<TransactionResult> AllocateReturnToInvoiceAsync (PurchaseInvoice invoice, PurchaseReturn purchaseReturn);
        Task<TransactionResult> AllocateReceiptToInvoiceAsync (PurchaseReceipt receipt, PurchaseInvoice invoice);

        Task<IList<Supplier>> GetAllAsync (Company company);
        Task<TransactionResult<Supplier>> CreateAsync (Company company, Supplier supplier);

        Task<TransactionResult<PurchaseOrder>> AddOrderAsync (Supplier supplier, PurchaseOrder order);
        Task<TransactionResult<PurchaseInvoice>> AddInvoiceAsync (Supplier supplier, PurchaseOrder order);
        Task<TransactionResult<PurchaseReceipt>> AddReceiptAsync (Supplier supplier, PurchaseOrder order);
        Task<TransactionResult<PurchaseReturn>> AddReturnAsync (Supplier supplier, PurchaseReceipt purchaseReturn);

        Task<Supplier> FindByIdAsync (Company company, string supplierId);
        Task<Supplier> FindByVendorNumberAsync (Company company, string vendorNumber);
        Task<PurchaseInvoice> FindInvoiceByNumberAsync (Supplier supplier, string invoiceNumber);
        Task<PurchaseInvoice> FindOrderByNumberAsync (Supplier supplier, string orderNumber);
        Task<PurchaseInvoice> FindReturnByNumberAsync (Supplier supplier, string orderNumber);
        Task<PurchaseReceipt> FindReceiptByNumberAsync (Supplier supplier, string receiptNumber);

        Task<IList<PurchaseOrder>> GetOrdersAsync (Supplier supplier);
        Task<IList<PurchaseInvoice>> GetInvoicesAsync (Supplier supplier);
        Task<IList<PurchaseReceipt>> GetReceiptsAsync (Supplier supplier);
        Task<IList<PurchaseReturn>> GetReturnAsync (Supplier supplier);

        Task<TransactionResult<Supplier>> UpdateAsync (Supplier supplier);

        Task<TransactionResult> DeleteAsync (Supplier supplier);
    }
}
