using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.DocumentSpace;
using Librebooks.Models.Entity.InventorySpace;
using Librebooks.Models.Entity.SupplierSpace;
using Librebooks.Models.Entity.SystemSpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Companies.Services;

public partial class CompanyStore : ICompanyStore
{
	public async Task<Result<Company>> UpdateAsync (Company company, CancellationToken cancellation = default)
	{
		company.RefreshConcurrencyToken();

		try
		{
			var result = context.Companies!.Update(company);
			await context.SaveChangesAsync(cancellation);
			return Result<Company>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			IList<Error> errors = [];

			if (ex is DbUpdateConcurrencyException)
			{
				errors.Add(Error.Create("", "Data has already changed. Please try again."));
			}

			if (ex is DbUpdateException)
			{
				errors.Add(Error.Create("", "Unable to update company. Please try again later."));
			}

			return Result<Company>.Failure([.. errors]);
		}
	}

	public async Task<Result<CompanyRegionalSetup>> UpdateRegionalSettingsAsync (CompanyRegionalSetup regionalSettings, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = context!.CompanyRegionalSettings!.Update(regionalSettings);
			await context.SaveChangesAsync(cancellationToken);
			return Result<CompanyRegionalSetup>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occured with Exception while trying to update CompanyRegionalSettings:*** \n\n{message}", ex.Message);
			return Result<CompanyRegionalSetup>.Failure();
		}
	}

	public async Task<Result<CompanyBankAccount>> UpdateDefaultTaxTypeAsync (CompanyTax defaultTaxType, CancellationToken cancellationToken = default)
	{
		try
		{
			defaultTaxType.Default = true;
			context!.CompanyTaxes!.Update(defaultTaxType);
			await context!.SaveChangesAsync(cancellationToken);
			return Result<CompanyBankAccount>.Success();
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occurred with Exception while trying to update CompanyDefaultTaxType:*** \n\n{message}", ex.Message);
			return Result<CompanyBankAccount>.Failure();
		}
	}

	public async Task<Result<Tax>> UpdateTaxAsync (Tax tax, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = context!.Taxes!.Update(tax);
			await context.SaveChangesAsync(cancellationToken);
			return Result<Tax>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occurred with Exception while trying to update CompanyDefaultTaxType:*** \n\n{message}", ex.Message);
			return Result<Tax>.Failure();
		}
	}

	public async Task<Result<CompanyImage>> UpdateLogoAsync (CompanyImage companyImage, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = await context!.CompanyImages!.AddAsync(companyImage);
			var companyLogo = await context.CompanyLogos!.FindAsync(companyImage.CompanyId!);

			if (companyLogo == null)
			{
				await context!.CompanyLogos!.AddAsync(new CompanyLogo(companyImage.CompanyId!, companyImage.Id));
			}
			else
			{
				companyLogo.ImageId = companyImage.CompanyId;
				context!.CompanyLogos!.Update(companyLogo);
			}

			await context!.SaveChangesAsync(cancellationToken);
			return Result<CompanyImage>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occurred with Exception while trying to Update Company Logo:*** \n\n{message}", ex.Message);
			return Result<CompanyImage>.Failure();
		}
	}

	public async Task<Result> UpdateMailSettingsAsync (CompanyMailSetup companyMailSettings, CancellationToken cancellationToken = default)
	{
		try
		{
			context!.CompanyMailSettings!.Update(companyMailSettings);
			await context!.SaveChangesAsync(cancellationToken);
			return Result.Success;
		}
		catch (Exception)
		{
			return Result.Failure();
		}
	}

	public async Task<Result<SupplierSetup>> UpdateSupplierSetupAsync (SupplierSetup supplierSetup, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = context.SupplierSetups!.Update(supplierSetup);
			await context.SaveChangesAsync(cancellationToken);
			return Result<SupplierSetup>.Success(result.Entity);
		}
		catch (Exception)
		{
			return Result<SupplierSetup>.Failure();
		}
	}

	public async Task<Result<CustomerSetup>> UpdateCustomerSetupAsync (CustomerSetup customerSetup, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = context.CustomerSetups!.Update(customerSetup);
			await context.SaveChangesAsync(cancellationToken);
			return Result<CustomerSetup>.Success(result.Entity);
		}
		catch (Exception)
		{
			return Result<CustomerSetup>.Failure();
		}
	}

	public async Task<Result<ItemSetup>> UpdateItemSetupAsync (ItemSetup itemSetup, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = context.ItemSetups!.Update(itemSetup);
			await context.SaveChangesAsync(cancellationToken);
			return Result<ItemSetup>.Success(result.Entity);
		}
		catch (Exception)
		{
			return Result<ItemSetup>.Failure();
		}
	}

	public async Task<Result<DocumentSetup>> UpdateDocumentSetupAsync (DocumentSetup documentSetup, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = context.DocumentSetups!.Update(documentSetup);
			await context.SaveChangesAsync(cancellationToken);
			return Result<DocumentSetup>.Success(result.Entity);
		}
		catch (Exception)
		{
			return Result<DocumentSetup>.Failure();
		}
	}

	public async Task<Result<BankAccount>> UpdateBankAccountAsync (BankAccount bankAccount, CancellationToken cancellationToken = default)
	{
		try
		{
			var result = context.BankAccounts!.Update(bankAccount);
			await context.SaveChangesAsync(cancellationToken);
			return Result<BankAccount>.Success(result.Entity);
		}
		catch (Exception)
		{
			return Result<BankAccount>.Failure();
		}

	}

}
