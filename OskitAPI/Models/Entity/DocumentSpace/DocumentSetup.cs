using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using MacbooksAPI.Models.Entity.CompanySpace;

namespace MacbooksAPI.Models.Entity.DocumentSpace
{
    public class DocumentSetup
    {
        public virtual string? Id { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? Title { get; set; }
        public virtual string? NumberPrefix { get; set; }
        public virtual string? NumberSuffix { get; set; }
        public virtual string? CurrentNumber { get; set; }
        public virtual bool RemoveLeadingZeros { get; set; }
        public virtual string? FootMessage { get; set; }
        public virtual string? NoteMessage { get; set; }
        public virtual string? TypeName { get; set; }
        public virtual string? PrintTemplateId { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public void UpdateConcurrencyToken ()
            => RowVersion = Guid.NewGuid().ToString("N");

        public virtual Company? Company { get; set; }
        public virtual DocumentPrintTemplate? PrintTemplate { get; set; }

        public DocumentSetup ()
            => Id = Guid.NewGuid().ToString("N");
        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<DocumentSetup>(options =>
            {
                options.ToTable(nameof(DocumentSetup))
                    .HasKey(p => p.Id)
                    .IsClustered();
            });
    }
}
