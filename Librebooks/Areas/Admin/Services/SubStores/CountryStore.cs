using Librebooks.Core.EFCore;
using Librebooks.Data;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Admin.Services.SubStores
{
    public class CountryStore : DbStoreBase
    {
        public CountryStore (AppDbContext context, ILogger<CountryStore> logger)
            : base(context, logger) { }

        /// <exception cref="DbUpdateException"/>
        public async Task<Country?> CreateAsync (Country country)
        {
            try
            {
                var result = await context!.Country!.AddAsync(country);
                await context.SaveChangesAsync();
                return result.Entity;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }

        /// <exception cref="DbUpdateException"/>
        public async Task<Country> UpdateAsync (Country country)
        {
            country.RowVersion = GenerateRowVersion();
            var result = context!.Country!.Update(country);
            await context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Country?> FindByCodeAsync (string code)
            => await context!.Country!.FindAsync(code);

        public async Task<IList<Country>> FindByCodesAsync (params string[] codes)
            => await context!.Country!.Where(p => codes.Contains(p.Code))
                .ToListAsync();

        public async Task<Country?> FindByNameAsync (string name)
            => await context!.Country!
                .Where(p => string.Equals(p.Name, name, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefaultAsync();

        public async Task<bool> DeleteAsync (params Country[] countries)
        {
            try
            {
                context!.Country!.RemoveRange(countries);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IList<Country>> FindAllAsync ()
            => await context!.Country!.ToListAsync();

    }
}
