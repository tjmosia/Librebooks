using OskitAPI.CoreLib.Operations;

namespace OskitAPI.Core.EFCore
{
    public class DBErrorDescriber
    {
        public static DbError IndexConstraint = new DbError
        {
            ErrorNumber = DbEngineErrorsCodes.IndexConstraint,
            Error = new TransactionError(nameof(IndexConstraint))
        };

        public static DbError PrimaryKeyConstraint = new DbError
        {
            ErrorNumber = DbEngineErrorsCodes.PrimaryKeyConstraint,
            Error = new TransactionError(nameof(PrimaryKeyConstraint), "Record with with the same key exists.")
        };
    }
}
