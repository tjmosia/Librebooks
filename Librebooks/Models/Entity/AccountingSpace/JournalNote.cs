using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Librebooks.Models.Entity.GeneralSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.AccountingSpace;

[Table(nameof(JournalNote))]
public class JournalNote
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public virtual int NoteId { get; set; }
    public virtual int JournalId { get; set; }

    public virtual Note? Note { get; set; }

    public static void OnModelCreating (ModelBuilder builder)
    {
        builder.Entity<JournalNote>(options =>
        {
            options.HasIndex(p => new { p.JournalId, p.NoteId })
                .IsClustered();
        });
    }
}
