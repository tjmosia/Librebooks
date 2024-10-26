using Microsoft.EntityFrameworkCore;

using Moskit.Models.Entity.GeneralSpace;

namespace Moskit.Models.Entity.CustomerSpace
{
    public class CustomerNote
    {
        public virtual string? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual string? NoteId { get; set; }
        public virtual Note? Note { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<CustomerNote>(options =>
            {
                options.ToTable(nameof(CustomerNote))
                    .HasKey(p => new { p.CustomerId, p.NoteId })
                    .IsClustered();

                options.HasOne(p => p.Note)
                    .WithOne()
                    .HasForeignKey<CustomerNote>(p => p.NoteId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });
    }
}
