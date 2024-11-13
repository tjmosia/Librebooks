using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace OskitAPI.Models.Entity.AccountingSpace
{
    public class AccountCategory
    {
        public virtual string? Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Description { get; set; }
        public virtual string? ClassType { get; set; }
        public virtual string? CashFlowTypeId { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public void UpdateConcurrencyToken ()
            => RowVersion = Guid.NewGuid().ToString("N");

        public virtual ICollection<Account>? Accounts { get; set; }
        public virtual AccountCashFlowType? CashFlowType { get; set; }

        public AccountCategory ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
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
}