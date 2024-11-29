using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Models.Entity.SystemSpace
{
    public class DateFormat
    {
        public virtual string? Id { get; set; } = Guid.NewGuid().ToString("N");
        public virtual string? Format { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public void UpdateConcurrencyToken ()
            => RowVersion = Guid.NewGuid().ToString("N");

        public DateFormat ()
            => Id = Guid.NewGuid().ToString("N");

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
