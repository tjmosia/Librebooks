using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Models.Entity.AccountingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SupplierSpace
{
    [Table(nameof(SupplierAdjustment))]
    public class SupplierAdjustment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public virtual int JournalId { get; set; }
        public virtual int CompanyId { get; set; }
        public virtual int SupplierId { get; set; }

        public virtual Journal? Journal { get; set; }
        public virtual Supplier? Supplier { get; set; }

        public static void OnModelCreating (ModelBuilder builder)
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

                options.HasOne<Company>()
                    .WithMany()
                    .HasForeignKey(p => p.CompanyId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
