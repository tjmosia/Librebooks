using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CompanySpace;

[Table(nameof(CompanyDefaultTaxType))]
public class CompanyDefaultTaxType
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public virtual int CompanyId { get; set; }
    public virtual int CompanyTaxTypeId { get; set; }

    public virtual CompanyTaxType? CompanyTaxType { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<CompanyDefaultTaxType>(options =>
        {
            options.HasOne<Company>()
                .WithOne()
                .HasForeignKey<CompanyDefaultTaxType>(p => p.CompanyId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
