using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Models.Entity.SystemSpace
{
    public class Country
    {
        public virtual string? Code { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? DialingCode { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public void UpdateConcurrencyToken ()
            => RowVersion = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<Country>(options =>
            {
                options.ToTable(nameof(Country))
                    .HasKey(x => x.Code)
                    .IsClustered();

                options.HasIndex(p => p.Name)
                    .IsUnique();

                options.Property(p => p.Code)
                    .HasMaxLength(3);
            });
    }
}
