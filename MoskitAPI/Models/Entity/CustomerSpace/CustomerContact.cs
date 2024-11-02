using Microsoft.EntityFrameworkCore;

using OskitAPI.Models.Entity.GeneralSpace;

namespace OskitAPI.Models.Entity.CustomerSpace
{
    public class CustomerContact
    {
        public virtual string? Id { get; set; }
        public virtual string? CustomerId { get; set; }
        public virtual string? ContactId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Contact? Contact { get; set; }

        public CustomerContact ()
            => Id = Guid.NewGuid().ToString("N");

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<CustomerContact>(options =>
            {
                options.ToTable(nameof(CustomerContact))
                    .HasKey(p => p.Id)
                    .IsClustered(false);

                options.HasIndex(p => new { p.CustomerId })
                    .IsUnique()
                    .IsClustered();
            });
        }
    }
}
