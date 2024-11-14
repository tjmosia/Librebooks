using Microsoft.EntityFrameworkCore;

using OskitAPI.Core.EFCore;
using OskitAPI.Data;
using OskitAPI.Models.Entity.SystemSpace;

namespace OskitAPI.Areas.SystemSetups.Services.SubStores
{
    public class CountryStore : DbStoreBase
    {
        public CountryStore (AppDbContext? context, ILogger<CountryStore>? logger)
            : base(context, logger) { }

        /// <exception cref="DbUpdateException"/>
        public async Task<Country> CreateAsync (Country country)
        {
            var result = await context!.Country.AddAsync(country);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        /// <exception cref="DbUpdateException"/>
        public async Task<Country> UpdateAsync (Country country)
        {
            var result = context!.Country.Update(country);
            await context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Country?> FindByCodeAsync (string code)
            => await context!.Country.FindAsync(code);

        public async Task<Country?> FindByNameAsync (string name)
            => await context!.Country
                .Where(p => string.Equals(p.Name, name, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefaultAsync();

        public async Task DeleteAsync (params Country[] countries)
        {
            context!.Country.RemoveRange(countries);
            await context.SaveChangesAsync();
        }

        public async Task<IList<Country>> FindAllAsync ()
            => await context!.Country.ToListAsync();

    }
}
