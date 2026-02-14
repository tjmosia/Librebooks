using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.InventorySpace
{
	[Table(nameof(ItemPriceAdjustment))]
	public class ItemPriceAdjustment () : VersionedEntityBase()
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public virtual int Id { get; set; }
		public virtual DateOnly Date { get; set; }
		public virtual decimal OldPrice { get; set; }
		public virtual decimal NewPrice { get; set; }

		public virtual int ItemId { get; set; }
		public virtual int CompanyId { get; set; }
		public virtual Item? Item { get; set; }

		public static void OnModelCreating (ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ItemPriceAdjustment>(options =>
			{
				options.HasIndex(p => new { p.CompanyId, p.ItemId, p.Id })
					.IsClustered();
			});
		}
	}
}
