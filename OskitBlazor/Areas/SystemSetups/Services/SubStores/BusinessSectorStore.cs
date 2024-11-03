using Microsoft.EntityFrameworkCore;

using OskitBlazor.Core.EFCore;
using OskitBlazor.Data;
using OskitBlazor.Models.Entity.SystemSpace;

namespace OskitBlazor.Areas.SystemSetups.Services.SubStores
{
    public class BusinessSectorStore : DbStoreBase
    {
        public BusinessSectorStore (AppDbContext? context, ILogger? logger)
            : base(context, logger) { }

        /// <exception cref="DbUpdateException"/>
        public async Task<BusinessSector> CreateAsync (BusinessSector businessSector)
        {
            var result = await context!.BusinessSector.AddAsync(businessSector);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        /// <exception cref="DbUpdateException"/>
        public async Task<BusinessSector> UpdateAsync (BusinessSector businessSector)
        {
            var result = context!.BusinessSector.Update(businessSector);
            await context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<BusinessSector?> FindByIdAsync (string id)
            => await context!.BusinessSector.FindAsync(id);

        public async Task<BusinessSector?> FindByNameAsync (string name)
            => await context!.BusinessSector
                .Where(p => string.Equals(p.Name, name, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefaultAsync();

        public async Task DeleteAsync (params BusinessSector[] businessSectors)
        {
            context!.BusinessSector.RemoveRange(businessSectors);
            await context.SaveChangesAsync();
        }

        public async Task<IList<BusinessSector>> FindAllAsync ()
            => await context!.BusinessSector.ToListAsync();

    }
}
