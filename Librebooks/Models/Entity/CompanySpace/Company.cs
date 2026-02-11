using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.AccountingSpace;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.PurchasesSpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SupplierSpace;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CompanySpace;

[Table(nameof(Company))]
public class Company () : VersionedEntityBase()
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [MaxLength(50)]
    public virtual string? UniqueNumber { get; set; }

    [Required]
    [MaxLength(100)]
    public virtual string? LegalName { get; set; }

    [MaxLength(100)]
    public virtual string? TradingName { get; set; }

    [Required]
    [MaxLength(20)]
    public virtual string? RegNumber { get; set; }

    [MaxLength(15)]
    public virtual string? VATNumber { get; set; }

    [MaxLength(155)]
    public virtual string? PostalAddress { get; set; }

    [MaxLength(155)]
    public virtual string? PhysicalAddress { get; set; }

    [Required]
    [MaxLength(15)]
    public virtual string? PhoneNumber { get; set; }

    [Required]
    [MaxLength(100)]
    public virtual string? EmailAddress { get; set; }

    [MaxLength(15)]
    public virtual string? FaxNumber { get; set; }

    public virtual int YearsInBusiness { get; set; }
    public virtual int? BusinessSectorId { get; set; }
    public virtual int? LogoId { get; set; }

    public virtual BusinessSector? BusinessSector { get; set; }
    public virtual CompanyRegionalSettings? RegionalSettings { get; set; }
    public virtual CompanyMailSettings? MailSettings { get; set; }
    public virtual ICollection<CompanyTaxType>? TaxTypes { get; set; }
    public virtual CompanyDefaultTaxType? DefaultTaxType { get; set; }
    public virtual ICollection<SalesPerson>? SalesPeople { get; set; }
    public virtual ICollection<PurchaseBuyer>? Buyers { get; set; }
    public virtual CompanyLogo? Logo { get; set; }

    public virtual ICollection<Account>? ChartOfAccounts { get; set; }
    public virtual ICollection<BankAccount>? BankAccounts { get; set; }
    public virtual CompanyDefaultBankAccount? DefaultBankAccount { get; set; }
    public virtual SupplierSetup? SupplierSetup { get; set; }
    public virtual CustomerSetup? CustomerSetup { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
        => builder.Entity<Company>(options =>
        {
            options.HasOne(p => p.RegionalSettings)
                .WithOne(p => p.Company)
                .HasForeignKey<CompanyRegionalSettings>(p => p.CompanyId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            options.HasOne(p => p.MailSettings)
                .WithOne(p => p.Company)
                .HasForeignKey<CompanyMailSettings>(p => p.CompanyId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            options.HasOne(p => p.DefaultBankAccount)
                .WithOne(p => p.Company)
                .HasForeignKey<CompanyDefaultBankAccount>(p => p.CompanyId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasOne(p => p.Logo)
                .WithOne()
                .HasForeignKey<CompanyLogo>(p => p.CompanyId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            options.HasOne(p => p.SupplierSetup)
                .WithOne()
                .HasForeignKey<SupplierSetup>(p => p.CompanyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            options.HasMany<CompanyImage>()
                .WithOne()
                .HasForeignKey(p => p.CompanyId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

        });
}
