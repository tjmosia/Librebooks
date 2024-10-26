using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace Moskit.Models.Entity.CustomerSpace
{
    public class CustomerCategory
    {
        public virtual string? Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Description { get; set; }
        public virtual string? CompanyId { get; set; }

        [Timestamp, ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<Customer>? Customers { get; set; }

        public CustomerCategory ()
            => Id = Guid.NewGuid().ToString("N");

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
