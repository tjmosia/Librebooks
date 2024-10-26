using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using Moskit.Core.Types;
using Moskit.Models.Entity.IdentitySpace;

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

        [ConcurrencyCheck]
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
                        .IsRequired()
                    .OnDelete(DeleteBehavior.SetNull);
            });
    }
}
