using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.AccountingSpace;
using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Models.Entity.PurchasesSpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SupplierSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.GeneralSpace
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
        public virtual string? CreatorId { get; set; }

        public virtual User? Creator { get; set; }

        public static void BuildModel (ModelBuilder builder)
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
