using OskitAPI.CoreLib.Operations;

namespace OskitAPI.Extensions.Data
{
    public struct DbErrorDescriber
    {
        public static TransactionError IndexConstraint (string value)
            => new TransactionError(nameof(IndexConstraint), value);

        public static TransactionError PrimaryKeyConstraint (string value)
            => new TransactionError(nameof(PrimaryKeyConstraint), value);
    }
}
