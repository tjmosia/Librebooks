using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using MacbooksAPI.Models.Entity.PurchasesSpace;
using MacbooksAPI.Models.Entity.SalesSpace;

namespace MacbooksAPI.Models.Entity.DocumentSpace
{
    public class DocumentStatus
    {
        public virtual string? Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Type { get; set; }
        public virtual string? Color { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public void UpdateConcurrencyToken ()
            => RowVersion = Guid.NewGuid().ToString("N");

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
