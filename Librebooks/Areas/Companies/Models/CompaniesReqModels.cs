using System.ComponentModel.DataAnnotations;

namespace Librebooks.Areas.Companies.Models
{
    public class CompaniesReqModels
    {
        public class CreateModel
        {
            [Required(ErrorMessage = $"Registered name is required")]
            public virtual string? LegalName { get; set; }

            public virtual string? TradingName { get; set; }

            public virtual string? RegNumber { get; set; }

            public virtual string? VATNumber { get; set; }

            [Required(ErrorMessage = $"Physical address is required")]
            public virtual string? PhysicalAddress { get; set; }

            [Required(ErrorMessage = $"Postal address is required")]
            public virtual string? PostalAddress { get; set; }

            [Required(ErrorMessage = $"Telephone is required")]
            public virtual string? TelephoneNumber { get; set; }

            [Required(ErrorMessage = $"{nameof(Email)} is required")]
            public virtual string? Email { get; set; }

            public virtual string? FaxNumber { get; set; }

            public virtual string? YearsInBusiness { get; set; }

            [Required(ErrorMessage = $"{nameof(BusinessSector)} is required")]
            public virtual int BusinessSector { get; set; }

            public virtual string? Logo { get; set; }
        }
    }
}
