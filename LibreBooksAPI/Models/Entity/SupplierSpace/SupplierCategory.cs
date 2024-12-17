using System.ComponentModel.DataAnnotations;

using LibreBooks.Models.Entity.CompanySpace;

using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Models.Entity.SupplierSpace
{
    public class SupplierCategory
    {
        public virtual string? Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Description { get; set; }
        public virtual string? CompanyId { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public virtual Company? Company { get; set; }
        public virtual ICollection<Supplier>? Suppliers { get; set; }

        public SupplierCategory ()
        {
            Id = Guid.NewGuid().ToString("N").ToUpper();
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<SupplierCategory>(options =>
            {
                options.ToTable(nameof(SupplierCategory))
                    .HasKey(p => p.Id)
                    .IsClustered(false);

                options.HasIndex(p => p.CompanyId)
                    .IsClustered()
                    .IsUnique();

                options.HasMany(p => p.Suppliers)
                    .WithOne(p => p.Category)
                    .HasForeignKey(p => p.CategoryId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}