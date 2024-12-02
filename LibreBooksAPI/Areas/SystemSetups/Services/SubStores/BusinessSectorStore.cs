using LibreBooks.Areas.SystemSetups.Services.SubStores;
using LibreBooks.Core.EFCore;
using LibreBooks.Data;
using LibreBooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;
namespace LibreBooksAPI.Areas.SystemSetups.Services.SubStores
{
    public class BusinessSectorStore : DbStoreBase
    {
        public BusinessSectorStore (AppDbContext? context, ILogger<CountryStore>? logger)
            : base(context, logger) { }

        public async Task<IList<BusinessSector>> FindAllAsync ()
        {
            ThrowIfDisposed();
            return await context!.BusinessSector!.ToListAsync();
        }

        /// <exception cref="DbUpdateException" />
        /// <exception cref="DbUpdateConcurrencyException" />
        public async Task<BusinessSector?> FindByIdAsync (string id)
        {
            ThrowIfDisposed();
            ArgumentNullException.ThrowIfNullOrEmpty(id, nameof(id));

            return await context!.BusinessSector!.FindAsync(id);
        }

        /// <exception cref="DbUpdateException" />
        /// <exception cref="DbUpdateConcurrencyException" />
        public async Task<BusinessSector?> CreateAsync (BusinessSector sector)
        {
            ThrowIfDisposed();
            ArgumentNullException.ThrowIfNullOrEmpty(nameof(sector));

            var result = await context!.BusinessSector!.AddAsync(sector);
            await context.SaveChangesAsync();

            return result.Entity;
        }


        /// <exception cref="DbUpdateException" />
        /// <exception cref="DbUpdateConcurrencyException" />
        public async Task DeleteAsync (params BusinessSector[] sectors)
        {
            ThrowIfDisposed();
            ArgumentNullException.ThrowIfNullOrEmpty(nameof(sectors));

            context!.BusinessSector!.RemoveRange(sectors);
            await context.SaveChangesAsync();
        }

        public void ThrowIfDisposed ()
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
        }
    }
}
