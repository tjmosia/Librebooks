using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SystemSpace
{
    public class DateFormat
    {
        public virtual string Id { get; set; }
        public virtual string? Format { get; set; }
        [ConcurrencyCheck]
        public virtual string RowVersion { get; set; }

        public DateFormat ()
        {
            Id = Guid.NewGuid().ToString("N").ToUpper();
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<DateFormat>(options =>
            {
                options.ToTable(nameof(DateFormat))
                    .HasKey(x => x.Id);

                options.HasIndex(p => p.Format)
                    .IsUnique();

            });
    }
}
