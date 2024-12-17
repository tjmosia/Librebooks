using LibreBooks.Models.Entity.GeneralSpace;

using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Models.Entity.CustomerSpace
{
    public class CustomerContact
    {
        public virtual string? ContactId { get; set; }
        public virtual string? CustomerId { get; set; }

        public virtual Contact? Contact { get; set; }

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<CustomerContact>(options =>
            {
                options.ToTable(nameof(CustomerContact))
                    .HasKey(p => p.ContactId)
                    .IsClustered(false);

                options.HasIndex(p => p.CustomerId)
                    .IsClustered();
            });
        }
    }
}
