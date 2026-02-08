using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Models.Entity.AccountingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CustomerSpace;

[Table(nameof(CustomerAdjustment))]
public class CustomerAdjustment
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int JournalId { get; set; }
    public virtual int CompanyId { get; set; }
    public virtual int CustomerId { get; set; }

    public virtual Company? Company { get; set; }
    public virtual Journal? Journal { get; set; }
    public virtual Customer? Customer { get; set; }

    public static void BuildModel (ModelBuilder builder)
    {
        builder.Entity<CustomerAdjustment>(options =>
        {
            options.HasIndex(p => new { p.CompanyId, p.CustomerId, p.JournalId })
                .IsClustered();

            options.HasOne(p => p.Journal)
                .WithOne()
                .HasForeignKey<CustomerAdjustment>(p => p.JournalId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
