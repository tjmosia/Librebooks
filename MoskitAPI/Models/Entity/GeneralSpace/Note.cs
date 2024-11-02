using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using Moskit.Core.Types;
using Moskit.Models.Entity.AccountingSpace;
using Moskit.Models.Entity.CustomerSpace;
using Moskit.Models.Entity.IdentitySpace;
using Moskit.Models.Entity.PurchasesSpace;
using Moskit.Models.Entity.SalesSpace;
using Moskit.Models.Entity.SupplierSpace;

namespace Moskit.Models.Entity.GeneralSpace
{
    public class Note
    {
        public virtual string? Id { get; set; }
        public virtual string? Description { get; set; }
        public virtual bool Actionable { get; set; }
        public virtual bool Completed { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime? DueDate { get; set; }
        public virtual string? CreatorId { get; set; }

        [ConcurrencyCheck, Timestamp]
        public virtual byte[]? RowVersion { get; set; }
        public virtual User? Creator { get; set; }

        public Note ()
            => Id = Guid.NewGuid().ToString("N");

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
