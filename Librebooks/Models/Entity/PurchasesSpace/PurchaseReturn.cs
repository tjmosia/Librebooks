﻿using Librebooks.Models.Entity.CompanySpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.PurchasesSpace
{
    public class PurchaseReturn
    {
        public virtual string? DocumentId { get; set; }
        public virtual string? CompanyId { get; set; }
        public virtual string? SupplierId { get; set; }

        public virtual Company? Company { get; set; }
        public virtual PurchaseDocument? Document { get; set; }
        public virtual ICollection<PurchaseInvoiceReturn>? Invoices { get; set; }

        public static void BuildModel (ModelBuilder builder)
            => builder.Entity<PurchaseReturn>(options =>
            {
                options.ToTable(nameof(PurchaseReturn))
                    .HasKey(x => x.DocumentId)
                    .IsClustered(false);

                options.HasIndex(p => new { p.CompanyId, p.SupplierId })
                    .IsUnique()
                    .IsClustered();

                options.HasOne(p => p.Document)
                    .WithOne()
                    .HasForeignKey<PurchaseReturn>(p => p.DocumentId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                options.HasMany(p => p.Invoices)
                    .WithOne(p => p.Return)
                    .HasForeignKey(p => p.ReturnId)
                        .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
