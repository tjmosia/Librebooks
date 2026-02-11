using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.PurchasesSpace;

[Table(nameof(PurchaseRequestForQuote))]
public class PurchaseRequestForQuote
{
    [Key]
    public virtual int DocumentId { get; set; }
    public virtual int CompanyId { get; set; }

    public virtual PurchaseDocument? Document { get; set; }

    public static void OnModelCreating (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PurchaseRequestForQuote>(options =>
        {
            options.ToTable(nameof(PurchaseRequestForQuote))
                .HasKey(p => p.DocumentId)
                .IsClustered(false);

            options.HasOne(p => p.Document)
                .WithOne()
                .HasForeignKey<PurchaseRequestForQuote>(p => p.DocumentId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}