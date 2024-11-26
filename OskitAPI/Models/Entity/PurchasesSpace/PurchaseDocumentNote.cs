using Microsoft.EntityFrameworkCore;

using MacbooksAPI.Models.Entity.GeneralSpace;

namespace MacbooksAPI.Models.Entity.PurchasesSpace
{
    public class PurchaseDocumentNote
    {
        public virtual string? DocumentId { get; set; }
        public virtual string? NoteId { get; set; }

        public virtual Note? Note { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<PurchaseDocumentNote>(options =>
            {
                options.ToTable(nameof(PurchaseDocumentNote))
                    .HasKey(e => new { e.DocumentId, e.NoteId })
                    .IsClustered();
            });
    }
}
