using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Models.Entity.SystemSpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.GeneralSpace;

[Table(nameof(PhysicalAddress))]
public class PhysicalAddress
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [MaxLength(75)]
    public virtual string? BuildingDetails { get; set; }

    [MaxLength(75)]
    public virtual string? StreetAddress1 { get; set; }

    [Required, MaxLength(75)]
    public virtual string? StreetAddress2 { get; set; }

    [Required, MaxLength(50)]
    public virtual string? Suburb { get; set; }

    [Required, MaxLength(50)]
    public virtual string? City { get; set; }

    [Required, MaxLength(50)]
    public virtual string? Province { get; set; }

    [MaxLength(10)]
    public virtual string? ZipCode { get; set; }
    public virtual int CountryId { get; set; }

    public virtual Country? Country { get; set; }


    public static void OnModelCreating (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PhysicalAddress>(options =>
        {
            options.HasOne(p => p.Country)
                .WithMany()
                .HasForeignKey(p => p.CountryId)
                    .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
