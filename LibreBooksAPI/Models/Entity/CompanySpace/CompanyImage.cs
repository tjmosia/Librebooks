using System.ComponentModel.DataAnnotations;

using LibreBooks.Models.Entity.PurchasesSpace;
using LibreBooks.Models.Entity.SalesSpace;

using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Models.Entity.CompanySpace
{
    public class CompanyImage
    {
        public virtual string Id { get; set; }
        public virtual byte[]? Data { get; set; }
        public virtual string? DateCreated { get; set; }
        public virtual string? CompanyId { get; set; }

        [ConcurrencyCheck]
        public virtual string RowVersion { get; set; }

        public CompanyImage ()
        {
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
            Id = Guid.NewGuid().ToString("N").ToUpper();
        }

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<CompanyImage>(options =>
            {
                options.ToTable(nameof(CompanyImage))
                    .HasKey(p => p.Id)
                    .IsClustered();

                options.HasOne<CompanyLogo>()
                    .WithOne(p => p.Image)
                    .HasForeignKey<CompanyLogo>(p => p.ImageId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany<SalesDocument>()
                    .WithOne(p => p.Logo)
                    .HasForeignKey(p => p.LogoId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany<SalesReceipt>()
                    .WithOne(p => p.Logo)
                    .HasForeignKey(p => p.LogoId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany<PurchaseDocument>()
                    .WithOne(p => p.Logo)
                    .HasForeignKey(p => p.LogoId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany<PurchaseReceipt>()
                    .WithOne(p => p.Logo)
                    .HasForeignKey(p => p.LogoId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
