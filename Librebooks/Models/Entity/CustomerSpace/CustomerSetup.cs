using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.CompanySpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CustomerSpace;

[Table(nameof(CustomerSetup))]
public class CustomerSetup () : VersionedEntityBase()
{
    [Key]
    public virtual int CompanyId { get; set; }

    [MaxLength(20)]
    public virtual string? Prefix { get; set; }

    [MaxLength(20)]
    public virtual string? Suffix { get; set; }

    public virtual int NextNumber { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<CustomerSetup>(options =>
        {
            options.HasOne<Company>()
                .WithOne()
                .HasForeignKey<CustomerSetup>(p => p.CompanyId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
