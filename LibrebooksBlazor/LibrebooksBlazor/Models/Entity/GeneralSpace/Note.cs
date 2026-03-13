using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibrebooksRazor.Extensions.Models;
using LibrebooksRazor.Models.Entity.AccountingSpace;
using LibrebooksRazor.Models.Entity.CustomerSpace;
using LibrebooksRazor.Models.Entity.IdentitySpace;
using LibrebooksRazor.Models.Entity.PurchasesSpace;
using LibrebooksRazor.Models.Entity.SalesSpace;
using LibrebooksRazor.Models.Entity.SupplierSpace;

using Microsoft.EntityFrameworkCore;

namespace LibrebooksRazor.Models.Entity.GeneralSpace
{
	[Table(nameof(Note))]
	public class Note () : VersionedEntityBase()
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public virtual int Id { get; set; }

		[Required]
		[MaxLength(255)]
		public virtual string? Description { get; set; }

		public virtual bool Actionable { get; set; }
		public virtual bool Completed { get; set; }
		public virtual DateOnly DateCreated { get; set; }
		public virtual DateOnly? DueDate { get; set; }
		public virtual int CreatorId { get; set; }

		public virtual User? Creator { get; set; }

		public static void OnModelCreating (ModelBuilder builder)
			=> builder.Entity<Note>(options =>
			{
				options.HasOne(p => p.Creator)
					.WithOne()
					.HasForeignKey<Note>(options => options.CreatorId)
						.IsRequired(false)
					.OnDelete(DeleteBehavior.SetNull);

				options.HasOne<CustomerNote>()
					.WithOne(p => p.Note)
					.HasForeignKey<CustomerNote>(options => options.NoteId)
						.IsRequired()
					.OnDelete(DeleteBehavior.Cascade);

				options.HasOne<SupplierNote>()
					.WithOne(p => p.Note)
					.HasForeignKey<SupplierNote>(options => options.NoteId)
						.IsRequired()
					.OnDelete(DeleteBehavior.Cascade);

				options.HasOne<SalesDocumentNote>()
					.WithOne(p => p.Note)
					.HasForeignKey<SalesDocumentNote>(p => p.NoteId)
						.IsRequired()
					.OnDelete(DeleteBehavior.Cascade);

				options.HasOne<PurchaseDocumentNote>()
					.WithOne(p => p.Note)
					.HasForeignKey<PurchaseDocumentNote>(p => p.NoteId)
						.IsRequired()
					.OnDelete(DeleteBehavior.Cascade);

				options.HasOne<JournalNote>()
					.WithOne(p => p.Note)
					.HasForeignKey<JournalNote>(p => p.NoteId)
						.IsRequired()
					.OnDelete(DeleteBehavior.Cascade);
			});
	}
}
