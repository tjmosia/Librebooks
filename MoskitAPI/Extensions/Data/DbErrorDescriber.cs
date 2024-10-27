using Moskit.CoreLib.Operations;

namespace Moskit.Extensions.Data
{
    public struct DbErrorDescriber
    {
        public static TransactionError DuplicateIndex (string value)
            => new TransactionError(nameof(DuplicateIndex), value);

        public static TransactionError DuplicateId (string value)
            => new TransactionError(nameof(DuplicateId), value);
    }
}
