using OskitAPI.CoreLib.Operations;

namespace OskitAPI.Core.EFCore
{
    public class DbError
    {
        public int ErrorNumber { get; set; }
        public TransactionError? Error { get; set; }
    }
}
