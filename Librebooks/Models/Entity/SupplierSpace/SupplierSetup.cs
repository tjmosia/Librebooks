using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.CompanySpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SupplierSpace
{
    [Table(nameof(SupplierSetup))]
    public class SupplierSetup () : VersionedEntityBase()
    {
        [Key]
        public virtual int CompanyId { get; set; }

        [MaxLength(50)]
        public virtual string? Prefix { get; set; }

        [MaxLength(50)]
        public virtual string? Suffix { get; set; }

        public virtual int NextNumber { get; set; }

        [MaxLength(10)]
        public virtual string? NumberFormat { get; set; }

        public static void OnModelCreating (ModelBuilder builder)
        {
            builder.Entity<SupplierSetup>(options =>
               {
                   options.HasOne<Company>()
                       .WithOne()
                       .HasForeignKey<SupplierSetup>(p => p.CompanyId)
                           .IsRequired()
                       .OnDelete(DeleteBehavior.Restrict);
               });
        }
    }
}
