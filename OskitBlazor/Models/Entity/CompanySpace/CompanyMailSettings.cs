using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace OskitBlazor.Models.Entity.CompanySpace
{
    public class CompanyMailSettings
    {
        public virtual string? CompanyId { get; set; }
        public virtual string? EmailAddress { get; set; }
        public virtual string? Password { get; set; }
        public virtual string? SmtpServerName { get; set; }
        public virtual string? SmtpPort { get; set; }

        [Timestamp, ConcurrencyCheck]
        public virtual byte[]? RowVersion { get; set; }

        public virtual Company? Company { get; set; }

        public static void BuildModel(ModelBuilder builder)
            => builder.Entity<CompanyMailSettings>(options =>
            {
                options.ToTable(nameof(CompanyMailSettings))
                    .HasKey(p => p.CompanyId);
            });
    }
}
