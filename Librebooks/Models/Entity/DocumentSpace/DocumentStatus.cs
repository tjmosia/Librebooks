using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.PurchasesSpace;
using Librebooks.Models.Entity.SalesSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.DocumentSpace;

[Table(nameof(DocumentStatus))]
public class DocumentStatus () : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [Required, MaxLength(50)]
    public virtual string? Name { get; set; }

    [Required, MaxLength(6)]
    public virtual string? Color { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<DocumentStatus>(options =>
        {
            options.HasMany<PurchaseDocument>()
                .WithOne(p => p.Status)
                .HasForeignKey(p => p.StatusId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasMany<SalesDocument>()
                .WithOne(p => p.Status)
                .HasForeignKey(p => p.StatusId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
