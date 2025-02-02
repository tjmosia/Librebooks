﻿using LibreBooks.Core.EFCore;
using LibreBooks.Data;
using LibreBooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace LibreBooks.Areas.SystemSetups.Services.SubStores
{
    public class CountryStore : DbStoreBase
    {
        public CountryStore (AppDbContext context, ILogger<CountryStore> logger)
            : base(context, logger) { }

        /// <exception cref="DbUpdateException"/>
        public async Task<Country> CreateAsync (Country country)
        {
            var result = await context!.Country!.AddAsync(country);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        /// <exception cref="DbUpdateException"/>
        public async Task<Country> UpdateAsync (Country country)
        {
            var result = context!.Country!.Update(country);
            await context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Country?> FindByCodeAsync (string code)
            => await context!.Country!.FindAsync(code);

        public async Task<Country?> FindByNameAsync (string name)
            => await context!.Country!
                .Where(p => string.Equals(p.Name, name, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefaultAsync();

        public async Task DeleteAsync (params Country[] countries)
        {
            context!.Country!.RemoveRange(countries);
            await context.SaveChangesAsync();
        }

        public async Task<IList<Country>> FindAllAsync ()
            => await context!.Country!.ToListAsync();

    }
}
