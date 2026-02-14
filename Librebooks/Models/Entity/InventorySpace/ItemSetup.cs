using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.CompanySpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.InventorySpace;

[Table(nameof(ItemSetup))]
public class ItemSetup () : VersionedEntityBase()
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
	public virtual int CompanyId { get; set; }

	[MaxLength(10)]
	public virtual string? Prefix { get; set; }

	[MaxLength(10)]
	public virtual string? Suffix { get; set; }

	public virtual int NextNumber { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
	{
		builder.Entity<ItemSetup>(options =>
		{
			options.HasOne<Company>()
				.WithOne()
				.HasForeignKey<ItemSetup>(p => p.CompanyId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);
		});
	}
}
