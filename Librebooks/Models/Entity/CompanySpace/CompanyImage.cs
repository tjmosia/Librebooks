using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.PurchasesSpace;
using Librebooks.Models.Entity.SalesSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CompanySpace;

[Table(nameof(CompanyImage))]
public class CompanyImage () : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }
    public virtual int CompanyId { get; set; }
    public virtual DateOnly DateCreated { get; set; }
    public virtual byte[]? Data { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<CompanyImage>(options =>
        {
            options.HasOne<CompanyLogo>()
                .WithOne(p => p.Image)
                .HasForeignKey<CompanyLogo>(p => p.ImageId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasMany<SalesDocument>()
                .WithOne(p => p.Logo)
                .HasForeignKey(p => p.LogoId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasMany<SalesReceipt>()
                .WithOne(p => p.Logo)
                .HasForeignKey(p => p.LogoId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasMany<PurchaseDocument>()
                .WithOne(p => p.Logo)
                .HasForeignKey(p => p.LogoId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasMany<PurchaseReceipt>()
                .WithOne(p => p.Logo)
                .HasForeignKey(p => p.LogoId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasOne<Company>()
                .WithMany()
                .HasForeignKey(p => p.CompanyId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

        });
    }
}
