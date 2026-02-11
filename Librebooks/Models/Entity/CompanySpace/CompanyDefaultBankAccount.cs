using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Models.Entity.BankingSpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CompanySpace;

[Table(nameof(CompanyDefaultBankAccount))]
public class CompanyDefaultBankAccount ()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public virtual int CompanyId { get; set; }
    public virtual int BankAccountId { get; set; }

    public virtual Company? Company { get; set; }
    public virtual BankAccount? BankAccount { get; set; }

    public CompanyDefaultBankAccount (int companyId, int bankAccountId)
        : this()
    {
        CompanyId = companyId;
        BankAccountId = bankAccountId;
    }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<CompanyDefaultBankAccount>(options =>
           {
           });
    }
}
