using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CompanySpace;

[Table(nameof(CompanyMailSettings))]
public class CompanyMailSettings () : VersionedEntityBase()
{
    public virtual int CompanyId { get; set; }
    [Required]

    [MaxLength(75)]
    public virtual string? EmailAddress { get; set; }

    [Required]
    [MaxLength(100)]
    public virtual string? Password { get; set; }

    [Required]
    [MaxLength(255)]
    public virtual string? SmtpServerName { get; set; }

    [Required, MaxLength(20)]
    public virtual string? SmtpPort { get; set; }

    public virtual Company? Company { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
        => builder.Entity<CompanyMailSettings>(options =>
        {
            options.ToTable(nameof(CompanyMailSettings))
                .HasKey(p => p.CompanyId);
        });
}
