using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Core.Types;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.CompanySpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.InventorySpace;

[Table(nameof(ItemAdjustment))]
public class ItemAdjustment () : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }
    public virtual DateOnly Date { get; set; }

    [Required, MaxLength(50)]
    public virtual string? Reason { get; set; }

    public virtual int ItemId { get; set; }

    [Column(TypeName = ColumnTypes.Number)]
    public virtual decimal OldQuantityOnHand { get; set; }

    [Column(TypeName = ColumnTypes.Number)]
    public virtual decimal QuantityOnHand { get; set; }

    [Column(TypeName = ColumnTypes.Monetary)]
    public virtual decimal OldPrice { get; set; }

    [Column(TypeName = ColumnTypes.Monetary)]
    public virtual decimal Price { get; set; }

    public virtual int CompanyId { get; set; }

    public virtual bool FromSales { get; set; }

    public virtual Item? Item { get; set; }
    public virtual Company? Company { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<ItemAdjustment>(options =>
        {
            options.HasIndex(p => new { p.CompanyId, p.ItemId })
                .IsClustered();

        });
    }
}
