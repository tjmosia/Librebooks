using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Models.Entity.AccountingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SupplierSpace
{
	[Table(nameof(SupplierAdjustment))]
	public class SupplierAdjustment
	{
		[Key]
		public virtual int JournalEntryId { get; set; }
		public virtual int CompanyId { get; set; }
		public virtual int SupplierId { get; set; }

		public virtual JournalEntry? JournalEntry { get; set; }
		public virtual Supplier? Supplier { get; set; }

		public static void OnModelCreating (ModelBuilder builder)
		{
			builder.Entity<SupplierAdjustment>(options =>
			{
				options.HasIndex(p => new { p.CompanyId, p.SupplierId, p.JournalEntryId })
					.IsClustered();

				options.HasOne(p => p.JournalEntry)
					.WithOne()
					.HasForeignKey<SupplierAdjustment>(p => p.JournalEntryId)
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
