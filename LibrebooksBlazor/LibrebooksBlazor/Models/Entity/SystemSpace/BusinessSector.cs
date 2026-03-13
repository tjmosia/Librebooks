using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using LibrebooksBlazor.Extensions.Models;
using LibrebooksBlazor.Models.Entity.CompanySpace;

using Microsoft.EntityFrameworkCore;

namespace LibrebooksBlazor.Models.Entity.SystemSpace
{
    [Table(nameof(BusinessSector))]
    [Index(nameof(Name), IsUnique = true)]
    public class BusinessSector () : VersionedEntityBase()
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public virtual int Id { get; set; }

        [Required]
        [MaxLength(75)]
        public virtual string? Name { get; set; }

        public virtual ICollection<Company>? Companies { get; set; }

        public BusinessSector (string name) : this()
        {
            Name = name;
        }

        public static void OnModelCreating (ModelBuilder builder)
        {
            builder.Entity<BusinessSector>(options =>
            {
                options.HasMany(p => p.Companies)
                    .WithOne(p => p.BusinessSector)
                    .HasForeignKey(p => p.BusinessSectorId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
