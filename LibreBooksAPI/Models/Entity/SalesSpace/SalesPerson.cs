using LibreBooks.Models.Entity.CompanySpace;
using LibreBooks.Models.Entity.CustomerSpace;
using LibreBooks.Models.Entity.GeneralSpace;

using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Models.Entity.SalesSpace
{
    public class SalesPerson
    {
        public virtual string? ContactId { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? CompanyUserId { get; set; }

        public virtual Company? Company { get; set; }
        public virtual Contact? Contact { get; set; }
        public virtual CompanyUser? CompanyUser { get; set; }
        public virtual ICollection<Customer>? Customers { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<SalesPerson>(options =>
            {
                options.ToTable(nameof(SalesPerson))
                    .HasKey(p => p.ContactId)
                    .IsClustered(false);

                options.HasIndex(p => new { p.CompanyId, p.ContactId })
                    .IsUnique()
                    .IsClustered();

                options.HasMany(p => p.Customers)
                    .WithOne(p => p.SalesPerson)
                    .HasForeignKey(p => p.SalesPersonId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.Customers)
                    .WithOne(p => p.SalesPerson)
                    .HasForeignKey(p => p.SalesPersonId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}