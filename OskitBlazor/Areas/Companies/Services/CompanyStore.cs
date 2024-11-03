using Microsoft.EntityFrameworkCore;

using OskitBlazor.CoreLib.Operations;
using OskitBlazor.Data;
using OskitBlazor.Models.Entity.CompanySpace;
using OskitBlazor.Models.Entity.IdentitySpace;

namespace OskitBlazor.Areas.Companies.Services
{
    public class CompanyStore : IDisposable
    {
        private readonly AppDbContext context;
        private readonly ILogger<CompanyStore> logger;
        private bool disposed = false;

        public CompanyStore (AppDbContext context, ILogger<CompanyStore> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<Company?> FindByIdAsync (string? id)
            => await context.Company.FindAsync(id);

        public async Task<Company?> FindByNumberAsync (string number)
            => await context.Company
                .Where(p => p.Number == number)
                .FirstOrDefaultAsync();

        public TransactionResult Add (Company company, User user)
        {
            try
            {
                var result = context.Company.Add(company);
                context.CompanyUser.Add(new CompanyUser
                {
                    CompanyId = company.Id,
                    UserId = user.Id
                });

                context.SaveChanges();
                return TransactionResult.Success;
            }
            catch (Exception ex)
            {
                logger.LogError("Exception Occurred at {method} with stack trade: \n {trace}", "CompanyService.Add", ex.StackTrace);
                return TransactionResult.Failure([]);
            }
        }

        protected virtual void Dispose (bool disposing)
        {
            if (!disposed && disposing)
                disposed = true;
        }

        public void Dispose ()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
