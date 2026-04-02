using Librebooks.CoreLib.Operations;

namespace Librebooks.Core.EFCore
{
    public class AppErrorDescriber
    {
        public Error DuplicateKey (string errorMessage = "")
        {
            return new Error
            {
                Code = nameof(DuplicateKey),
                Description = errorMessage
            };
        }

        public Error DuplicateIndex (string errorMessage = "")
        {
            return new Error
            {
                Code = nameof(DuplicateIndex),
                Description = errorMessage
            };
        }
    }
}
