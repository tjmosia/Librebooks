using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace Moskit.Models.Entity.SystemSpace
{
    public class ShippingMethod
    {
        public virtual string? Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Description { get; set; }

        [Timestamp, ConcurrencyCheck]
        public virtual byte[]? RowVersion { get; set; }

        public ShippingMethod ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<ShippingMethod>(options =>
            {
                options.ToTable(nameof(ShippingMethod))
                    .HasKey(p => p.Id)
                    .IsClustered();

                options.HasIndex(p => p.Name)
                    .IsUnique();
            });
    }
}
