using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using OskitAPI.Models.Entity.CustomerSpace;

namespace OskitAPI.Models.Entity.SystemSpace
{
    public class SystemCompanyNumber
    {
        public virtual string? Id { get; set; }
        public virtual string? NumberPrefix { get; set; }
        public virtual long NumberNext { get; set; }
        public virtual string? NumberFormat { get; set; }

        [Timestamp, ConcurrencyCheck]
        public virtual byte[]? RowVersion { get; set; }

        public SystemCompanyNumber ()
            => Id = Guid.NewGuid().ToString("N");

        public void Increment () => ++NumberNext;

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<Customer>(options =>
            {
                options.ToTable(nameof(SystemCompanyNumber))
                    .HasKey(x => x.Id)
                    .IsClustered();
            });
    }
}
