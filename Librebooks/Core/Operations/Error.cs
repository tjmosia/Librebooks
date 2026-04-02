using Librebooks.Core.EFCore;

using Microsoft.AspNetCore.Identity;

namespace Librebooks.CoreLib.Operations
{
	public class Error : IdentityError
	{
		public Error () { }

		public Error (string code = "", string description = "")
		{
			Code = code;
			Description = description;
		}

		public static Error FromIdentityError (IdentityError error)
			=> new(error.Code, error.Description);

		public static Error FromDbError (DbError error)
			=> error.Error!;

		public static IEnumerable<Error> FromIdentityErrors (params IdentityError[] errors)
		{
			foreach (var error in errors)
				yield return new Error(error.Code, error.Description);
		}

		public static Error FromIE (IdentityError error)
			=> FromIdentityError(error);

		public static IEnumerable<Error> FromIEs (params IdentityError[] errors)
			=> FromIdentityErrors(errors);

		public static Error Create (string code, string description = "")
			=> new(code, description);
	}
}
