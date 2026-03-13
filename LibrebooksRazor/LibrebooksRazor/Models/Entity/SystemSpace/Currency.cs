using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibrebooksRazor.Extensions.Models;
using LibrebooksRazor.Models.Entity.SalesSpace;
using Microsoft.EntityFrameworkCore;

namespace LibrebooksRazor.Models.Entity.SystemSpace;

[Table(nameof(Currency))]
[Index(nameof(Name), IsUnique = true)]
public class Currency () : VersionedEntityBase()
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public virtual int Id { get; set; }

	[Required, MaxLength(75)]
	public virtual string? Name { get; set; }

	[Required, MaxLength(3)]
	public virtual string? Code { get; set; }

	[MaxLength(50)]
	public virtual string? Symbol { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
	{
		builder.Entity<Currency>(options =>
		{
			options.HasMany<SalesDocument>()
				.WithOne(p => p.Currency)
				.HasForeignKey(p => p.CurrencyId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);
		});
	}
}
