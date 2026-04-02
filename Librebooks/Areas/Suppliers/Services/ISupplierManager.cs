using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.PurchasesSpace;
using Librebooks.Models.Entity.SupplierSpace;

namespace Librebooks.Areas.Suppliers.Services
{
    public interface ISupplierManager
    {
        Task<Result> AllocateReturnToInvoiceAsync (PurchaseInvoice invoice, PurchaseReturn purchaseReturn);
        Task<Result> AllocateReceiptToInvoiceAsync (PurchaseReceipt receipt, PurchaseInvoice invoice);

        Task<IList<Supplier>> GetAllAsync (Company company);
        Task<Result<Supplier>> CreateAsync (Company company, Supplier supplier);

        Task<Result<PurchaseOrder>> AddOrderAsync (Supplier supplier, PurchaseOrder order);
        Task<Result<PurchaseInvoice>> AddInvoiceAsync (Supplier supplier, PurchaseOrder order);
        Task<Result<PurchaseReceipt>> AddReceiptAsync (Supplier supplier, PurchaseOrder order);
        Task<Result<PurchaseReturn>> AddReturnAsync (Supplier supplier, PurchaseReceipt purchaseReturn);

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

        Task<Result<Supplier>> UpdateAsync (Supplier supplier);

        Task<Result> DeleteAsync (Supplier supplier);
    }
}
