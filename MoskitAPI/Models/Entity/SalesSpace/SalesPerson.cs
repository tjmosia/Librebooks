using Microsoft.EntityFrameworkCore;

using Moskit.Models.Entity.CompanySpace;
using Moskit.Models.Entity.CustomerSpace;
using Moskit.Models.Entity.GeneralSpace;

namespace Moskit.Models.Entity.SalesSpace
{
    public class SalesPerson
    {
        public virtual string? Id { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? ContactId { get; set; }
        public virtual string? CompanyUserId { get; set; }

        public virtual Company? Company { get; set; }
        public virtual Contact? Contact { get; set; }
        public virtual CompanyUser? CompanyUser { get; set; }
        public virtual ICollection<Customer>? Customers { get; set; }

        public SalesPerson ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<SalesPerson>(options =>
            {
                options.ToTable(nameof(SalesPerson))
                    .HasKey(p => p.Id)
                    .IsClustered(false);

                options.HasIndex(p => p.CompanyId)
                    .IsClustered();

                options.HasMany(p => p.Customers)
                    .WithOne(p => p.SalesPerson)
                    .HasForeignKey(p => p.SalesPersonId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}