using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.AccountingSpace;

[Table(nameof(AccountCategory))]
public class AccountCategory () : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [Required, MaxLength(75)]
    public virtual string? Name { get; set; }

    [MaxLength(255)]
    public virtual string? Description { get; set; }

    [MaxLength(75)]
    public virtual string? ClassType { get; set; }

    public virtual int CashFlowTypeId { get; set; }

    public virtual AccountCashFlowType? CashFlowType { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<AccountCategory>(options =>
        {
            options.HasOne(p => p.CashFlowType)
                .WithMany()
                .HasForeignKey(p => p.CashFlowTypeId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}