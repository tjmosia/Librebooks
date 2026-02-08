using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CustomerSpace
{
    [Table(nameof(CustomerCategory))]
    public class CustomerCategory () : VersionedEntityBase()
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Required, MaxLength(75)]
        public virtual string? Name { get; set; }

        [MaxLength(255)]
        public virtual string? Description { get; set; }

        public virtual int CompanyId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<Customer>? Customers { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<CustomerCategory>(options =>
            {
                options.ToTable(nameof(CustomerCategory))
                    .HasKey(p => p.Id)
                    .IsClustered(false);

                options.HasIndex(p => p.CompanyId)
                    .IsClustered()
                    .IsUnique();

                options.HasMany(p => p.Customers)
                    .WithOne(p => p.Category)
                    .HasForeignKey(p => p.CategoryId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);
            });
    }
}
