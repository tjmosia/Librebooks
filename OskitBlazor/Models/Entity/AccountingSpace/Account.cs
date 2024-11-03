using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using OskitBlazor.Core.Types;
using OskitBlazor.Models.Entity.CompanySpace;

namespace OskitBlazor.Models.Entity.AccountingSpace
{
    public class Account
    {
        public virtual string? Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual decimal Balance { get; set; }
        public virtual string? VATId { get; set; }
        public virtual string? Description { get; set; }
        public virtual string? ParentAccountId { get; set; }
        public virtual bool Active { get; set; }
        public virtual bool System { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? CategoryId { get; set; }

        [Timestamp, ConcurrencyCheck]
        public virtual byte[]? RowVersion { get; set; }

        public virtual Account? ParentAccount { get; set; }
        public virtual Company? Company { get; set; }
        public virtual AccountCategory? Category { get; set; }
        public virtual CompanyValueAddedTax? VAT { get; set; }

        public virtual ICollection<Journal>? DebitHistory { get; set; }
        public virtual ICollection<Journal>? CreditHistory { get; set; }
        public virtual ICollection<Account>? SubAccounts { get; set; }

        public Account ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<Account>(options =>
            {
                options.ToTable(nameof(Account))
                    .HasKey(p => p.Id)
                    .IsClustered(false);

                options.HasIndex(p => new { p.CompanyId, p.CategoryId })
                    .IsClustered();

                options.Property(p => p.Balance)
                    .HasColumnType(ColumnTypes.Monetary);

                options.HasMany(p => p.SubAccounts)
                    .WithOne(p => p.ParentAccount)
                    .HasForeignKey(p => p.ParentAccountId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.DebitHistory)
                    .WithOne(p => p.DebitAccount)
                    .HasForeignKey(p => p.DebitAccountId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany(p => p.CreditHistory)
                    .WithOne(p => p.CreditAccount)
                    .HasForeignKey(p => p.CreditAccountId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
