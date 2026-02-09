using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Core.Types;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.CompanySpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.AccountingSpace;

[Table(nameof(Journal))]
public class Journal () : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [Column(ColumnTypes.DATETIME)]
    public virtual DateTime CreatedAt { get; set; }

    [MaxLength(75)]
    public virtual string? Reference { get; set; }

    [MaxLength(255)]
    public virtual string? Description { get; set; }
    public virtual int DebitAccountId { get; set; }
    public virtual int CreditAccountId { get; set; }

    [Column(TypeName = ColumnTypes.MONETARY)]
    public virtual decimal Amount { get; set; }

    [Column(TypeName = ColumnTypes.PERCENTATE)]
    public virtual decimal TaxRate { get; set; }
    public virtual bool Posted { get; set; }
    public virtual int TaxTypeId { get; set; }
    public virtual int CompanyId { get; set; }

    public virtual Company? Company { get; set; }
    public virtual CompanyTaxType? TaxType { get; set; }
    public virtual Account? DebitAccount { get; set; }
    public virtual Account? CreditAccount { get; set; }
    public virtual ICollection<JournalNote>? Notes { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<Journal>(options =>
        {
            options.HasIndex(p => new { p.CompanyId, p.Id })
                .IsClustered();

            options.HasMany(p => p.Notes)
                .WithOne()
                .HasForeignKey(p => p.JournalId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
