using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Models.Entity.CompanySpace
{
    public class CompanySalesTaxType
    {
        public virtual string? CompanyId { get; set; }
        public virtual string? CompanyTaxTypeId { get; set; }

        public virtual CompanyTaxType? CompanyTaxType { get; set; }
        public virtual Company? Company { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<CompanySalesTaxType>(options =>
            {
                options.ToTable(nameof(CompanySalesTaxType))
                    .HasKey(x => x.CompanyId)
                    .IsClustered();
            });
    }
}
