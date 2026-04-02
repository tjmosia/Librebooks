using Librebooks.CoreLib.Operations;

namespace Librebooks.Core.EFCore
{
    public class DbError
    {
        public int ErrorNumber { get; set; }
        public Error? Error { get; set; }
    }
}
