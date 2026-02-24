using Librebooks.Models.Entity.CompanySpace;

namespace Librebooks.Areas.Companies.Data;

public readonly struct CompanyDto (Company company)
{
	public readonly int CompanyId = company.Id;
	public readonly string Name = company.LegalName!;
	public readonly string? Logo = company.Logo?.Image?.DataAsBase64;
}
