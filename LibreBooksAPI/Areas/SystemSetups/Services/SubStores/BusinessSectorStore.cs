﻿using LibreBooks.Core.EFCore;
using LibreBooks.Data;
using LibreBooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;
namespace LibreBooks.Areas.SystemSetups.Services.SubStores
{
    public class BusinessSectorStore : DbStoreBase
    {
        public BusinessSectorStore (AppDbContext context, ILogger<CountryStore> logger)
            : base(context, logger) { }

        public async Task<IList<BusinessSector>> FindAllAsync ()
        => await context!
                .BusinessSector!
                .OrderBy(p => p.Name)
                .ToListAsync();

        public async Task<BusinessSector?> FindByIdAsync (string id)
        => await context!
                .BusinessSector!
                .FindAsync(id);

        public async Task<BusinessSector?> FindByNameAsync (string name)
        => await context.BusinessSector!.Where(p => p.Name == name)
            .FirstOrDefaultAsync();

        public async Task<BusinessSector?> CreateAsync (BusinessSector sector)
        {
            try
            {
                var result = await context!.BusinessSector!.AddAsync(sector);
                await context.SaveChangesAsync();

                return result.Entity;
            }
            catch (Exception) { }

            return null;
        }

        public async Task<BusinessSector?> UpdateAsync (BusinessSector sector)
        {
            try
            {
                var result = context!.BusinessSector!.Update(sector);
                await context.SaveChangesAsync();

                return result.Entity;
            }
            catch (Exception) { }

            return null;
        }

        public async Task<bool> DeleteAsync (params BusinessSector[] sectors)
        {
            try
            {
                context!.BusinessSector!.RemoveRange(sectors);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}