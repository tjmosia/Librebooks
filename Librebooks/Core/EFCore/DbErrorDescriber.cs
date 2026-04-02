using Librebooks.CoreLib.Operations;

namespace Librebooks.Core.EFCore
{
    public class DbErrorDescriber
    {
        public DbError IndexConstraint (string? description = null)
            => new DbError
            {
                ErrorNumber = DbEngineErrorsCodes.IndexConstraint,
                Error = new Error(nameof(IndexConstraint), description ?? "")
            };

        public DbError PrimaryKeyConstraint (string? description = null)
            => new DbError
            {
                ErrorNumber = DbEngineErrorsCodes.PrimaryKeyConstraint,
                Error = new Error(nameof(PrimaryKeyConstraint), description ?? "")
            };
    }
}
