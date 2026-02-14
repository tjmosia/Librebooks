using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.CustomerSpace;

[Table(nameof(CustomerAccountsContact))]
public class CustomerAccountsContact
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
	public virtual int CustomerContactId { get; set; }
	public virtual int CustomerId { get; set; }

	public virtual CustomerContact? CustomerContact { get; set; }
	public virtual Customer? Customer { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
		=> builder.Entity<CustomerAccountsContact>(options =>
		{
			options.HasIndex(p => new { p.CustomerId, p.CustomerContactId })
				.IsClustered();

			options.HasOne(p => p.CustomerContact)
				.WithOne()
				.HasForeignKey<CustomerAccountsContact>(p => p.CustomerContactId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		});
}
