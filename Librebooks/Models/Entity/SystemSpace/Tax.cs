using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Core.Constants;
using Librebooks.Extensions.Models;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SystemSpace;

[Table(nameof(Tax))]
public class Tax () : VersionedEntityBase()
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public virtual int Id { get; set; }

	[Required, MaxLength(75)]
	public virtual string? Name { get; set; }

	[Column(TypeName = ColumnTypes.PERCENTATE)]
	public virtual decimal Rate { get; set; }

	public virtual bool System { get; set; }

	[Required, MaxLength(100)]
	public virtual string? Type { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
	{
		builder.Entity<Tax>(options =>
		{

		});
	}
}
