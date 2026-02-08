using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CustomerSpace;

[Table(nameof(CustomerSetup))]
public class CustomerSetup () : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
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
            options.ToTable(nameof(CustomerSetup))
                .HasKey(p => p.CompanyId)
                .IsClustered();
        });
    }
}
