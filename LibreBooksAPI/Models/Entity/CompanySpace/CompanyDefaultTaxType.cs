using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Models.Entity.CompanySpace
{
    public class CompanyDefaultTaxType
    {
        public virtual string? CompanyId { get; set; }
        public virtual string? CompanyTaxTypeId { get; set; }

        public virtual CompanyTaxType? CompanyTaxType { get; set; }
        public virtual Company? Company { get; set; }

        public CompanyDefaultTaxType () { }

        public CompanyDefaultTaxType (string companyId, string companyTaxTypeId)
        {
            CompanyId = companyId;
            CompanyTaxTypeId = companyTaxTypeId;
        }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<CompanyDefaultTaxType>(options =>
            {
                options.ToTable(nameof(CompanyDefaultTaxType))
                    .HasKey(x => x.CompanyId)
                    .IsClustered();
            });
    }
}
