using Microsoft.EntityFrameworkCore;

using Moskit.Data;
using Moskit.Models.Entity.SystemSpace;

namespace Moskit.Areas.SystemSetups.Services.SubStores
{
    public class SystemCompanyNumberStore (AppDbContext? context)
    {
        private readonly AppDbContext? context = context;

        public async Task<SystemCompanyNumber?> CurrentAsync ()
            => await context!.SystemCompanyNumber.FirstOrDefaultAsync();

        public async Task<SystemCompanyNumber> UpdateAsync (SystemCompanyNumber setup)
        {
            var result = context!.SystemCompanyNumber.Update(setup);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<SystemCompanyNumber> CreateAsync (SystemCompanyNumber setup)
        {
            var result = await context!.SystemCompanyNumber.AddAsync(setup);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
