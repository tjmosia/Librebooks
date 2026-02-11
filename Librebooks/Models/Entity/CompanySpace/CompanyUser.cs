using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Models.Entity.SalesSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CompanySpace
{
    [Table(nameof(CompanyUser))]
    public class CompanyUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual int CompanyId { get; set; }

        public virtual Company? Company { get; set; }
        public virtual User? User { get; set; }

        public static void OnModelCreating (ModelBuilder builder)
        {
            builder.Entity<CompanyUser>(options =>
               {
                   options.HasIndex(p => new { p.UserId, p.CompanyId })
                       .IsUnique()
                       .IsClustered();

                   options.HasOne(p => p.Company)
                       .WithMany()
                       .HasForeignKey(p => p.CompanyId)
                           .IsRequired()
                       .OnDelete(DeleteBehavior.Restrict);

                   options.HasOne<SalesPerson>()
                       .WithOne(p => p.CompanyUser)
                       .HasForeignKey<SalesPerson>(p => p.CompanyUserId)
                           .IsRequired(false)
                       .OnDelete(DeleteBehavior.Restrict);
               });
        }
    }
}
