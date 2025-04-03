using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.AccountingSpace
{
    public class AccountCashFlowType
    {
        public virtual string Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Description { get; set; }

        [ConcurrencyCheck]
        public virtual string RowVersion { get; set; }

        public AccountCashFlowType ()
        {
            Id = Guid.NewGuid().ToString("N").ToUpper();
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<AccountCashFlowType>(options =>
            {
                options.ToTable(nameof(AccountCashFlowType))
                    .HasKey(x => x.Id)
                    .IsClustered();

                options.HasMany<AccountCategory>()
                    .WithOne(p => p.CashFlowType)
                    .HasForeignKey(p => p.CashFlowTypeId)
                        .IsRequired(true)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
