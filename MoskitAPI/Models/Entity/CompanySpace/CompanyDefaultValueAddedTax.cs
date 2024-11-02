using Microsoft.EntityFrameworkCore;

namespace Moskit.Models.Entity.CompanySpace
{
    public class CompanyDefaultValueAddedTax
    {
        public virtual string? CompanyId { get; set; }
        public virtual string? CompanyVATId { get; set; }

        public virtual CompanyValueAddedTax? CompanyVAT { get; set; }
        public virtual Company? Company { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<CompanyDefaultValueAddedTax>(options =>
            {
                options.ToTable(nameof(CompanyDefaultValueAddedTax))
                    .HasKey(x => x.CompanyId)
                    .IsClustered();

                options.HasOne(p => p.Company)
                .WithOne(p => p.DefaultVAT)
                .HasForeignKey<CompanyDefaultValueAddedTax>(p => p.CompanyId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                options.HasOne(p => p.CompanyVAT)
                .WithOne(p => p.CompanyDefaultVAT)
                .HasForeignKey<CompanyDefaultValueAddedTax>(p => p.CompanyVATId)
                    .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
