using Librebooks.Areas.Systems.Services.Stores;
using Librebooks.Core.EFCore;
using Librebooks.CoreLib.Operations;
using Librebooks.Data;
using Librebooks.Models.Entity.CompanySpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Systems.Services;

public class SystemsStore (
	AppDbContext context,
	ILogger<DbStoreBase> logger,
	ShippingTermStore shippingTerms,
	ShippingMethodStore shippingMethods,
	CountryStore countries,
	CurrencyStore currencies,
	DateFormatStore dateFormats,
	CompanyNumberStore companyNumber,
	PaymentMethodStore paymentMethods,
	PaymentTermStore paymentTerms,
	TaxStore taxTypes,
	BusinessSectorStore businessSector)
	: DbStoreBase(context, logger)
{
	public readonly ShippingTermStore ShippingTermsStore = shippingTerms;
	public readonly ShippingMethodStore ShippingMethodsStore = shippingMethods;
	public readonly CountryStore CountriesStore = countries;
	public readonly CurrencyStore CurrenciesStore = currencies;
	public readonly DateFormatStore DateFormatsStore = dateFormats;
	public readonly CompanyNumberStore CompanyNumberStore = companyNumber;
	public readonly PaymentMethodStore PaymentMethodsStore = paymentMethods;
	public readonly PaymentTermStore PaymentTermsStore = paymentTerms;
	public readonly TaxStore TaxTypesStore = taxTypes;
	public readonly BusinessSectorStore BusinessSectorStore = businessSector;
	public async Task<CompanySetup?> GetCompanySetupAsync (CancellationToken cancellationToken = default)
		=> await context.CompanySetup!.FirstOrDefaultAsync(cancellationToken);

	public async Task<Result<CompanySetup>> UpdateCompanySetupAsync (CompanySetup companySetup, CancellationToken cancellationToken = default)
	{
		companySetup.RefreshConcurrencyToken();
		try
		{
			var result = context.CompanySetup!.Update(companySetup);
			await context.SaveChangesAsync(cancellationToken);
			return Result<CompanySetup>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			var errors = new List<Error>();

			if (ex is DbUpdateException)
			{
				errors.Add(Error.Create("", "Update unsuccessful. Please try again later."));
			}

			if (ex is DbUpdateConcurrencyException)
			{
				errors.Add(Error.Create("", "Update unsuccessful. Multiple users are editing the same data at the same time. Please try again."));
			}

			return Result<CompanySetup>.Failure();
		}
	}
}
