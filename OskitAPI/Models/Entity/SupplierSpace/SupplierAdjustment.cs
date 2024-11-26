using Microsoft.EntityFrameworkCore;

using MacbooksAPI.Models.Entity.AccountingSpace;
using MacbooksAPI.Models.Entity.CompanySpace;

namespace MacbooksAPI.Models.Entity.SupplierSpace
{
    public class SupplierAdjustment
    {
        public virtual string? JournalId { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? SupplierId { get; set; }

        public virtual Company? Company { get; set; }
        public virtual Journal? Journal { get; set; }
        public virtual Supplier? Supplier { get; set; }

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<SupplierAdjustment>(options =>
            {
                options.ToTable(nameof(SupplierAdjustment))
                    .HasKey(p => p.JournalId)
                    .IsClustered(false);

                options.HasIndex(p => new { p.CompanyId, p.JournalId })
                    .IsClustered();

                options.HasOne(p => p.Journal)
                    .WithOne()
                    .HasForeignKey<SupplierAdjustment>(p => p.JournalId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
