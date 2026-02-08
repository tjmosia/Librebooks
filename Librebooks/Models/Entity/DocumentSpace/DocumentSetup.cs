using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.DocumentSpace
{
    [Table(nameof(DocumentSetup))]
    public class DocumentSetup () : VersionedEntityBase()
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [MaxLength(75)]
        public virtual string? Type { get; set; }

        [MaxLength(50)]
        public virtual string? Title { get; set; }

        [MaxLength(20)]
        public virtual string? Prefix { get; set; }

        [MaxLength(20)]
        public virtual string? Suffix { get; set; }

        public virtual int NextNumber { get; set; }

        public virtual short LeadingZeros { get; set; }

        [MaxLength(500)]
        public virtual string? FooterMessage { get; set; }

        [MaxLength(500)]
        public virtual string? NoteMessage { get; set; }

        public virtual int CompanyId { get; set; }

        public virtual int PrintTemplateId { get; set; }

        public virtual DocumentPrintTemplate? PrintTemplate { get; set; }

        public static void OnModelCreating (ModelBuilder builder)
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
