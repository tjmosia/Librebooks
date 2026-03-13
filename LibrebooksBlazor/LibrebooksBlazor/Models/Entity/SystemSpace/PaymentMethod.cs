using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibrebooksBlazor.Extensions.Models;
using LibrebooksBlazor.Models.Entity.BankingSpace;
using LibrebooksBlazor.Models.Entity.PurchasesSpace;
using LibrebooksBlazor.Models.Entity.SalesSpace;

using Microsoft.EntityFrameworkCore;

namespace LibrebooksBlazor.Models.Entity.SystemSpace
{
	[Table(nameof(PaymentMethod))]
	[Index(nameof(Name), IsUnique = true)]
	public class PaymentMethod () : VersionedEntityBase()
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public virtual int Id { get; set; }

		[Required, MaxLength(50)]
		public virtual string? Name { get; set; }

		[MaxLength(255)]
		public virtual string? Description { get; set; }

		public static void OnModelCreating (ModelBuilder builder)
			=> builder.Entity<PaymentMethod>(options =>
			{
				options.HasMany<BankAccount>()
					.WithOne(p => p.PaymentMethod)
					.HasForeignKey(p => p.PaymentMethodId)
						.IsRequired()
					.OnDelete(DeleteBehavior.Restrict);

				options.HasMany<SalesReceipt>()
					.WithOne(p => p.PaymentMethod)
					.HasForeignKey(p => p.PaymentMethodId)
						.IsRequired()
					.OnDelete(DeleteBehavior.Restrict);

				options.HasMany<PurchaseReceipt>()
					.WithOne(p => p.PaymentMethod)
					.HasForeignKey(p => p.PaymentMethodId)
						.IsRequired()
					.OnDelete(DeleteBehavior.Restrict);
			});
	}
}
