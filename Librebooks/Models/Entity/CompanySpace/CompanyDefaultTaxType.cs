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
    public virtual Company? Company { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<CompanyDefaultTaxType>(options =>
           {

           });
    }
}
