using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.DocumentSpace;

[Table(nameof(DocumentType))]
public class DocumentType () : VersionedEntityBase()
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public virtual int Id { get; set; }

	[Required, MaxLength(100)]
	public virtual string? Name { get; set; }

	[Required, MaxLength(50)]
	public virtual string? Group { get; set; }

	[Required, MaxLength(100)]
	public virtual string? Type { get; set; }

	public static void OnModelCreating (ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<DocumentType>(options =>
		{

		});
	}
}
