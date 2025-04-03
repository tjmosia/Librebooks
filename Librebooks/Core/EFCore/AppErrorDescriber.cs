using Librebooks.CoreLib.Operations;

namespace Librebooks.Core.EFCore
{
    public class AppErrorDescriber
    {
        public TransactionError DuplicateKey (string errorMessage = "")
        {
            return new TransactionError
            {
                Code = nameof(DuplicateKey),
                Description = errorMessage
            };
        }

        public TransactionError DuplicateIndex (string errorMessage = "")
        {
            return new TransactionError
            {
                Code = nameof(DuplicateIndex),
                Description = errorMessage
            };
        }
    }
}
