using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibrebooksBlazor.Core.Types;
using LibrebooksBlazor.Extensions.Models;
using LibrebooksBlazor.Models.Entity.BankingSpace;
using LibrebooksBlazor.Models.Entity.CompanySpace;
using LibrebooksBlazor.Models.Entity.CustomerSpace;
using LibrebooksBlazor.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace LibrebooksBlazor.Models.Entity.SalesSpace;

[Table(nameof(SalesReceipt))]
public class SalesReceipt () : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }
    public virtual DateOnly Date { get; set; }

    [MaxLength(50)]
    public virtual string? Number { get; set; }

    [MaxLength(50)]
    public virtual string? Reference { get; set; }

    [Column(TypeName = ColumnTypes.MONETARY)]
    public virtual decimal Amount { get; set; }

    [MaxLength(255)]
    public virtual string? Message { get; set; }

    [MaxLength(255)]
    public virtual string? Comments { get; set; }

    public virtual bool Archived { get; set; }
    public virtual bool Reconciled { get; set; }
    public virtual bool Recorded { get; set; }
    public virtual int BankAccountId { get; set; }
    public virtual int CompanyId { get; set; }
    public virtual int CustomerId { get; set; }
    public virtual int PaymentMethodId { get; set; }

    public virtual BankAccount? BankAccount { get; set; }
    public virtual PaymentMethod? PaymentMethod { get; set; }
    public virtual ICollection<SalesInvoiceReceipt>? AllocatedInvoices { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<SalesReceipt>(options =>
          {
              options.HasIndex(p => new { p.CompanyId, p.Id })
                  .IsClustered();

              options.HasMany(p => p.AllocatedInvoices)
                  .WithOne(p => p.Receipt)
                  .HasForeignKey(p => p.ReceiptId)
                      .IsRequired()
                  .OnDelete(DeleteBehavior.Restrict);

              options.HasOne<Company>()
                  .WithMany()
                  .HasForeignKey(p => p.CompanyId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Restrict);

              options.HasOne<Customer>()
                  .WithMany()
                  .HasForeignKey(p => p.CustomerId)
                      .IsRequired(true)
                  .OnDelete(DeleteBehavior.Restrict);
          });
    }
}
