using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;
namespace OskitBlazor.Models.Entity.BankingSpace
{
    public class BankAccountCategory
    {
        public virtual string? Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Type { get; set; }
        public virtual string? Description { get; set; }

        [Timestamp, ConcurrencyCheck]
        public virtual byte[]? RowVersion { get; set; }

        public BankAccountCategory ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<BankAccountCategory>(options =>
            {
                options.ToTable(nameof(BankAccountCategory))
                    .HasKey(p => p.Id)
                    .IsClustered();

                options.HasMany<BankAccount>()
                    .WithOne(p => p.Category)
                    .HasForeignKey(p => p.CategoryId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}