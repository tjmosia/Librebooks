using Librebooks.Areas.Systems.Services;
using Librebooks.Data;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.GeneralSpace;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Companies.Services;

public partial class CompanyStore (AppDbContext context, ILogger<CompanyStore> logger, ISystemsManager sysManager)
	: ICompanyStore
{
	private readonly ISystemsManager systemManager = sysManager;
	private readonly AppDbContext context = context;
	private readonly ILogger<CompanyStore> logger = logger;
	public async Task<Company?> FindByIdAsync (int companyId, CancellationToken cancellationToken = default)
		=> await context!.Companies!
			.Where(p => p.Id == companyId)
			.Include(p => p.Logo)
				.ThenInclude(p => p!.Image)
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<Company?> FindByIdAsync (int companyId, int userId, CancellationToken cancellationToken = default)
	{
		return await context.CompanyUsers!.Where(p => p.CompanyId == companyId && p.UserId == userId)
			.Include(p => p.Company)
			.ThenInclude(p => p!.Logo)
				.ThenInclude(p => p!.Image)
			.Select(p => p.Company)
			.FirstOrDefaultAsync(cancellationToken);
	}

	public async Task<IList<Company?>> FindByUserIdAsync (int userId, CancellationToken cancellationToken = default)
	{
		return await context.CompanyUsers!.Where(p => p.UserId == userId)
			.Include(p => p.Company)
			.ThenInclude(p => p!.Logo)
				.ThenInclude(p => p!.Image)
			.Select(p => p.Company)
			.ToListAsync(cancellationToken);
	}

	public async Task<CompanyRegionalSetup?> FindRegionalSettingsAsync (int companyId, CancellationToken cancellationToken = default)
		=> await context!.CompanyRegionalSettings!
			.FindAsync([companyId], cancellationToken);

	public async Task<CompanyImage?> FindLogoAsync (int companyId, CancellationToken cancellationToken = default)
		=> await context!.CompanyLogos!
			.Where(p => p.CompanyId == companyId)
			.Include(p => p.Image)
			.Select(p => p.Image)
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<IList<Tax>> FindTaxTypesAsync (int companyId, CancellationToken cancellationToken = default)
		=> await context!.CompanyTaxes!
			.Where(p => p.CompanyId == companyId)
			.Include(p => p.TaxType)
			.Select(p => p.TaxType!)
			.ToListAsync(cancellationToken);

	public async Task<Tax?> FindTaxByIdAsync (int companyId, int taxTypeId, CancellationToken cancellationToken = default)
		=> await context!.CompanyTaxes!
			.Where(p => p.CompanyId == companyId && p.TaxId == taxTypeId)
			.Include(p => p.TaxType)
			.Select(p => p.TaxType)
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<Tax?> FindDefaultTaxAsync (int companyId, CancellationToken cancellationToken = default)
		=> await context!.CompanyTaxes!
			.Where(p => p.CompanyId == companyId && p.Default)
			.Include(p => p.TaxType)
			.Select(p => p.TaxType)
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<CompanyMailSetup?> FindMailSettingsAsync (int companyId, CancellationToken cancellationToken = default)
		=> await context!.CompanyMailSettings!
			.FindAsync([companyId], cancellationToken);

	public async Task<BankAccount?> FindDefaultBankAccountAsync (int companyId, CancellationToken cancellationToken = default)
	 => await context!.CompanyDefaultBankAccounts!
			.Where(p => p.CompanyId == companyId)
			.Include(p => p.BankAccount)
			.Select(p => p.BankAccount)
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<BankAccount?> FindBankAccountByIdAsync (int companyId, int bankAccountId, CancellationToken cancellationToken = default)
		=> await context!.BankAccounts!
			.Where(p => p.CompanyId == companyId && bankAccountId == p.Id)
			.FirstOrDefaultAsync(cancellationToken);
	public async Task<IList<BankAccount>> FindBankAccountsAsync (int companyId, CancellationToken cancellationToken = default)
		=> await context!.BankAccounts!
			.Where(p => p.CompanyId == companyId)
			.ToListAsync(cancellationToken);

	public async Task<Contact?> FindSalesPersonByIdAsync (int companyId, int salesPersonId, CancellationToken cancellationToken = default)
		=> await context!.SalesPeople!
			.Where(p => p.CompanyId == companyId && p.ContactId == salesPersonId)
			.Include(p => p.Contact)
			.Select(p => p.Contact)
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<Contact?> FindSalesPersonByUserIdAsync (int companyId, int userId, CancellationToken cancellationToken = default)
		=> await context!.SalesPeople!
			.Where(p => p.CompanyId == companyId && p.CompanyUserId == userId)
			.Include(p => p.Contact)
			.Select(p => p.Contact)
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<IList<User>> FindUsersAsync (int companyId, CancellationToken cancellationToken = default)
		=> await context!.CompanyUsers!
			.Where(p => p.CompanyId == companyId)
			.Include(p => p.User)
			.Select(p => p.User!)
			.ToListAsync(cancellationToken);
}
