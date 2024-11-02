using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using Moskit.Core.EFCore;
using Moskit.CoreLib.Operations;
using Moskit.Data;
using Moskit.Extensions.Data;
using Moskit.Models.Entity.SystemSpace;

namespace Moskit.Areas.SystemSetups.Services.SubStores
{
    public class ValueAddedTaxStore : DbStoreBase
    {
        public ValueAddedTaxStore (AppDbContext? context, ILogger? logger)
            : base(context, logger) { }

        /// <exception cref="DbUpdateException"/>
        public async Task<TransactionResult<ValueAddedTax>> CreateAsync (ValueAddedTax vat)
        {
            try
            {
                var result = await context!.ValueAddedTax.AddAsync(vat);
                await context.SaveChangesAsync();
                return TransactionResult<ValueAddedTax>.Success(result.Entity);
            }
            catch (Exception ex)
            {
                logger!.LogError("Error occured with exception {ex} while attempting to add a VAT record.", ex.GetType().Name);
                logger!.LogError("{ex}", ex.StackTrace);

                if (ex.InnerException != null && ex.InnerException is SqlException)
                {
                    var sqlEx = ex.InnerException as SqlException;
                    logger!.LogError("{ex}", sqlEx!.StackTrace);

                    if (sqlEx.Number == DbEngineErrorsCodes.PrimaryKeyConstraint)
                        return TransactionResult<ValueAddedTax>.Failure(DbErrorDescriber.PrimaryKeyConstraint("Code or Name"));
                }

                throw;
            }
        }

        /// <exception cref="DbUpdateException"/>
        public async Task<TransactionResult<ValueAddedTax>> UpdateAsync (ValueAddedTax vat)
        {
            var result = context!.ValueAddedTax.Update(vat);
            await context.SaveChangesAsync();
            return TransactionResult<ValueAddedTax>.Success(result.Entity);
        }

        public async Task<ValueAddedTax?> FindByIdAsync (string id)
            => await context!.ValueAddedTax.FindAsync(id);

        public async Task DeleteAsync (params ValueAddedTax[] vats)
        {
            context!.ValueAddedTax.RemoveRange(vats);
            await context.SaveChangesAsync();
        }

        public async Task<IList<ValueAddedTax>> FindAllAsync ()
            => await context!.ValueAddedTax.ToListAsync();
    }
}
