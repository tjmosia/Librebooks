using System.ComponentModel.DataAnnotations;

namespace LibreBooks.Areas.Companies.Models
{
    public class CompanyInputModel
    {
        [Required]
        public virtual string? LegalName { get; set; }
        public virtual string? TradingName { get; set; }
        public virtual string? RegNumber { get; set; }
        public virtual string? ValueAddedTaxNumber { get; set; }
        public virtual string? BillingAddress { get; set; }
        public virtual string? DeliveryAddress { get; set; }
        public virtual string? Telephone { get; set; }
        public virtual string? EmailAddress { get; set; }
        public virtual string? FaxNumber { get; set; }
        public virtual string? Logo { get; set; }
    }
}
