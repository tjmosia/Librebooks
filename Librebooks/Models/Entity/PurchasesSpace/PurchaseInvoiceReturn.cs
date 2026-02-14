using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.PurchasesSpace
{
	[Table(nameof(PurchaseInvoiceReturn))]
	public class PurchaseInvoiceReturn
	{
		public virtual int ReturnId { get; set; }
		public virtual int InvoiceId { get; set; }

		[MaxLength(255)]
		public virtual string? Comment { get; set; }

		public virtual PurchaseReturn? Return { get; set; }
		public virtual PurchaseInvoice? Invoice { get; set; }

		public static void OnModelCreating (ModelBuilder builder)
		{
			builder.Entity<PurchaseInvoiceReturn>(options =>
			{
				options.HasKey(x => new { x.InvoiceId, x.ReturnId })
					.IsClustered();
			});
		}
	}
}
