using System.ComponentModel.DataAnnotations;

using LibreBooks.Core.Types;

using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Models.Entity.SystemSpace
{
    public class TaxTypes
    {
        public virtual string? Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual decimal Rate { get; set; }
        public virtual bool System { get; set; }
        public virtual string? Group { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public void UpdateConcurrencyToken ()
            => RowVersion = Guid.NewGuid().ToString("N");

        public TaxTypes ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<TaxTypes>(options =>
            {
                options.ToTable(nameof(TaxTypes))
                    .HasKey(x => x.Id)
                    .IsClustered();

                options.Property(p => p.Rate)
                    .HasColumnType(ColumnTypes.Percentage);

                options.HasIndex(p => new { p.Name, p.System, p.Group })
                    .IsUnique();
            });
    }
}
