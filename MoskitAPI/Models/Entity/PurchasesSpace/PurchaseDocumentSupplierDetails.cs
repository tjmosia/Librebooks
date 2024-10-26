using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using Moskit.Models.Entity.SupplierSpace;

namespace Moskit.Models.Entity.PurchasesSpace
{
    public class PurchaseDocumentSupplierDetails
    {
        public virtual string? Id { get; set; }
        public virtual string? SupplierId { get; set; }
        public virtual string? Supplier { get; set; }
        public virtual string? BillingAddress { get; set; }
        public virtual string? PhysicalAddress { get; set; }
        public virtual string? VATNumber { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual bool Active { get; set; }

        [Timestamp, ConcurrencyCheck]
        public virtual byte[]? RowVersion { get; set; }

        public PurchaseDocumentSupplierDetails ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<PurchaseDocumentSupplierDetails>(options =>
            {
                options.ToTable(nameof(PurchaseDocumentSupplierDetails))
                    .HasKey(x => x.Id)
                    .IsClustered(false);

                options.HasIndex(p => p.SupplierId)
                    .IsClustered();

                options.HasOne<Supplier>()
                    .WithOne()
                    .HasForeignKey<PurchaseDocumentSupplierDetails>(p => p.SupplierId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasMany<PurchaseDocument>()
                .WithOne(p => p.SupplierDetails)
                .HasForeignKey(propa => propa.SupplierDetailsId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
