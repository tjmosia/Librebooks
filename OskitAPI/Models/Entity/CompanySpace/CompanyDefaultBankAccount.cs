using Microsoft.EntityFrameworkCore;

using MacbooksAPI.Models.Entity.BankingSpace;

namespace MacbooksAPI.Models.Entity.CompanySpace
{
    public class CompanyDefaultBankAccount
    {
        public virtual string? CompanyId { get; set; }
        public virtual string? BankAccountId { get; set; }

        public virtual Company? Company { get; set; }
        public virtual BankAccount? BankAccount { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<CompanyDefaultBankAccount>(options =>
            {
                options.ToTable(nameof(CompanyDefaultBankAccount))
                    .HasKey(a => a.CompanyId)
                    .IsClustered();

                options.HasOne(p => p.Company)
                    .WithOne()
                    .HasForeignKey<CompanyDefaultBankAccount>(p => p.CompanyId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
