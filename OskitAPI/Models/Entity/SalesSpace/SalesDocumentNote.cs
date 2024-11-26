using Microsoft.EntityFrameworkCore;

using MacbooksAPI.Models.Entity.GeneralSpace;

namespace MacbooksAPI.Models.Entity.SalesSpace
{
    public class SalesDocumentNote
    {
        public virtual string? DocumentId { get; set; }
        public virtual string? NoteId { get; set; }

        public virtual Note? Note { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<SalesDocumentNote>(options =>
            {
                options.ToTable(nameof(SalesDocumentNote))
                    .HasKey(p => new { p.DocumentId, p.NoteId });
            });
    }
}
