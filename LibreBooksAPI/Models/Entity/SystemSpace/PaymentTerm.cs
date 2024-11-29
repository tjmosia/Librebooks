using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Models.Entity.SystemSpace
{
    public class PaymentTerm
    {
        public virtual string? Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? ShortName { get; set; }
        public virtual string? Type { get; set; }
        public virtual string? Description { get; set; }

        [ConcurrencyCheck]
        public virtual string? RowVersion { get; set; }

        public void UpdateConcurrencyToken ()
            => RowVersion = Guid.NewGuid().ToString("N");

        public PaymentTerm ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<PaymentTerm>(options =>
            {
                options.ToTable(nameof(PaymentTerm))
                    .HasKey(x => x.Id)
                    .IsClustered();

                options.HasIndex(p => p.Name)
                    .IsUnique();
            });
    }
}
