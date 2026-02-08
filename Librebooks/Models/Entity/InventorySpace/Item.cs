using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.CompanySpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.InventorySpace;

[Table(nameof(Item))]
[Index(nameof(Code), IsUnique = true)]
public class Item () : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [MaxLength(50)]
    public virtual string? Code { get; set; }

    [Required, MaxLength(255)]
    public virtual string? Description { get; set; }

    [MaxLength(20)]
    public virtual string? UnitOfMeasure { get; set; }

    public virtual bool Physical { get; set; }

    public virtual int? CategoryId { get; set; }
    public virtual int TaxTypeId { get; set; }
    public virtual int CompanyId { get; set; }
    public virtual int Active { get; set; }

    public virtual Company? Company { get; set; }
    public virtual ItemCategory? Category { get; set; }
    public virtual ItemInventory? Inventory { get; set; }
    public virtual CompanyTaxType? TaxType { get; set; }
    public virtual ICollection<ItemAdjustment>? StockAdjustments { get; set; }
    public virtual ICollection<ItemPriceAdjustment>? PriceAdjustments { get; set; }
    public virtual ICollection<ItemDetail>? Details { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<Item>(options =>
        {
            options.HasIndex(p => new { p.CompanyId, p.Id })
                .IsClustered()
                .IsUnique();

            options.HasOne(p => p.Inventory)
                .WithOne(p => p.Item)
                .HasForeignKey<ItemInventory>(p => p.ItemId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            options.HasMany(p => p.StockAdjustments)
                .WithOne(p => p.Item)
                .HasForeignKey(p => p.ItemId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            options.HasMany(p => p.PriceAdjustments)
                .WithOne(p => p.Item)
                .HasForeignKey(p => p.ItemId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
