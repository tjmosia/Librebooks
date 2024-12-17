using System.ComponentModel.DataAnnotations;

using LibreBooks.Models.Entity.CustomerSpace;

using Microsoft.EntityFrameworkCore;

namespace LibreBooksAPI.Models.Entity.CompanySpace
{
    public class CompanySetup
    {
        public virtual string? Id { get; set; }
        public virtual string? Prefix { get; set; }
        public virtual string? Suffix { get; set; }
        public virtual long NextNumber { get; set; }
        public virtual string? NumberFormat { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public CompanySetup ()
        {
            Id = Guid.NewGuid().ToString("N").ToUpper();
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<Customer>(options =>
            {
                options.ToTable(nameof(CompanySetup))
                    .HasKey(x => x.Id)
                    .IsClustered();
            });
    }
}
