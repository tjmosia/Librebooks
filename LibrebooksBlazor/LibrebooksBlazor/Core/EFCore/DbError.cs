using LibrebooksBlazor.CoreLib.Operations;

namespace LibrebooksBlazor.Core.EFCore
{
    public class DbError
    {
        public int ErrorNumber { get; set; }
        public TransactionError? Error { get; set; }
    }
}
