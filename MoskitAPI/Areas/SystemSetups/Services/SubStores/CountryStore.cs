using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using Moskit.Core.EFCore;
using Moskit.CoreLib.Operations;
using Moskit.Data;
using Moskit.Extensions.Data;
using Moskit.Models.Entity.SystemSpace;

namespace Moskit.Areas.SystemSetups.Services.SubStores
{
    public class CountryStore : DbStoreBase
    {
        public CountryStore (AppDbContext? context, ILogger? logger)
            : base(context, logger) { }

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
                logger!.LogError("{ex}", ex.Message);

                if (ex.Message.Contains("Country.Name"))
                    logger!.LogInformation("Country name");

                if (ex.InnerException != null && ex.InnerException is SqlException)
                {
                    var sqlEx = ex.InnerException as SqlException;
                    logger!.LogError("{ex}", sqlEx!.Errors[0]);

                    if (sqlEx!.ErrorCode == DbEngineErrorsCodes.IndexConstraint)
                        return TransactionResult<Country>.Failure(DbErrorDescriber.IndexConstraint("Code"));

                    if (sqlEx.ErrorCode == DbEngineErrorsCodes.PrimaryKeyConstraint)
                        return TransactionResult<Country>.Failure(DbErrorDescriber.PrimaryKeyConstraint("Code or Name"));
                }

                return TransactionResult<Country>.Failure();
            }
        }

        /// <exception cref="DbUpdateException"/>
        public async Task<Country> UpdateAync (Country country)
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
