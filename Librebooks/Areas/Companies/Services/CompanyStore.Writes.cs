using Librebooks.CoreLib.Operations;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.GeneralSpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SystemSpace;

namespace Librebooks.Areas.Companies.Services;

public partial class CompanyStore : ICompanyStore
{
	/***********************************************************************************************************************************
	****** INSERT TRANSACTIONS
	***********************************************************************************************************************************/

	public async Task<Result<Company>> CreateAsync (Company company)
	{
		try
		{
			var result = await context!.AddAsync(company);
			context.SaveChanges();
			return Result<Company>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error with Exception occurred while trying to create Company:*** \n\n{message}", ex.Message);
			return Result<Company>.Failure();
		}
	}

	public async Task<Result<Tax>> CreateTaxTypeAsync (Company company, Tax taxType)
	{
		try
		{
			var result = await context.Taxes!.AddAsync(taxType);
			await context.CompanyTaxes!.AddAsync(new CompanyTax(company.Id, taxType.Id));
			await context.SaveChangesAsync();

			return Result<Tax>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occured with Exception while creating Company TaxType:*** \n\n{message}", ex.Message);
			return Result<Tax>.Failure();
		}
	}

	public async Task<Result<Contact>> CreateSalesPersonAsync (Company company, Contact contact)
	{
		try
		{
			var result = await context.Contacts!.AddAsync(contact);

			await context.SalesPeople!.AddAsync(new SalesPerson
			{
				CompanyId = company.Id,
				ContactId = contact.Id,
			});

			await context.SaveChangesAsync();

			return Result<Contact>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occured with Exception while creating Company TaxType:*** \n\n{message}", ex.Message);
			return Result<Contact>.Failure();
		}
	}

	public async Task<Result<BankAccount>> CreateBankAccountAsync (Company company, BankAccount bankAccount)
	{
		try
		{
			bankAccount.CompanyId = company.Id;
			var result = await context.BankAccounts!.AddAsync(bankAccount);
			await context.SaveChangesAsync();
			return Result<BankAccount>.Success(result.Entity);
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occured with Exception while creating Company Bank Account:*** \n\n{message}", ex.Message);
			return Result<BankAccount>.Failure();
		}
	}

	public async Task<Result<BankAccount>> CreateDefaultBankAccountAsync (Company company, BankAccount bankAccount)
	{
		try
		{
			var result = await context.CompanyDefaultBankAccounts!
				.AddAsync(new CompanyBankAccount(company.Id, bankAccount.Id));
			await context.SaveChangesAsync();
			return Result<BankAccount>.Success(bankAccount);
		}
		catch (Exception ex)
		{
			logger!.LogError("***DB Error occured with Exception while creating Company Default Bank Account:*** \n\n{message}", ex.Message);
			return Result<BankAccount>.Failure();
		}
	}

	public async Task<Result<CompanyImage>> CreateLogoAsync (Company company, CompanyImage image)
	{
		try
		{
			var result = await context.CompanyImages!.AddAsync(image);
			await context.SaveChangesAsync();
			await UpdateLogoAsync(result.Entity);
			return Result<CompanyImage>.Success(result.Entity);
		}
		catch (Exception)
		{
			return Result<CompanyImage>.Failure();
		}
	}


}
