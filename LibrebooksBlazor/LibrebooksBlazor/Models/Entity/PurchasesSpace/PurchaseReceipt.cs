using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using LibrebooksRazor.Core.Types;
using LibrebooksRazor.Extensions.Models;
using LibrebooksRazor.Models.Entity.BankingSpace;
using LibrebooksRazor.Models.Entity.CompanySpace;
using LibrebooksRazor.Models.Entity.SupplierSpace;
using LibrebooksRazor.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace LibrebooksRazor.Models.Entity.PurchasesSpace
{
	[Table(nameof(PurchaseReceipt))]
	public class PurchaseReceipt () : VersionedEntityBase()
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public virtual int Id { get; set; }
		public virtual DateOnly Date { get; set; }

		[MaxLength(50), Required]
		public virtual string? Number { get; set; }

		[MaxLength(50)]
		public virtual string? Reference { get; set; }

		[MaxLength(255)]
		public virtual string? Description { get; set; }

		[MaxLength(255)]
		public virtual string? Comments { get; set; }

		[Column(TypeName = ColumnTypes.MONETARY)]
		public virtual decimal Amount { get; set; }

		public virtual bool Reconciled { get; set; }
		public virtual bool Recorded { get; set; }
		public virtual int SupplierId { get; set; }
		public virtual int CompanyId { get; set; }
		public virtual int BankAccountId { get; set; }
		public virtual int PaymentMethodId { get; set; }

		public virtual PaymentMethod? PaymentMethod { get; set; }
		public virtual BankAccount? BankAccount { get; set; }
		public virtual ICollection<PurchaseInvoiceReceipt>? AllocatedInvoices { get; set; }

		public static void OnModelCreating (ModelBuilder builder)
		{
			builder.Entity<PurchaseReceipt>(options =>
			{
				options.HasOne(p => p.PaymentMethod)
					.WithMany()
					.HasForeignKey(p => p.PaymentMethodId)
						.IsRequired()
					.OnDelete(DeleteBehavior.Restrict);

				options.HasMany(p => p.AllocatedInvoices)
					.WithOne(p => p.Receipt)
					.HasForeignKey(p => p.ReceiptId)
						.IsRequired()
					.OnDelete(DeleteBehavior.Restrict);

				options.HasOne<Supplier>()
					.WithMany()
					.HasForeignKey(p => p.SupplierId)
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
