using LibreBooks.Models.Entity.BankingSpace;

using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Models.Entity.CompanySpace
{
    public class CompanyDefaultBankAccount
    {
        public virtual string? CompanyId { get; set; }
        public virtual string? BankAccountId { get; set; }

        public virtual Company? Company { get; set; }
        public virtual BankAccount? BankAccount { get; set; }

        public CompanyDefaultBankAccount () { }
        public CompanyDefaultBankAccount (string companyId, string bankAccountId) : this()
        {
            CompanyId = companyId;
            BankAccountId = bankAccountId;
        }

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
