using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Models.Entity.CompanySpace
{
    public class CompanyLogo
    {
        public virtual string? CompanyId { get; set; }
        public virtual string? ImageId { get; set; }

        public virtual CompanyImage? Image { get; set; }

        public CompanyLogo () { }

        public CompanyLogo (string companyId, string imageId)
        {
            CompanyId = companyId;
            ImageId = imageId;
        }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<CompanyLogo>(options =>
            {
                options.ToTable(nameof(CompanyLogo))
                    .HasKey(p => p.CompanyId)
                    .IsClustered();
            });
    }
}
