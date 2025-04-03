using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.InventorySpace
{
    public class ItemSetup
    {
        public virtual string? CompanyId { get; set; }
        public virtual string? Prefix { get; set; }
        public virtual string? Suffix { get; set; }
        public virtual short LeadingZeros { get; set; }
        public virtual long NextNumber { get; set; }
        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public ItemSetup ()
        {
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<ItemSetup>(options =>
            {
                options.ToTable(nameof(ItemSetup))
                    .HasKey(p => p.CompanyId)
                    .IsClustered(false);

            });
        }
    }
}
