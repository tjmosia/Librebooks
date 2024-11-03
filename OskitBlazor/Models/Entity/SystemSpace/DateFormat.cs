using Microsoft.EntityFrameworkCore;

namespace OskitBlazor.Models.Entity.SystemSpace
{
    public class DateFormat
    {
        public virtual string? Id { get; set; } = Guid.NewGuid().ToString("N");
        public virtual string? Format { get; set; }
        public virtual byte[]? RowVersion { get; set; }

        public DateFormat ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<DateFormat>(options =>
            {
                options.ToTable(nameof(DateFormat))
                    .HasKey(x => x.Id);

                options.Property(p => p.RowVersion)
                    .IsRowVersion();
            });
    }
}
