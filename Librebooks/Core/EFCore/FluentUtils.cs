using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Librebooks.CoreLib.EFCore
{
    public class FluentUtils<TModel, TKey> where TModel : class where TKey : notnull
    {

        public static void UseSequentialId (PropertyBuilder<TKey> propertyBuilder)
        {
            propertyBuilder.HasDefaultValue("newsequentialid()");
        }
    }

    public class FluentUtils
    {
        public static string NewSequentialId = "newsequentialid()";
    }
}
