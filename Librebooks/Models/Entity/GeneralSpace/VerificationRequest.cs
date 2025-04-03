using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Models.Entity.GeneralSpace
{
    public class VerificationRequest
    {
        public virtual string? Id { get; set; }
        public virtual string? Subject { get; set; }
        public virtual string? RequestUrl { get; set; }
        public virtual string? HashString { get; set; }
        public virtual Boolean Verified { get; set; }
        public virtual DateTime ValidUntil { get; set; }
        public virtual short Attempts { get; set; }

        [ConcurrencyCheck]
        public virtual string RowVersion { get; set; }

        public VerificationRequest ()
        {
            Id = Guid.NewGuid().ToString("N").ToUpper();
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
            Verified = false;
            Attempts = 0;
        }

        public VerificationRequest (string subject, string requestUrl)
            : this()
        {
            Subject = subject;
            RequestUrl = requestUrl;
        }

        public void RefreshConcurrencyToken ()
        {
            RowVersion = Guid.NewGuid().ToString("N").ToUpper();
        }

        public void Confirm ()
        {
            Verified = true;
            ValidUntil = DateTime.Now;
        }

        public bool HasExpired ()
            => DateTime.Now.CompareTo(ValidUntil) > 0 || Attempts == 3;

        public static void BuildModel (ModelBuilder builder)
        {
            builder.Entity<VerificationRequest>(options =>
            {
                options.ToTable(nameof(VerificationRequest))
                    .HasKey(p => p.Id)
                    .IsClustered();
            });
        }
    }
}
