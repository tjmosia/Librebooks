using System.ComponentModel.DataAnnotations;

namespace MacbooksAPI.Areas.Companies.Models
{
    public class NewCompanyModel
    {
        [Required(ErrorMessage = "Registered name is required.")]
        public string? RegName { get; set; }
        public string? TradingName { get; set; }
        [Required(ErrorMessage = "Year founded is required.")]
        public int YearRegistered { get; set; }
        [Required(ErrorMessage = "Physical address is required.")]
        public string? PhysicalAddress { get; set; }
        public string? PostalADdress { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }
    }
}
