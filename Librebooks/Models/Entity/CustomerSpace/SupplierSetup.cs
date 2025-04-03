using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CustomerSpace
{
    public class CustomerSetup
    {
        public virtual string? CompanyId { get; set; }
        public virtual string? Prefix { get; set; }
        public virtual string? Suffix { get; set; }
        public virtual long NextNumber { get; set; }
        public virtual short LeadingZeros { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public CustomerSetup ()
        {
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<CustomerSetup>(options =>
            {
                options.ToTable(nameof(CustomerSetup))
                    .HasKey(p => p.CompanyId)
                    .IsClustered();
            });
    }
}
