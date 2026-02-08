using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SupplierSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.GeneralSpace
{
    [Table(nameof(Contact))]
    public class Contact () : VersionedEntityBase()
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual string? FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual string? LastName { get; set; }

        [MaxLength(75)]
        public virtual string? Email { get; set; }

        [MaxLength(15)]
        public virtual string? Telephone { get; set; }

        [MaxLength(15)]
        public virtual string? Mobile { get; set; }

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<Contact>(options =>
            {
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
