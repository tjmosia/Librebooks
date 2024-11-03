using Microsoft.EntityFrameworkCore;

namespace OskitBlazor.Models.Entity.CustomerSpace
{
    public class CustomerAccountsContact
    {
        public virtual string? CustomerId { get; set; }
        public virtual string? CustomerContactId { get; set; }

        public virtual CustomerContact? CustomerContact { get; set; }
        public virtual Customer? Customer { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<CustomerAccountsContact>(options =>
            {
                options.ToTable(nameof(CustomerAccountsContact))
                    .HasKey(p => new { p.CustomerId, p.CustomerContactId })
                    .IsClustered();

                options.HasOne(p => p.CustomerContact)
                    .WithOne()
                    .HasForeignKey<CustomerAccountsContact>(p => p.CustomerContactId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });
    }
}
