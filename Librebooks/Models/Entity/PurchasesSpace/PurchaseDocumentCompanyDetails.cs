using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.CompanySpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.PurchasesSpace;

[Table(nameof(PurchaseDocumentCompanyDetails))]
public class PurchaseDocumentCompanyDetails () : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }
    public virtual int CompanyId { get; set; }

    [Required, MaxLength(75)]
    public virtual string? CompanyName { get; set; }

    [MaxLength(155)]
    public virtual string? PhysicalAddress { get; set; }

    [MaxLength(155)]
    public virtual string? PostalAddress { get; set; }

    [MaxLength(10)]
    public virtual string? VATNumber { get; set; }
    public virtual int? LogoId { get; set; }
    public virtual DateOnly DateCreated { get; set; }
    public virtual bool Active { get; set; }
    public virtual string? CurrencyCode { get; set; }

    public virtual CompanyImage? Logo { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<PurchaseDocumentCompanyDetails>(options =>
        {
            options.HasIndex(p => new { p.CompanyId, p.Id })
                .IsClustered();

            options.HasOne<Company>()
                .WithOne()
                .HasForeignKey<PurchaseDocumentCompanyDetails>(p => p.CompanyId)
                    .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            options.HasOne(p => p.Logo)
                .WithMany()
                .HasForeignKey(p => p.LogoId)
                    .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
