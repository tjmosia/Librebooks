using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Core.Types;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.SystemSpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CustomerSpace;

[Table(nameof(CustomerAdjustment))]
public class CustomerAdjustment
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [MaxLength(50), Required]
    public virtual string? Number { get; set; }

    [MaxLength(50)]
    public virtual string? Reference { get; set; }

    public virtual int TaxTypeId { get; set; }

    [Column(TypeName = ColumnTypes.PERCENTATE)]
    public virtual int TaxRate { get; set; }

    [Column(TypeName = ColumnTypes.MONETARY)]
    public virtual decimal? ExclusiveAmount { get; set; }

    [MaxLength(255)]
    public virtual string? Description { get; set; }

    [MaxLength(255)]
    public virtual string? Comments { get; set; }

    public virtual int CompanyId { get; set; }
    public virtual int CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }
    public virtual TaxType? TaxType { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<CustomerAdjustment>(options =>
        {
            options.HasIndex(p => new { p.CompanyId, p.CustomerId, p.Id })
                .IsClustered();

            options.HasOne<Company>()
                .WithMany()
                .HasForeignKey(p => p.CompanyId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasOne(p => p.Customer)
                .WithMany()
                .HasForeignKey(p => p.CustomerId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
