using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace Moskit.Models.Entity.SystemSpace
{
    public class ShippingTerm
    {
        public virtual string? Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Description { get; set; }

        [Timestamp, ConcurrencyCheck]
        public virtual byte[]? RowVersion { get; set; }

        public ShippingTerm ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<ShippingTerm>(options =>
            {
                options.ToTable(nameof(ShippingTerm))
                    .HasKey(p => p.Id)
                    .IsClustered();
            });
    }
}
