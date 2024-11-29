using Microsoft.EntityFrameworkCore;

using LibreBooks.Models.Entity.AccountingSpace;
using LibreBooks.Models.Entity.CompanySpace;

namespace LibreBooks.Models.Entity.CustomerSpace
{
    public class CustomerAdjustment
    {
        public virtual string? JournalId { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? CustomerId { get; set; }

        public virtual Company? Company { get; set; }
        public virtual Journal? Journal { get; set; }
        public virtual Customer? Customer { get; set; }

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<CustomerAdjustment>(options =>
            {
                options.ToTable(nameof(CustomerAdjustment))
                    .HasKey(p => p.JournalId)
                    .IsClustered(false);

                options.HasIndex(p => new { p.CompanyId, p.CustomerId, p.JournalId })
                    .IsClustered();

                options.HasOne(p => p.Journal)
                    .WithOne()
                    .HasForeignKey<CustomerAdjustment>(p => p.JournalId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
