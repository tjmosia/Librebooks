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
    public virtual string? ClassType { get; set; }
    public virtual int CashFlowTypeId { get; set; }

    public virtual ICollection<Account>? Accounts { get; set; }
    public virtual AccountCashFlowType? CashFlowType { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
        => builder.Entity<AccountCategory>(options =>
        {
            options.ToTable(nameof(AccountCategory))
                .HasKey(x => x.Id)
                .IsClustered();

            options.HasMany(p => p.Accounts)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
}