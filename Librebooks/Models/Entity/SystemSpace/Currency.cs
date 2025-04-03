using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SystemSpace
{
    public class Currency
    {
        public virtual string? Code { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Symbol { get; set; }

        [ConcurrencyCheck]
        public virtual string RowVersion { get; set; }

        public Currency ()
            => RowVersion = Guid.NewGuid().ToString("N").ToUpper();

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<Currency>(options =>
            {
                options.ToTable(nameof(Currency))
                    .HasKey(x => x.Code)
                    .IsClustered();
            });
    }
}
