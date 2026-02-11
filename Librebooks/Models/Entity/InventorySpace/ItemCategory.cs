using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Models.Entity.CompanySpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.InventorySpace;

[Table(nameof(ItemCategory))]
public class ItemCategory
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [Required, MaxLength(100)]
    public virtual string? Name { get; set; }

    [Required, MaxLength(155)]
    public virtual string? Description { get; set; }
    public virtual int CompanyId { get; set; }
    public virtual int? ParentId { get; set; }

    public virtual ItemCategory? Parent { get; set; }
    public virtual Company? Company { get; set; }
    public virtual ICollection<Item>? Items { get; set; }
    public virtual ICollection<ItemCategory>? SubCategories { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
        => builder.Entity<ItemCategory>(options =>
        {
            options.ToTable(nameof(ItemCategory))
                .HasKey(p => p.Id)
                .IsClustered(false);

            options.HasIndex(p => p.CompanyId)
                .IsClustered();

            options.HasMany(p => p.SubCategories)
                .WithOne(p => p.Parent)
                .HasForeignKey(p => p.ParentId)
                    .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            options.HasMany(p => p.Items)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                    .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            options.HasOne(p => p.Company)
                .WithMany()
                .HasForeignKey(p => p.CompanyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
}
