using System.ComponentModel.DataAnnotations;

using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SupplierSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.GeneralSpace
{
    public class Contact
    {
        public virtual string Id { get; set; }
        public virtual string? FirstName { get; set; }
        public virtual string? LastName { get; set; }
        public virtual string? Email { get; set; }
        public virtual string? Telephone { get; set; }
        public virtual string? Mobile { get; set; }

        [ConcurrencyCheck]
        public virtual string RowVersion { get; set; }

        public Contact ()
        {
            Id = Guid.NewGuid().ToString("N").ToUpper();
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

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
