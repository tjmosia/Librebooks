using LibreBooks.CoreLib.Operations;

namespace LibreBooks.Core.EFCore
{
    public class DbError
    {
        public int ErrorNumber { get; set; }
        public TransactionError? Error { get; set; }
    }
}
