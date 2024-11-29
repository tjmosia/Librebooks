using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using LibreBooks.Core.Types;
using LibreBooks.Models.Entity.SalesSpace;

namespace LibreBooks.Models.Entity.CustomerSpace
{
    public class CustomerWriteOff
    {
        public virtual string? Id { get; set; }
        public virtual string? CustomerName { get; set; }
        public virtual string? Number { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual string? Description { get; set; }
        public virtual string? Reference { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? CustomerId { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public void UpdateConcurrencyToken ()
            => RowVersion = Guid.NewGuid().ToString("N");

        public virtual ICollection<SalesInvoice>? Invoices { get; set; }

        public CustomerWriteOff ()
            => Id = Guid.NewGuid().ToString("N");

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
}
