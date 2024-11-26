using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using MacbooksAPI.Core.Types;
using MacbooksAPI.Models.Entity.BankingSpace;
using MacbooksAPI.Models.Entity.CompanySpace;
using MacbooksAPI.Models.Entity.CustomerSpace;
using MacbooksAPI.Models.Entity.SystemSpace;

namespace MacbooksAPI.Models.Entity.SalesSpace
{
    public class SalesReceipt
    {
        public virtual string? Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string? CustomerName { get; set; }
        public virtual string? Reference { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual string? Message { get; set; }
        public virtual string? Comments { get; set; }
        public virtual bool Archived { get; set; }
        public virtual bool Reconciled { get; set; }
        public virtual bool Recorded { get; set; }
        public virtual string? BankAccountId { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? CustomerId { get; set; }
        public virtual string? PaymentMethodId { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public void UpdateConcurrencyToken ()
            => RowVersion = Guid.NewGuid().ToString("N");

        public virtual Company? Company { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual BankAccount? BankAccount { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }
        public virtual ICollection<SalesInvoiceReceipt>? AllocatedInvoices { get; set; }

        public SalesReceipt ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<SalesReceipt>(options =>
            {
                options.ToTable(nameof(SalesReceipt))
                    .HasKey(p => p.Id)
                    .IsClustered(false);

                options.Property(p => p.Date)
                    .HasColumnType(ColumnTypes.Date);

                options.HasIndex(p => p.CompanyId)
                    .IsClustered();

                options.Property(p => p.Amount)
                    .HasColumnType(ColumnTypes.Monetary);

                options.HasMany(p => p.AllocatedInvoices)
                    .WithOne(p => p.Receipt)
                    .HasForeignKey(p => p.ReceiptId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
