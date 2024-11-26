using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using MacbooksAPI.Models.Entity.CustomerSpace;

namespace MacbooksAPI.Models.Entity.SystemSpace
{
    public class SystemCompanyNumber
    {
        public virtual string? Id { get; set; }
        public virtual string? NumberPrefix { get; set; }
        public virtual long NumberNext { get; set; }
        public virtual string? NumberFormat { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public void UpdateConcurrencyToken ()
            => RowVersion = Guid.NewGuid().ToString("N");

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
