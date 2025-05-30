﻿using System.ComponentModel.DataAnnotations;

namespace Librebooks.Areas.Companies.Models
{
    public class CompaniesResModels
    {
        public class CompanyDTO
        {
            [Required]
            public virtual string? LegalName { get; set; }
            public virtual string? TradingName { get; set; }
            public virtual string? RegNumber { get; set; }
            public virtual string? ValueAddedTaxNumber { get; set; }
            public virtual string? BillingAddress { get; set; }
            public virtual string? DeliveryAddress { get; set; }
            public virtual string? TelephoneNumber { get; set; }
            public virtual string? EmailAddress { get; set; }
            public virtual string? FaxNumber { get; set; }
            public virtual string? Logo { get; set; }
        }
    }
}
