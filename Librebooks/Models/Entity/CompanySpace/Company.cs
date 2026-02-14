using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CompanySpace;

[Table(nameof(Company))]
public class Company () : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [MaxLength(50)]
    public virtual string? UniqueNumber { get; set; }

    [Required]
    [MaxLength(100)]
    public virtual string? LegalName { get; set; }

    [MaxLength(100)]
    public virtual string? TradingName { get; set; }

    [Required]
    [MaxLength(20)]
    public virtual string? RegNumber { get; set; }

    [MaxLength(15)]
    public virtual string? VATNumber { get; set; }

    [MaxLength(155)]
    public virtual string? PostalAddress { get; set; }

    [MaxLength(155)]
    public virtual string? PhysicalAddress { get; set; }

    [Required]
    [MaxLength(15)]
    public virtual string? PhoneNumber { get; set; }

    [Required]
    [MaxLength(100)]
    public virtual string? EmailAddress { get; set; }

    [MaxLength(15)]
    public virtual string? FaxNumber { get; set; }

    public virtual int YearsInBusiness { get; set; }
    public virtual int BusinessSectorId { get; set; }

    public virtual BusinessSector? BusinessSector { get; set; }
    public virtual CompanyLogo? Logo { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
        => builder.Entity<Company>(options =>
        {
            options.HasOne(p => p.Logo)
                .WithOne()
                .HasForeignKey<CompanyLogo>(p => p.CompanyId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
}
