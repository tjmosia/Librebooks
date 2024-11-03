using Microsoft.EntityFrameworkCore;

namespace OskitAPI.Models.Entity.SupplierSpace
{
    public class SupplierAccountsContact
    {
        public virtual string? SupplierId { get; set; }
        public virtual string? SupplierContactId { get; set; }

        public virtual SupplierContact? SupplierContact { get; set; }
        public virtual Supplier? Supplier { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<SupplierAccountsContact>(options =>
            {
                options.ToTable(nameof(SupplierAccountsContact))
                    .HasKey(p => new { p.SupplierId, p.SupplierContactId })
                    .IsClustered();

                options.HasOne(p => p.SupplierContact)
                    .WithOne()
                    .HasForeignKey<SupplierAccountsContact>(p => p.SupplierContactId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

    }
}