using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace OskitAPI.Models.Entity.SystemSpace
{
    public class Currency
    {
        public virtual string? Code { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Symbol { get; set; }

        [Timestamp, ConcurrencyCheck]
        public virtual byte[]? RowVersion { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<Currency>(options =>
            {
                options.ToTable(nameof(Currency))
                    .HasKey(x => x.Code)
                    .IsClustered();

                options.HasIndex(p => p.Name)
                    .IsUnique();
            });
    }
}
