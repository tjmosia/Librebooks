using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using Moskit.Models.Entity.CompanySpace;

namespace Moskit.Models.Entity.InventorySpace
{
    public class Item
    {
        public virtual string? Id { get; set; }
        public virtual string? Code { get; set; }
        public virtual string? Description { get; set; }
        public virtual string? Unit { get; set; }
        public virtual bool Physical { get; set; }
        public virtual string? CategoryId { get; set; }
        public virtual string? VATId { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual bool? Active { get; set; }

        [Timestamp, ConcurrencyCheck]
        public virtual byte[]? RowVersion { get; set; }

        public virtual Company? Company { get; set; }
        public virtual ItemCategory? Category { get; set; }
        public virtual ItemInventory? Inventory { get; set; }
        public virtual CompanyValueAddedTax? VAT { get; set; }
        public virtual ICollection<ItemAdjustment>? Adjustments { get; set; }

        public Item ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<Item>(options =>
            {
                options.ToTable(nameof(Item))
                    .HasKey(p => p.Id)
                    .IsClustered(false);

                options.HasIndex(p => new { p.CompanyId, p.Code })
                    .IsClustered()
                    .IsUnique();

                options.Property(p => p.Code)
                    .IsRequired();

                options.Property(p => p.CompanyId)
                    .IsRequired();

                options.HasOne(p => p.Inventory)
                    .WithOne(p => p.Item)
                    .HasForeignKey<ItemInventory>(p => p.ItemId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasMany(p => p.Adjustments)
                    .WithOne(p => p.Item)
                    .HasForeignKey(p => p.ItemId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
