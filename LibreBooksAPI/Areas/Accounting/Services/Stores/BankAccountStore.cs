using LibreBooks.CoreLib.Operations;
using LibreBooks.Models.Entity.BankingSpace;

namespace LibreBooksAPI.Areas.Accounting.Services.Stores
{
    public sealed class BankAccountStore
    {
        public TransactionResult<BankAccount> CreateAsync (BankAccount bankAccount)
        {
            return TransactionResult<BankAccount>.Success();
        }
    }
}
