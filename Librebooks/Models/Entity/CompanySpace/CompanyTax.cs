using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Models.Entity.AccountingSpace;
using Librebooks.Models.Entity.InventorySpace;
using Librebooks.Models.Entity.PurchasesSpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CompanySpace;

[Table(nameof(CompanyTax))]
public class CompanyTax ()
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public virtual int Id { get; set; }
	public virtual int CompanyId { get; set; }
	public virtual int TaxId { get; set; }
	public virtual bool Default { get; set; } = false;

	public CompanyTax (int companyId, int taxTypeId)
		: this()
	{
		CompanyId = companyId;
		TaxId = taxTypeId;
	}

	public virtual Tax? TaxType { get; set; }

	public static void BuildModel (ModelBuilder builder)
	{
		builder.Entity<CompanyTax>(options =>
		{
			options.HasIndex(p => new { p.CompanyId, p.TaxId })
				.IsUnique()
				.IsClustered();

			options.HasOne<Company>()
				.WithMany(p => p.Taxes)
				.HasForeignKey(p => p.CompanyId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne(p => p.TaxType)
				.WithOne()
				.HasForeignKey<CompanyTax>(p => p.TaxId)
				.IsRequired(true)
				.OnDelete(DeleteBehavior.Restrict);

			options.HasMany<JournalEntry>()
				.WithOne(p => p.Tax)
				.HasForeignKey(p => p.TaxId)
					.IsRequired(false)
				.OnDelete(DeleteBehavior.SetNull);

			options.HasMany<Account>()
				.WithOne(p => p.Tax)
				.HasForeignKey(p => p.TaxId)
					.IsRequired(false)
				.OnDelete(DeleteBehavior.SetNull);

			options.HasMany<SalesLine>()
				.WithOne(p => p.Tax)
				.HasForeignKey(p => p.TaxId)
					.IsRequired(false)
				.OnDelete(DeleteBehavior.SetNull);

			options.HasMany<PurchaseLine>()
				.WithOne(p => p.Tax)
				.HasForeignKey(p => p.TaxId)
					.IsRequired(false)
				.OnDelete(DeleteBehavior.SetNull);

			options.HasMany<Item>()
				.WithOne(p => p.TaxType)
				.HasForeignKey(p => p.TaxId)
					.IsRequired(false)
				.OnDelete(DeleteBehavior.SetNull);
		});
	}
}
