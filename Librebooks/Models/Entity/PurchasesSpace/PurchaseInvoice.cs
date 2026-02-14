using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Models.Entity.CompanySpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.PurchasesSpace
{
	[Table(nameof(PurchaseInvoice))]
	public class PurchaseInvoice
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public virtual int DocumentId { get; set; }
		public virtual int CompanyId { get; set; }
		public virtual int SupplierId { get; set; }

		public virtual PurchaseDocument? Document { get; set; }
		public virtual PurchaseOrderInvoice? Order { get; set; }
		public virtual ICollection<PurchaseInvoiceReturn>? Returns { get; set; }
		public virtual ICollection<PurchaseInvoiceReceipt>? Receipts { get; set; }

		public static void OnModelCreating (ModelBuilder builder)
		{
			builder.Entity<PurchaseInvoice>(options =>
			{
				options.HasIndex(p => new { p.CompanyId, p.SupplierId, p.DocumentId })
					.IsClustered()
					.IsUnique();

				options.HasOne(p => p.Document)
					.WithOne()
					.HasForeignKey<PurchaseInvoice>(p => p.DocumentId)
						.IsRequired()
					.OnDelete(DeleteBehavior.Cascade);

				options.HasOne(p => p.Order)
					.WithOne(p => p.Invoice)
					.HasForeignKey<PurchaseOrderInvoice>(p => p.InvoiceId)
						.IsRequired()
					.OnDelete(DeleteBehavior.Restrict);

				options.HasMany(p => p.Returns)
					.WithOne(p => p.Invoice)
					.HasForeignKey(p => p.InvoiceId)
						.IsRequired()
					.OnDelete(DeleteBehavior.Restrict);

				options.HasMany(p => p.Receipts)
					.WithOne(p => p.Invoice)
					.HasForeignKey(p => p.InvoiceId)
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