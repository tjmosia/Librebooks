using Librebooks.Models.Entity.CompanySpace;

namespace Librebooks.Areas.Companies.Data;

public readonly struct CompanySummaryDto (Company company)
{
	public readonly int CompanyId = company.Id;
	public readonly string Name = company.LegalName!;
	public readonly string? Logo = company.Logo?.Image?.DataAsBase64;
}
