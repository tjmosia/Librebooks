using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.SupplierSpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.PurchasesSpace
{
    [Table(nameof(PurchaseDocumentSupplierDetails))]
    public class PurchaseDocumentSupplierDetails () : VersionedEntityBase()
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public virtual DateTime CreatedAt { get; set; }

        public virtual int SupplierId { get; set; }

        [Required, MaxLength(155)]
        public virtual string? SupplierName { get; set; }

        [Required, MaxLength(155)]
        public virtual string? PhysicalAddress { get; set; }

        [MaxLength(155)]
        public virtual string? PostalAddress { get; set; }

        [MaxLength(10)]
        public virtual string? VATNumber { get; set; }

        public virtual bool Active { get; set; }

        public static void OnModelCreating (ModelBuilder builder)
            => builder.Entity<PurchaseDocumentSupplierDetails>(options =>
            {
                options.ToTable(nameof(PurchaseDocumentSupplierDetails))
                    .HasKey(p => p.Id)
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
