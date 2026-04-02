using Librebooks.Models.Entity.SystemSpace;

namespace Librebooks.Areas.Systems.Data;

public class CountryCountryData (Country country)
{
	public readonly int Id = country.Id;
	public readonly string Name = country.Name!;
	public readonly string Code = country.Code!;
	public readonly string? DialingCode = country.DialingCode;
}
