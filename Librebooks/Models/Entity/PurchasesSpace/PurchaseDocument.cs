using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.DocumentSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.PurchasesSpace;

[Table(nameof(PurchaseDocument))]
public class PurchaseDocument () : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }
    public virtual DateOnly Date { get; set; }
    public virtual DateOnly? DueDate { get; set; }

    [MaxLength(50)]
    public virtual string? Number { get; set; }

    [MaxLength(50)]
    public virtual string? SuppplierReference { get; set; }

    [MaxLength(255)]
    public virtual string? Message { get; set; }

    [MaxLength(500)]
    public virtual string? Footer { get; set; }

    public virtual string? Currency { get; set; }
    public virtual bool Printed { get; set; }
    public virtual int? SupplierInfoId { get; set; }
    public virtual int CompanyInfoId { get; set; }
    public virtual int StatusId { get; set; }

    public virtual DocumentStatus? Status { get; set; }
    public virtual ICollection<PurchaseDocumentLine>? Lines { get; set; }
    public virtual ICollection<PurchaseDocumentNote>? Notes { get; set; }
    public virtual DocumentSupplierInfo? SupplierInfo { get; set; }
    public virtual DocumentCompanyInfo? CompanyInfo { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<PurchaseDocument>(options =>
        {
            options.HasMany(p => p.Lines)
                .WithOne(p => p.Document)
                .HasForeignKey(p => p.DocumentId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasMany(p => p.Notes)
                .WithOne()
                .HasForeignKey(p => p.DocumentId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            options.HasOne(p => p.Status)
                .WithMany()
                .HasForeignKey(p => p.StatusId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasOne(p => p.SupplierInfo)
                .WithMany()
                .HasForeignKey(p => p.SupplierInfoId)
                    .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

        });
    }
}
