using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.CompanySpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SalesSpace;

[Table(nameof(SalesDocumentCompanyDetails))]
public class SalesDocumentCompanyDetails () : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }
    public virtual int CompanyId { get; set; }

    [Required, MaxLength(75)]
    public virtual int CompanyName { get; set; }

    [MaxLength(155)]
    public virtual string? PhysicalAddress { get; set; }

    [MaxLength(155)]
    public virtual string? PostalAddress { get; set; }

    [MaxLength(10)]
    public virtual string? VATNumber { get; set; }
    public virtual int? LogoId { get; set; }
    public virtual DateOnly DateCreated { get; set; }
    public virtual bool Active { get; set; }

    public virtual CompanyImage? Logo { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
        => builder.Entity<SalesDocumentCompanyDetails>(options =>
        {
            options.HasIndex(p => new { p.CompanyId, p.Id })
                .IsClustered();

            options.HasOne<Company>()
                .WithOne()
                .HasForeignKey<SalesDocumentCompanyDetails>(p => p.CompanyId)
                    .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            options.HasOne(p => p.Logo)
                .WithMany()
                .HasForeignKey(p => p.LogoId)
                    .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            options.HasMany<SalesDocument>()
                .WithOne(p => p.CompanyDetails)
                .HasForeignKey(propa => propa.CompanyDetailsId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasMany<SalesReceipt>()
                .WithOne(p => p.CompanyDetails)
                .HasForeignKey(p => p.CustomerDetailsId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });
}
