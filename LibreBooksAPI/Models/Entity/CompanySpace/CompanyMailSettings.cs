using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Models.Entity.CompanySpace
{
    public class CompanyMailSettings
    {
        public virtual string? CompanyId { get; set; }
        public virtual string? EmailAddress { get; set; }
        public virtual string? Password { get; set; }
        public virtual string? SmtpServerName { get; set; }
        public virtual string? SmtpPort { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public CompanyMailSettings ()
            => RowVersion = Guid.NewGuid().ToString("N").ToUpper();

        public virtual Company? Company { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<CompanyMailSettings>(options =>
            {
                options.ToTable(nameof(CompanyMailSettings))
                    .HasKey(p => p.CompanyId);
            });
    }
}
