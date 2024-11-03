using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using OskitAPI.Models.Entity.CustomerSpace;
using OskitAPI.Models.Entity.SalesSpace;
using OskitAPI.Models.Entity.SupplierSpace;

namespace OskitAPI.Models.Entity.GeneralSpace
{
    public class Contact
    {
        public virtual string? Id { get; set; }
        public virtual string? FirstName { get; set; }
        public virtual string? LastName { get; set; }
        public virtual string? Email { get; set; }
        public virtual string? Telephone { get; set; }
        public virtual string? Mobile { get; set; }

        [Timestamp, ConcurrencyCheck]
        public virtual byte[]? RowVersion { get; set; }

        public Contact ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<Contact>(options =>
            {
                options.ToTable(nameof(Contact))
                    .HasKey(p => p.Id)
                    .IsClustered();

                options.HasOne<SalesPerson>()
                    .WithOne(p => p.Contact)
                    .HasForeignKey<SalesPerson>(p => p.ContactId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasOne<CustomerContact>()
                    .WithOne(p => p.Contact)
                    .HasForeignKey<CustomerContact>(p => p.ContactId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasOne<SupplierContact>()
                    .WithOne(p => p.Contact)
                    .HasForeignKey<SupplierContact>(p => p.ContactId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
