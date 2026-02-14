using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Core.Types;
using Librebooks.Extensions.Models;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.InventorySpace;

[Table(nameof(ItemInventory))]
public class ItemInventory () : VersionedEntityBase()
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
	public virtual int ItemId { get; set; }

	[Column(TypeName = ColumnTypes.MONETARY)]
	public virtual decimal Price { get; set; }

	[Column(TypeName = ColumnTypes.NUMBER)]
	public virtual decimal QuantityOnHand { get; set; }

	[Column(TypeName = ColumnTypes.NUMBER)]
	public virtual decimal MinQuantity { get; set; }

	[Column(TypeName = ColumnTypes.NUMBER)]
	public virtual decimal MaxQuantity { get; set; }

	public virtual Item? Item { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
	{
		builder.Entity<ItemInventory>(options =>
		{
			options.ToTable(nameof(ItemInventory))
				.HasKey(p => p.ItemId);

			options.Property(p => p.QuantityOnHand)
				.HasColumnType(ColumnTypes.NUMBER);

			options.Property(p => p.Price)
				.HasColumnType(ColumnTypes.MONETARY);

			options.Property(p => p.MinQuantity)
				.HasColumnType(ColumnTypes.NUMBER);

			options.Property(p => p.MaxQuantity)
				.HasColumnType(ColumnTypes.NUMBER);
		});
	}
}
