using System.ComponentModel.DataAnnotations;

using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CompanySpace
{
    public class CompanyRegionalSettings
    {
        public virtual string? CompanyId { get; set; }
        public virtual string? CountryCode { get; set; }
        public virtual string? CurrencyCode { get; set; }
        public virtual string? DecimalMark { get; set; }
        public virtual string? ThousandsSeperator { get; set; }
        public virtual string? DateFormatId { get; set; }
        public virtual int RoundToNearest { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public CompanyRegionalSettings ()
            => RowVersion = Guid.NewGuid().ToString("N").ToUpper();

        public virtual DateFormat? DateFormat { get; set; }
        public virtual Country? Country { get; set; }
        public virtual Currency? Currency { get; set; }
        public virtual Company? Company { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<CompanyRegionalSettings>(options =>
            {
                options.ToTable(nameof(CompanyRegionalSettings))
                    .HasKey(x => x.CompanyId)
                    .IsClustered();

                options.HasOne(p => p.DateFormat)
                    .WithOne()
                    .HasForeignKey<CompanyRegionalSettings>(p => p.DateFormatId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasOne(p => p.Country)
                    .WithOne()
                    .HasForeignKey<CompanyRegionalSettings>(p => p.CountryCode)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasOne(p => p.Currency)
                    .WithOne()
                    .HasForeignKey<CompanyRegionalSettings>(p => p.CurrencyCode)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
