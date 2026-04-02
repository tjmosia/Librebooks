using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Core.Constants;
using Librebooks.Extensions.Models;
using Librebooks.Models.Entity.CompanySpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.AccountingSpace;

[Table(nameof(Account))]
public class Account () : VersionedEntityBase()
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public virtual int Id { get; set; }

	[Required, MaxLength(75)]
	public virtual string? Name { get; set; }

	[Column(TypeName = ColumnTypes.MONETARY)]
	public virtual decimal Balance { get; set; }

	public virtual int TaxId { get; set; }

	[MaxLength(255), Required]
	public virtual string? Description { get; set; }
	public virtual int? ParentAccountId { get; set; }
	public virtual bool Active { get; set; }
	public virtual bool System { get; set; }
	public virtual int? CompanyId { get; set; }
	public virtual int CategoryId { get; set; }

	public virtual Account? ParentAccount { get; set; }
	public virtual AccountCategory? Category { get; set; }
	public virtual CompanyTax? Tax { get; set; }

	public virtual ICollection<Account>? SubAccounts { get; set; }

	public static void OnModelCreating (ModelBuilder builder)
		=> builder.Entity<Account>(options =>
		{
			options.HasIndex(p => new { p.CompanyId, p.CategoryId })
				.IsClustered();

			options.HasMany(p => p.SubAccounts)
				.WithOne(p => p.ParentAccount)
				.HasForeignKey(p => p.ParentAccountId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne(p => p.Category)
				.WithMany()
				.HasForeignKey(p => p.CategoryId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne<Company>()
				.WithMany(p => p.ChartOfAccounts)
				.HasForeignKey(p => p.CompanyId)
					.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			options.HasOne(p => p.Tax)
			   .WithMany()
			   .HasForeignKey(p => p.TaxId)
				   .IsRequired()
			   .OnDelete(DeleteBehavior.Restrict);
		});
}
