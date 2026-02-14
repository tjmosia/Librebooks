using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Models.Entity.CompanySpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.PurchasesSpace;

[Table(nameof(PurchaseOrder))]
public class PurchaseOrder
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
	public virtual int DocumentId { get; set; }
	public virtual int CompanyId { get; set; }
	public virtual int SupplierId { get; set; }

	public virtual PurchaseDocument? Document { get; set; }
	public virtual ICollection<PurchaseOrderInvoice>? Invoices { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
	{
		builder.Entity<PurchaseOrder>(options =>
		{
			options.HasIndex(p => new { p.SupplierId, p.CompanyId, p.DocumentId })
				.IsClustered()
				.IsUnique();

			options.HasOne(p => p.Document)
				.WithOne()
				.HasForeignKey<PurchaseOrder>(p => p.DocumentId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			options.HasMany(p => p.Invoices)
				.WithOne(p => p.Order)
				.HasForeignKey(p => p.OrderId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne<Company>()
				.WithMany()
				.HasForeignKey(p => p.CompanyId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);
		});
	}
}
