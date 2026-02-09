using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.AccountingSpace;

public class AccountCashFlowType () : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [Required, MaxLength(75)]
    public virtual string? Name { get; set; }

    [MaxLength(255)]
    public virtual string? Description { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<AccountCashFlowType>(options =>
        {
            options.HasMany<AccountCategory>()
                .WithOne(p => p.CashFlowType)
                .HasForeignKey(p => p.CashFlowTypeId)
                    .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
