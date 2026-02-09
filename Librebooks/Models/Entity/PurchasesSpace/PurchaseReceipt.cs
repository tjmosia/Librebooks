using System.ComponentModel.DataAnnotations;

using Librebooks.Core.Types;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.PurchasesSpace
{
    public class PurchaseReceipt
    {
        public virtual string? Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string? Number { get; set; }
        public virtual string? SupplierDetailsId { get; set; }
        public virtual string? Reference { get; set; }
        public virtual string? Description { get; set; }
        public virtual string? Comments { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual bool Reconciled { get; set; }
        public virtual bool Recorded { get; set; }
        public virtual string? SupplierId { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? BankAccountId { get; set; }
        public virtual string? PaymentMethodId { get; set; }
        public virtual string? LogoId { get; set; }
        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public virtual CompanyImage? Logo { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }
        public virtual Company? Company { get; set; }
        public virtual BankAccount? BankAccount { get; set; }
        public virtual PurchaseDocumentSupplierDetails? SupplierDetails { get; set; }
        public virtual ICollection<PurchaseInvoiceReceipt>? AllocatedInvoices { get; set; }

        public PurchaseReceipt ()
        {
            Id = Guid.NewGuid().ToString("N").ToUpper();
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<PurchaseReceipt>(options =>
            {
                options.ToTable(nameof(PurchaseReceipt))
                    .HasKey(x => x.Id)
                    .IsClustered(false);

                options.HasIndex(p => new { p.CompanyId, p.SupplierId })
                    .IsClustered()
                    .IsUnique();

                options.Property(p => p.Date)
                    .HasColumnType(ColumnTypes.DATE);

                options.Property(p => p.Amount)
                    .HasColumnType(ColumnTypes.MONETARY);

                options.HasOne(p => p.PaymentMethod)
                    .WithMany()
                    .HasForeignKey(p => p.PaymentMethodId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.AllocatedInvoices)
                    .WithOne(p => p.Receipt)
                    .HasForeignKey(p => p.ReceiptId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
