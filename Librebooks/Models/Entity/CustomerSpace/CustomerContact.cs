using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Models.Entity.GeneralSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CustomerSpace
{
    [Table(nameof(CustomerContact))]
    public class CustomerContact
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int ContactId { get; set; }
        public virtual int CustomerId { get; set; }

        public virtual Contact? Contact { get; set; }

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<CustomerContact>(options =>
            {
                options.HasIndex(p => new { p.CustomerId, p.ContactId })
                    .IsClustered();
            });
        }
    }
}
