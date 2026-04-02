using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.SalesSpace;

namespace Librebooks.Areas.Companies.Services;

public partial class CompanyStore : ICompanyStore
{
	public async Task<Result> DeleteSalesPersonAsync (SalesPerson salesPerson)
	{
		try
		{
			context.SalesPeople!.Remove(salesPerson);
			context.Contacts!.Remove(salesPerson.Contact!);
			await context!.SaveChangesAsync();

			return Result.Success;
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occurred with Exception while trying to remove Sales Person:*** \n\n{message}", ex.Message);
			return Result.Failure();
		}
	}

	public async Task<Result> DeleteAsync (Company company)
	{

		try
		{
			context.Companies!.Remove(company);
			await context.SaveChangesAsync();
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occurred with Exception while removing Company:*** \n\n{message}", ex.Message);
			return Result.Failure();
		}
		return Result.Success;
	}

	public async Task<Result> DeleteTaxTypeAsync (CompanyTax companyTaxType)
	{
		ArgumentNullException.ThrowIfNull(companyTaxType.TaxType, nameof(companyTaxType.TaxType));

		if (companyTaxType.TaxType!.System)
			return Result.Failure(Error.Create("", "Cannot remove a system tax type."));

		try
		{
			context.CompanyTaxes!.Remove(companyTaxType);
			context.Taxes!.Remove(companyTaxType.TaxType);
			await context.SaveChangesAsync();
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occurred with Exception while trying to remove Sales Person:*** \n\n{message}", ex.Message);
			return Result.Failure();
		}

		return Result.Success;
	}

	public async Task<Result> DeleteBankAccountAsync (BankAccount bankAccount)
	{
		try
		{
			context.BankAccounts!.Remove(bankAccount);
			await context.SaveChangesAsync();
			return Result.Success;
		}
		catch (Exception)
		{
			return Result.Failure();
		}
	}

	public async Task<Result> DeleteLogoAsync (CompanyLogo companyLogo)
	{
		try
		{
			context.CompanyLogos!.Remove(companyLogo);
			await context.SaveChangesAsync();
			return Result.Success;
		}
		catch (Exception)
		{
			return Result.Failure();
		}
	}
}
