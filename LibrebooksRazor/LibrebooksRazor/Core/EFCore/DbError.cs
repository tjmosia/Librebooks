using LibrebooksRazor.CoreLib.Operations;

namespace LibrebooksRazor.Core.EFCore
{
    public class DbError
    {
        public int ErrorNumber { get; set; }
        public TransactionError? Error { get; set; }
    }
}
