using System.ComponentModel.DataAnnotations;

using Librebooks.Core.Types;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SystemSpace
{
    public class TaxType
    {
        public virtual string Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual decimal Rate { get; set; }
        public virtual bool System { get; set; }
        public virtual string? Group { get; set; }

        [ConcurrencyCheck]
        public virtual string RowVersion { get; set; }

        public TaxType ()
        {
            Id = Guid.NewGuid().ToString("N").ToUpper();
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<TaxType>(options =>
            {
                options.ToTable(nameof(TaxType))
                    .HasKey(x => x.Id)
                    .IsClustered();

                options.Property(p => p.Rate)
                    .HasColumnType(ColumnTypes.Percentage);

                options.HasIndex(p => new { p.Name, p.System, p.Group })
                    .IsUnique();
            });
    }
}
