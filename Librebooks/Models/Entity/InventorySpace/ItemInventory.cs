using System.ComponentModel.DataAnnotations;

using Librebooks.Core.Types;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.InventorySpace
{
    public class ItemInventory
    {
        public virtual string? ItemId { get; set; }
        public virtual decimal Price { get; set; }
        public virtual decimal QuantityOnHand { get; set; }
        public virtual decimal MinQuantityAllowed { get; set; }
        public virtual decimal MaxQuantityAllowed { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public virtual Item? Item { get; set; }

        public ItemInventory ()
            => RowVersion = Guid.NewGuid().ToString("N").ToUpper();

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<ItemInventory>(options =>
            {
                options.ToTable(nameof(ItemInventory))
                    .HasKey(p => p.ItemId);

                options.Property(p => p.QuantityOnHand)
                    .HasColumnType(ColumnTypes.Number);

                options.Property(p => p.Price)
                    .HasColumnType(ColumnTypes.Monetary);

                options.Property(p => p.MinQuantityAllowed)
                    .HasColumnType(ColumnTypes.Number);

                options.Property(p => p.MaxQuantityAllowed)
                    .HasColumnType(ColumnTypes.Number);
            });
        }
    }
}
