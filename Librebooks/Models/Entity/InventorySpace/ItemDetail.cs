using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.InventorySpace;

[Table(nameof(ItemDetail))]
public class ItemDetail
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [Required, MaxLength(25)]
    public virtual string? Name { get; set; }

    [Required, MaxLength(50)]
    public virtual string? Value { get; set; }

    public virtual int ItemId { get; set; }
    public virtual Item? Item { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<ItemDetail>(options =>
        {
            options.HasOne(p => p.Item)
                .WithMany(p => p.Details)
                .HasForeignKey(p => p.ItemId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
