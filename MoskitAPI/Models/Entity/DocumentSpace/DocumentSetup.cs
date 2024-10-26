using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using Moskit.Models.Entity.CompanySpace;

namespace Moskit.Models.Entity.DocumentSpace
{
    public class DocumentSetup
    {
        public virtual string? CompanyId { get; set; }
        public virtual string? Type { get; set; }
        public virtual string? Title { get; set; }
        public virtual string? NumberPrefix { get; set; }
        public virtual string? NumberSuffix { get; set; }
        public virtual string? CurrentNumber { get; set; }
        public virtual bool RemoveLeadingZeros { get; set; }
        public virtual string? FootMessage { get; set; }
        public virtual string? NoteMessage { get; set; }
        public virtual string? PrintTemplateId { get; set; }

        [Timestamp, ConcurrencyCheck]
        public virtual byte[]? RowVersion { get; set; }

        public virtual Company? Company { get; set; }
        public virtual DocumentPrintTemplate? PrintTemplate { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<DocumentSetup>(options =>
            {
                options.ToTable(nameof(DocumentSetup))
                    .HasKey(p => p.CompanyId)
                    .IsClustered();
            });
    }
}
