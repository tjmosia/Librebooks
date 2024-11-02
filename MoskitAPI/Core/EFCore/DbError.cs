using Moskit.CoreLib.Operations;

namespace Moskit.Core.EFCore
{
    public class DbError
    {
        public int ErrorNumber { get; set; }
        public TransactionError? Error { get; set; }
    }
}
