using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CompanySpace;

[Table(nameof(CompanyRegionalSettings))]
public class CompanyRegionalSettings () : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public virtual int CompanyId { get; set; }

    [Required, MaxLength(1)]
    public virtual string? DecimalMark { get; set; }

    [Required, MaxLength(1)]
    public virtual string? ThousandsSeperator { get; set; }
    public virtual int DateFormatId { get; set; }
    public virtual int CountryId { get; set; }
    public virtual int CurrencyId { get; set; }
    public virtual int RoundToNearest { get; set; }

    public virtual DateFormat? DateFormat { get; set; }
    public virtual Country? Country { get; set; }
    public virtual Currency? Currency { get; set; }
    public virtual Company? Company { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<CompanyRegionalSettings>(options =>
        {
            options.HasOne(p => p.DateFormat)
                .WithOne()
                .HasForeignKey<CompanyRegionalSettings>(p => p.DateFormatId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasOne(p => p.Country)
                .WithOne()
                .HasForeignKey<CompanyRegionalSettings>(p => p.CountryId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasOne(p => p.Currency)
                .WithOne()
                .HasForeignKey<CompanyRegionalSettings>(p => p.CurrencyId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
