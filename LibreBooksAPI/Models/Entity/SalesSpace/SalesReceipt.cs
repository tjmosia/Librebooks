using System.ComponentModel.DataAnnotations;

using LibreBooks.Core.Types;
using LibreBooks.Models.Entity.BankingSpace;
using LibreBooks.Models.Entity.CompanySpace;
using LibreBooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Models.Entity.SalesSpace
{
    public class SalesReceipt
    {
        public virtual string? Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string? CustomerDetailsId { get; set; }
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
        public virtual string? LogoId { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public virtual CompanyImage? Logo { get; set; }
        public virtual Company? Company { get; set; }
        public virtual SalesDocumentCustomerDetails? CustomerDetails { get; set; }
        public virtual BankAccount? BankAccount { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }
        public virtual ICollection<SalesInvoiceReceipt>? AllocatedInvoices { get; set; }

        public SalesReceipt ()
        {
            Id = Guid.NewGuid().ToString("N").ToUpper();
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

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
