using OskitBlazor.CoreLib.Operations;

namespace OskitBlazor.Core.EFCore
{
    public class DbError
    {
        public int ErrorNumber { get; set; }
        public TransactionError? Error { get; set; }
    }
}
