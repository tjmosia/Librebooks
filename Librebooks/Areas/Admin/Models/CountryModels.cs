using System.ComponentModel.DataAnnotations;

using Librebooks.Models.Entity.SystemSpace;

namespace Librebooks.Areas.Admin.Models
{
    public abstract class CountryModelBase
    {
        [Required(ErrorMessage = "Country code is required.")]
        [StringLength(3, ErrorMessage = "Code must be 3 characters long.", MinimumLength = 3)]
        public string? Code { get; set; }
        [Required(ErrorMessage = "Country name is required.")]
        public string? Name { get; set; }
        public string? DialingCode { get; set; }
    }

    public class CountryModel : CountryModelBase
    {
        public Country MapToCountry (Country country)
        {
            country.Name = Name;
            country.Code = Code;
            country.DialingCode = DialingCode;

            return country;
        }
    }

    public class CountryUpdateModel : CountryModel
    {
        [Required(ErrorMessage = "Current country code is required.")]
        public string? CurrentCode { get; set; }
    }

    public class CountryDTO : CountryModelBase
    {
        public static CountryDTO MapFromCountry (Country country)
            => new CountryDTO
            {
                DialingCode = country.DialingCode,
                Name = country.Name,
                Code = country.Code
            };
    }
}
