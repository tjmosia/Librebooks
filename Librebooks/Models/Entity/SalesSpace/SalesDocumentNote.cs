using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Models.Entity.GeneralSpace;
using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.SalesSpace;

[Table(nameof(SalesDocumentNote))]
public class SalesDocumentNote
{
    [Key]
    public virtual int NoteId { get; set; }
    public virtual int DocumentId { get; set; }

    public virtual Note? Note { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<SalesDocumentNote>(options =>
        {

        });
    }
}

