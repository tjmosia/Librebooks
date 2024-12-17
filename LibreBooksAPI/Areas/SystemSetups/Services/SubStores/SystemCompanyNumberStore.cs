using LibreBooks.Core.EFCore;
using LibreBooks.Data;

using LibreBooksAPI.Models.Entity.CompanySpace;

using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Areas.SystemSetups.Services.SubStores
{
    public class SystemCompanyNumberStore : DbStoreBase
    {
        public SystemCompanyNumberStore (AppDbContext? context, ILogger<SystemCompanyNumberStore>? logger)
            : base(context, logger) { }

        public async Task<CompanySetup?> CurrentAsync ()
            => await context!.SystemCompanyNumber!.FirstOrDefaultAsync();

        public async Task<CompanySetup> CreateAsync (CompanySetup setup)
        {
            var result = await context!.SystemCompanyNumber!.AddAsync(setup);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<CompanySetup> UpdateAsync (CompanySetup setup)
        {
            var result = context!.SystemCompanyNumber!.Update(setup);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
