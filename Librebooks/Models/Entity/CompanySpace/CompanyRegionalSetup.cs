using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CompanySpace;

[Table(nameof(CompanyRegionalSetup))]
public class CompanyRegionalSetup () : VersionedEntityBase()
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
	public virtual int CompanyId { get; set; }

	[Required, MaxLength(1)]
	public virtual string? DecimalMark { get; set; }

	[Required, MaxLength(1)]
	public virtual string? ThousandsSeperator { get; set; }
	public virtual int DateFormatId { get; set; }
	public virtual int CountryId { get; set; }
	public virtual int CurrencyId { get; set; }
	public virtual int RoundToNearest { get; set; }

	public virtual DateFormat? DateFormat { get; set; }
	public virtual Country? Country { get; set; }
	public virtual Currency? Currency { get; set; }
	public virtual Company? Company { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
	{
		builder.Entity<CompanyRegionalSetup>(options =>
		{
			options.HasOne(p => p.DateFormat)
				.WithOne()
				.HasForeignKey<CompanyRegionalSetup>(p => p.DateFormatId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne(p => p.Country)
				.WithOne()
				.HasForeignKey<CompanyRegionalSetup>(p => p.CountryId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne(p => p.Currency)
				.WithOne()
				.HasForeignKey<CompanyRegionalSetup>(p => p.CurrencyId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne(p => p.Company)
				.WithOne()
				.HasForeignKey<CompanyRegionalSetup>(p => p.CompanyId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);
		});
	}
}
