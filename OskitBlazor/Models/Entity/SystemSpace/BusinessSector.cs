using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

using OskitBlazor.Models.Entity.CompanySpace;

namespace OskitBlazor.Models.Entity.SystemSpace
{
    [Table(nameof(BusinessSector))]
    [Index(nameof(Name), IsUnique = true)]
    public class BusinessSector
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual string? Id { get; set; }
        [Required]
        public virtual string? Name { get; set; }
        [Timestamp, ConcurrencyCheck]
        public virtual byte[]? RowVersion { get; set; }

        public virtual ICollection<Company>? Companies { get; set; }

        public BusinessSector ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<BusinessSector>(options =>
            {
                options.ToTable(nameof(BusinessSector))
                    .HasKey(p => p.Id)
                    .IsClustered();

                options.Property(p => p.RowVersion)
                    .IsRowVersion();

                options.HasMany(p => p.Companies)
                    .WithOne(p => p.BusinessSector)
                    .HasForeignKey(p => p.BusinessSectorId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
