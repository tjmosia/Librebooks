using Microsoft.AspNetCore.Identity;

namespace OskitAPI.CoreLib.Operations
{
    public class TransactionError : IdentityError
    {
        public TransactionError () { }

        public TransactionError (string code, string description = "")
        {
            Code = code;
            Description = description;
        }

        public static TransactionError FromIdentityError (IdentityError error)
            => new TransactionError(error.Code, error.Description);

        public static IEnumerable<TransactionError> FromIdentityErrors (params IdentityError[] errors)
        {
            foreach (var error in errors)
                yield return new TransactionError(error.Code, error.Description);
        }

        public static TransactionError FromIE (IdentityError error)
            => FromIdentityError(error);

        public static IEnumerable<TransactionError> FromIEs (params IdentityError[] errors)
            => FromIdentityErrors(errors);
    }
}
