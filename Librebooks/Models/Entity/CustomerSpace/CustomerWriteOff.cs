using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Core.Types;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.SalesSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CustomerSpace;

[Table(nameof(CustomerWriteOff))]
public class CustomerWriteOff () : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }
    public virtual string? CustomerName { get; set; }
    public virtual string? Number { get; set; }
    public virtual decimal Amount { get; set; }
    public virtual string? Description { get; set; }
    public virtual string? Reference { get; set; }
    public virtual DateTime Date { get; set; }
    public virtual string? CompanyId { get; set; }
    public virtual string? CustomerId { get; set; }

    public virtual ICollection<SalesInvoice>? Invoices { get; set; }
    public virtual ICollection<SalesQuote>? Quotes { get; set; }
    public virtual ICollection<SalesInvoice>? SalesOrders { get; set; }

    public static void BuildModel (ModelBuilder builder)
        => builder.Entity<CustomerWriteOff>(options =>
        {
            options.ToTable(nameof(CustomerWriteOff))
                .HasKey(p => p.Id)
                .IsClustered(false);

            options.Property(p => p.Date)
                .HasColumnType(ColumnTypes.Date);

            options.Property(p => p.Amount)
                .HasColumnType(ColumnTypes.Monetary);

            options.HasIndex(p => new { p.CompanyId, p.CustomerId })
                .IsClustered();

            options.HasIndex(p => new { p.CompanyId, p.Number })
                .IsUnique();
        });
}
