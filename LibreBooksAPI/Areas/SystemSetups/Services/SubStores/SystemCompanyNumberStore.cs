using LibreBooks.Core.EFCore;
using LibreBooks.Data;
using LibreBooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Areas.SystemSetups.Services.SubStores
{
    public class SystemCompanyNumberStore : DbStoreBase
    {
        public SystemCompanyNumberStore (AppDbContext? context, ILogger<SystemCompanyNumberStore>? logger)
            : base(context, logger) { }

        public async Task<SystemCompanyNumber?> CurrentAsync ()
            => await context!.SystemCompanyNumber!.FirstOrDefaultAsync();

        public async Task<SystemCompanyNumber> CreateAsync (SystemCompanyNumber setup)
        {
            var result = await context!.SystemCompanyNumber!.AddAsync(setup);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<SystemCompanyNumber> UpdateAsync (SystemCompanyNumber setup)
        {
            var result = context!.SystemCompanyNumber!.Update(setup);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
