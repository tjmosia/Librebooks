using System.ComponentModel.DataAnnotations;

using Librebooks.Core.Types;
using Librebooks.Models.Entity.AccountingSpace;
using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Models.Entity.PurchasesSpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SupplierSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.GeneralSpace
{
    public class Note
    {
        public virtual string Id { get; set; }
        public virtual string? Description { get; set; }
        public virtual bool Actionable { get; set; }
        public virtual bool Completed { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime? DueDate { get; set; }
        public virtual string? CreatorId { get; set; }

        [ConcurrencyCheck]
        public virtual string RowVersion { get; set; }

        public virtual User? Creator { get; set; }

        public Note ()
        {
            Id = Guid.NewGuid().ToString("N").ToUpper();
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<Note>(options =>
            {
                options.ToTable(nameof(Note))
                    .HasKey(p => p.Id)
                    .IsClustered();

                options.Property(p => p.DateCreated)
                    .HasColumnType(ColumnTypes.Date);

                options.Property(p => p.DueDate)
                    .HasColumnType(ColumnTypes.Date);

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
