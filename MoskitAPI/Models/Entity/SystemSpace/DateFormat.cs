using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace OskitAPI.Models.Entity.SystemSpace
{
    public class DateFormat
    {
        public virtual string? Id { get; set; } = Guid.NewGuid().ToString("N");
        public virtual string? Format { get; set; }

        [Timestamp, ConcurrencyCheck]
        public virtual byte[]? RowVersion { get; set; }

        public DateFormat ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<DateFormat>(options =>
            {
                options.ToTable(nameof(DateFormat))
                    .HasKey(x => x.Id);
            });
    }
}
