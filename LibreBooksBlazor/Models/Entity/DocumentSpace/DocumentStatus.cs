using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using OskitBlazor.Models.Entity.PurchasesSpace;
using OskitBlazor.Models.Entity.SalesSpace;

namespace OskitBlazor.Models.Entity.DocumentSpace
{
    public class DocumentStatus
    {
        public virtual string? Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Type { get; set; }
        public virtual string? Color { get; set; }

        [Timestamp, ConcurrencyCheck]
        public virtual byte[]? RowVersion { get; set; }

        public DocumentStatus ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<DocumentStatus>(options =>
            {
                options.ToTable(nameof(DocumentStatus))
                    .HasKey(p => p.Id)
                    .IsClustered();

                options.HasMany<PurchaseDocument>()
                    .WithOne(p => p.Status)
                    .HasForeignKey(p => p.StatusId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasMany<SalesDocument>()
                    .WithOne(p => p.Status)
                    .HasForeignKey(p => p.StatusId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
