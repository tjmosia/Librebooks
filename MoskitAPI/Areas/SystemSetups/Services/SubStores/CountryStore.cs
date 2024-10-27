using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using Moskit.Core.EFCore;
using Moskit.CoreLib.Operations;
using Moskit.Data;
using Moskit.Extensions.Data;
using Moskit.Models.Entity.SystemSpace;

namespace Moskit.Areas.SystemSetups.Services.SubStores
{
    public class CountryStore (AppDbContext? context, ILogger? logger)
    {
        private readonly AppDbContext? context = context;
        private readonly ILogger? logger = logger;

        /// <exception cref="DbUpdateException"/>
        public async Task<TransactionResult<Country>> CreateAsync (Country country)
        {
            try
            {
                var result = await context!.Country.AddAsync(country);
                await context.SaveChangesAsync();
                return TransactionResult<Country>.Success(result.Entity);
            }
            catch (Exception ex)
            {
                logger!.LogError("Error occured with exception {ex} while attempting to add a country record.", ex.GetType().Name);
                logger!.LogError("{ex}", ex.StackTrace);

                if (ex.InnerException != null && ex.InnerException is SqlException)
                {
                    var sqlEx = ex.InnerException as SqlException;
                    logger!.LogError("{ex}", sqlEx!.StackTrace);

                    if (sqlEx.Number == DbEngineErrorsCodes.DuplicateKey)
                        return TransactionResult<Country>.Failure(DbErrorDescriber.DuplicateIndex("Code or Name"));
                }

                throw;
            }
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
