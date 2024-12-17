using System.ComponentModel.DataAnnotations;

using LibreBooks.Models.Entity.CompanySpace;

using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Models.Entity.SystemSpace
{
    public class BusinessSector
    {
        public virtual string Id { get; set; }
        public virtual string? Name { get; set; }
        [ConcurrencyCheck]
        public virtual string RowVersion { get; set; }

        public virtual ICollection<Company>? Companies { get; set; }

        public BusinessSector ()
        {
            Id = Guid.NewGuid().ToString("N").ToUpper();
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<BusinessSector>(options =>
            {
                options.ToTable(nameof(BusinessSector))
                    .HasKey(p => p.Id)
                    .IsClustered();

                options.Property(p => p.RowVersion)
                    .IsRowVersion();

                options.HasIndex(p => p.Name)
                    .IsUnique();

                options.HasMany(p => p.Companies)
                    .WithOne(p => p.BusinessSector)
                    .HasForeignKey(p => p.BusinessSectorId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
