using Librebooks.Core.EFCore;
using Librebooks.Data;

using Librebooks.Models.Entity.CompanySpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Admin.Services.SubStores
{
    public class SystemCompanyNumberStore : DbStoreBase
    {
        public SystemCompanyNumberStore (AppDbContext context, ILogger<SystemCompanyNumberStore> logger)
            : base(context, logger) { }

        public async Task<CompanySetup?> GetCurrentAsync ()
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
