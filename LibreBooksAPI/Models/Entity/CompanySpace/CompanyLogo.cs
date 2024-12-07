using Microsoft.EntityFrameworkCore;

namespace LibreBooksAPI.Models.Entity.CompanySpace
{
    public class CompanyLogo
    {
        public virtual string? Id { get; set; }
        public virtual byte[]? Logo { get; set; }
        public virtual DateTime? DateCreated { get; set; }
        public virtual string? CompanyId { get; set; }

        public string? GetLogoAsBase64 ()
            => Logo == null ? null : Convert.ToBase64String(Logo);

        ///<exception cref="ArgumentNullException" />
        public void SetLogo (string base64StringLogo)
            => Logo = Convert.FromBase64String(base64StringLogo);

        public CompanyLogo () => Id = Guid.NewGuid().ToString("N").ToUpperInvariant();

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<CompanyLogo>(options =>
            {
                options.ToTable(nameof(CompanyLogo))
                    .HasKey(p => p.Id)
                    .IsClustered();

                options.Property(p => p.Logo)
                    .IsRequired();
            });
    }
}
