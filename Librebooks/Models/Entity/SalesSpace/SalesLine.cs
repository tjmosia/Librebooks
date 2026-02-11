using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Core.Types;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.InventorySpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SalesSpace;

[Table(nameof(SalesLine))]
public class SalesLine () : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }
    public virtual bool IsItemType { get; set; }

    [MaxLength(50)]
    public virtual string? ItemCode { get; set; }
    public virtual int AccountId { get; set; }
    [MaxLength(255)]
    public virtual string? Description { get; set; }

    [MaxLength(20)]
    public virtual string? Unit { get; set; }


    [Column(TypeName = ColumnTypes.PERCENTATE)]
    public virtual decimal Price { get; set; }


    [Column(TypeName = ColumnTypes.PERCENTATE)]
    public virtual decimal DiscountRate { get; set; }


    [Column(TypeName = ColumnTypes.PERCENTATE)]
    public virtual decimal TaxRate { get; set; }

    public virtual int TaxTypeId { get; set; }

    [MaxLength(500)]
    public virtual string? Comment { get; set; }

    public virtual ICollection<SalesDocumentLine>? DocumentLines { get; set; }
    public virtual Item? Item { get; set; }
    public virtual CompanyTaxType? TaxType { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<SalesLine>(options =>
        {
            options.HasMany(p => p.DocumentLines)
                .WithOne(p => p.Line)
                .HasForeignKey(p => p.LineId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasOne<Item>()
                 .WithMany()
                 .HasPrincipalKey(i => i.Code)
                 .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
