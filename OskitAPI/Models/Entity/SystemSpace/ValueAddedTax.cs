using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using OskitAPI.Core.Types;

namespace OskitAPI.Models.Entity.SystemSpace
{
    public class ValueAddedTax
    {
        public virtual string? Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual decimal Rate { get; set; }
        public virtual bool System { get; set; }

        [Timestamp, ConcurrencyCheck]
        public virtual byte[]? RowVersion { get; set; }

        public ValueAddedTax ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<ValueAddedTax>(options =>
            {
                options.ToTable(nameof(ValueAddedTax))
                    .HasKey(x => x.Id)
                    .IsClustered();

                options.Property(p => p.Rate)
                    .HasColumnType(ColumnTypes.Percentage);

                options.HasIndex(p => p.Name)
                    .IsUnique();
            });
    }
}
