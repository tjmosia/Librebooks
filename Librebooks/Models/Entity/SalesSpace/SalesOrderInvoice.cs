using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SalesSpace;

[Table(nameof(SalesOrderInvoice))]
[PrimaryKey(nameof(OrderId), nameof(InvoiceId))]
public class SalesOrderInvoice
{
    public virtual int OrderId { get; set; }
    public virtual int InvoiceId { get; set; }


    public virtual SalesOrder? Order { get; set; }
    public virtual SalesInvoice? Invoice { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<SalesOrderInvoice>(options =>
        {

        });
    }
}
