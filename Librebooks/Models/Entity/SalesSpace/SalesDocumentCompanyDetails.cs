using System.ComponentModel.DataAnnotations;

using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.CustomerSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SalesSpace
{
    public class SalesDocumentCompanyDetails
    {
        public virtual string Id { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? CompanyName { get; set; }
        public virtual string? PhysicalAddress { get; set; }
        public virtual string? PostalAddress { get; set; }
        public virtual string? VATNumber { get; set; }
        public virtual byte[]? Logo { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual bool Active { get; set; }
        [ConcurrencyCheck]
        public virtual string RowVersion { get; set; }

        public SalesDocumentCompanyDetails ()
        {
            Id = Guid.NewGuid().ToString("N").ToUpper();
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<SalesDocumentCompanyDetails>(options =>
            {
                options.ToTable(nameof(SalesDocumentCompanyDetails))
                    .HasKey(x => x.Id)
                    .IsClustered(false);

                options.HasIndex(p => p.CompanyId)
                    .IsClustered();

                options.HasOne<Company>()
                    .WithOne()
                    .HasForeignKey<SalesDocumentCompanyDetails>(p => p.CompanyId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany<SalesDocument>()
                    .WithOne(p => p.CompanyDetails)
                    .HasForeignKey(propa => propa.CompanyDetails)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasOne<Company>()
                    .WithOne()
                    .HasForeignKey<SalesDocumentCompanyDetails>(p => p.CompanyId)
                        .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

                options.HasMany<SalesReceipt>()
                    .WithOne(p => p.CompanyDetails)
                    .HasForeignKey(p => p.CustomerDetailsId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });
    }
}
}
