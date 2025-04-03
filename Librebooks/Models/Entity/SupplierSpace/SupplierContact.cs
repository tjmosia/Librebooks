using Librebooks.Models.Entity.GeneralSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SupplierSpace
{
    public class SupplierContact
    {
        public virtual string? ContactId { get; set; }
        public virtual string? SupplierId { get; set; }

        public virtual Contact? Contact { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<SupplierContact>(options =>
            {
                options.ToTable(nameof(SupplierContact))
                    .HasKey(p => p.ContactId)
                    .IsClustered(false);

                options.HasIndex(p => p.SupplierId)
                    .IsClustered();
            });
    }
}