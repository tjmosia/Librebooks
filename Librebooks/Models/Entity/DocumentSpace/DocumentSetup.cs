using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.DocumentSpace
{
    public class DocumentSetup
    {
        public virtual string? Id { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? Type { get; set; }
        public virtual string? Title { get; set; }
        public virtual string? Prefix { get; set; }
        public virtual string? Suffix { get; set; }
        public virtual long NextNumber { get; set; }
        public virtual short LeadingZeros { get; set; }
        public virtual string? FooterMessage { get; set; }
        public virtual string? NoteMessage { get; set; }
        public virtual string? PrintTemplateId { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public virtual DocumentPrintTemplate? PrintTemplate { get; set; }

        public DocumentSetup ()
        {
            Id = Guid.NewGuid().ToString("N").ToUpper();
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<DocumentSetup>(options =>
            {
                options.ToTable(nameof(DocumentSetup))
                    .HasKey(p => p.Id)
                    .IsClustered(false);

                options.HasIndex(p => new { p.CompanyId, p.Id })
                    .IsUnique()
                    .IsClustered();
            });
    }
}
