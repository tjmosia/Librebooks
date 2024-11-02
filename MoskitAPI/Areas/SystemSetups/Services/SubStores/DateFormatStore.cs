using Microsoft.EntityFrameworkCore;

using Moskit.Core.EFCore;
using Moskit.Data;
using Moskit.Models.Entity.SystemSpace;

namespace Moskit.Areas.SystemSetups.Services.SubStores
{
    public class DateFormatStore : DbStoreBase
    {
        public DateFormatStore (AppDbContext? context, ILogger? logger)
            : base(context, logger) { }

        public async Task<DateFormat> CreateAsync (DateFormat dateFormat)
        {
            var result = await context!.DateFormat.AddAsync(dateFormat);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<DateFormat> UpdateAsync (DateFormat dateFormat)
        {
            var result = context!.DateFormat.Update(dateFormat);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<DateFormat?> FindByIdAsync (string id)
            => await context!.DateFormat.FindAsync(id);

        public async Task DeleteAsync (params DateFormat[] dateFormats)
        {
            context!.DateFormat.RemoveRange(dateFormats);
            await context.SaveChangesAsync();
        }

        public async Task<IList<DateFormat>> FindAllAsync ()
            => await context!.DateFormat.ToListAsync();
    }
}
