using Microsoft.EntityFrameworkCore;

using Moskit.Models.Entity.GeneralSpace;

namespace Moskit.Models.Entity.SupplierSpace
{
    public class SupplierContact
    {
        public virtual string? Id { get; set; }
        public virtual string? SupplierId { get; set; }
        public virtual string? ContactId { get; set; }

        public virtual Supplier? Supplier { get; set; }
        public virtual Contact? Contact { get; set; }

        public SupplierContact ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<SupplierContact>(options =>
            {
                options.ToTable(nameof(SupplierContact))
                    .HasKey(p => p.Id)
                    .IsClustered(false);

                options.HasIndex(p => p.SupplierId)
                    .IsClustered();

                options.HasOne(p => p.Contact)
                    .WithOne()
                    .HasForeignKey<SupplierContact>(p => p.ContactId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });
    }
}