using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using Moskit.Models.Entity.CompanySpace;

namespace Moskit.Models.Entity.SystemSpace
{
    public class RegionalSettings
    {
        public virtual string? CompanyId { get; set; }
        public virtual string? CountryCode { get; set; }
        public virtual string? CurrencyCode { get; set; }
        public virtual string? DecimalMark { get; set; }
        public virtual string? ThousandsSeperator { get; set; }
        public virtual string? DateFormatId { get; set; }
        public virtual int RoundToNearest { get; set; }

        [Timestamp, ConcurrencyCheck]
        public virtual byte[]? RowVersion { get; set; }

        public virtual DateFormat? DateFormat { get; set; }
        public virtual Country? Country { get; set; }
        public virtual Currency? Currency { get; set; }
        public virtual Company? Company { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<RegionalSettings>(options =>
            {
                options.ToTable(nameof(RegionalSettings))
                    .HasKey(x => x.CompanyId)
                    .IsClustered();

                options.HasOne(p => p.DateFormat)
                    .WithOne()
                    .HasForeignKey<RegionalSettings>(p => p.DateFormatId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasOne(p => p.Country)
                    .WithOne()
                    .HasForeignKey<RegionalSettings>(p => p.CountryCode)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasOne(p => p.Currency)
                    .WithOne()
                    .HasForeignKey<RegionalSettings>(p => p.CurrencyCode)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
