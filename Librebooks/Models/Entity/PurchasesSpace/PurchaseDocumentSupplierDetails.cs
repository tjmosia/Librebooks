using System.ComponentModel.DataAnnotations;

using Librebooks.Models.Entity.SupplierSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.PurchasesSpace
{
    public class PurchaseDocumentSupplierDetails
    {
        public virtual string Id { get; set; }
        public virtual string? SupplierId { get; set; }
        public virtual string? SupplierName { get; set; }
        public virtual string? BillingAddress { get; set; }
        public virtual string? PhysicalAddress { get; set; }
        public virtual string? VATNumber { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual bool Active { get; set; }
        [ConcurrencyCheck]
        public virtual string RowVersion { get; set; }

        public virtual Supplier? Supplier { get; set; }

        public PurchaseDocumentSupplierDetails ()
        {
            Id = Guid.NewGuid().ToString("N").ToUpper();
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<PurchaseDocumentSupplierDetails>(options =>
            {
                options.ToTable(nameof(PurchaseDocumentSupplierDetails))
                    .HasKey(p => p.Id)
                    .IsClustered(false);

                options.HasIndex(p => p.SupplierId)
                    .IsClustered();

                options.HasOne(p => p.Supplier)
                    .WithOne()
                    .HasForeignKey<PurchaseDocumentSupplierDetails>(p => p.SupplierId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasMany<PurchaseDocument>()
                .WithOne(p => p.SupplierDetails)
                .HasForeignKey(propa => propa.SupplierDetailsId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                options.HasMany<PurchaseReceipt>()
                    .WithOne(p => p.SupplierDetails)
                    .HasForeignKey(p => p.SupplierDetailsId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
