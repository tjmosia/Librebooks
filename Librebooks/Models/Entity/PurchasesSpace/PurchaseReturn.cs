using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Models.Entity.CompanySpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.PurchasesSpace
{
	[Table(nameof(PurchaseReturn))]
	public class PurchaseReturn
	{
		[Key]
		public virtual int DocumentId { get; set; }
		public virtual int CompanyId { get; set; }
		public virtual int SupplierId { get; set; }

		public virtual PurchaseDocument? Document { get; set; }
		public virtual ICollection<PurchaseInvoiceReturn>? Invoices { get; set; }

		public static void OnModelCreating (ModelBuilder builder)
		{
			builder.Entity<PurchaseReturn>(options =>
			{
				options.HasIndex(p => new { p.CompanyId, p.SupplierId, p.DocumentId })
					.IsClustered();

				options.HasOne(p => p.Document)
					.WithOne()
					.HasForeignKey<PurchaseReturn>(p => p.DocumentId)
						.IsRequired()
					.OnDelete(DeleteBehavior.Cascade);

				options.HasMany(p => p.Invoices)
					.WithOne(p => p.Return)
					.HasForeignKey(p => p.ReturnId)
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
}
