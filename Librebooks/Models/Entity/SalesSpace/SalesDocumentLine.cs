using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Core.Types;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SalesSpace;

[Table(nameof(SalesDocumentLine))]
public class SalesDocumentLine
{
    [Key]
    public virtual int LineId { get; set; }
    public virtual int DocumentId { get; set; }

    [Column(TypeName = ColumnTypes.NUMBER)]
    public virtual decimal Quantity { get; set; }

    public virtual SalesLine? Line { get; set; }
    public virtual SalesDocument? Document { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<SalesDocumentLine>(options =>
        {

        });
    }
}
