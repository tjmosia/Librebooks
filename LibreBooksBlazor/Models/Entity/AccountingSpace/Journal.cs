using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using OskitBlazor.Core.Types;
using OskitBlazor.Models.Entity.CompanySpace;

namespace OskitBlazor.Models.Entity.AccountingSpace
{
    public class Journal
    {
        public virtual string? Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string? Reference { get; set; }
        public virtual string? Description { get; set; }
        public virtual string? DebitAccountId { get; set; }
        public virtual string? CreditAccountId { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual decimal TaxRate { get; set; }
        public virtual bool Posted { get; set; }
        public virtual string? VATId { get; set; }
        public virtual string? CompanyId { get; set; }

        [Timestamp, ConcurrencyCheck]
        public virtual byte[]? RowVersion { get; set; }

        public virtual Company? Company { get; set; }
        public virtual CompanyValueAddedTax? VAT { get; set; }
        public virtual Account? DebitAccount { get; set; }
        public virtual Account? CreditAccount { get; set; }
        public virtual ICollection<JournalNote>? Notes { get; set; }

        public Journal ()
            => Id = Guid.NewGuid().ToString();

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<Journal>(options =>
            {
                options.ToTable(nameof(Journal))
                    .HasKey(p => p.Id)
                    .IsClustered(false);

                options.HasIndex(p => p.CompanyId)
                    .IsClustered();

                options.Property(p => p.Amount)
                    .HasColumnType(ColumnTypes.Monetary);

                options.Property(p => p.TaxRate)
                    .HasColumnType(ColumnTypes.Percentage);

                options.Property(p => p.Date)
                    .HasColumnType(ColumnTypes.Date);

                options.HasMany(p => p.Notes)
                    .WithOne()
                    .HasForeignKey(p => p.JournalId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
