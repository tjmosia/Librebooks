using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.GeneralSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SalesSpace
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

        public SalesPerson () { }

        public SalesPerson (string companyId, string contactId, string? companyUserId = null)
            : this()
        {
            CompanyId = companyId;
            CompanyUserId = companyUserId;
            ContactId = contactId;
        }

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
            });
    }
}