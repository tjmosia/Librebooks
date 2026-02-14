using Librebooks.Areas.Admin.Services;
using Librebooks.Core.EFCore;
using Librebooks.CoreLib.Operations;
using Librebooks.Data;
using Librebooks.Models.Entity.BankingSpace;
using Librebooks.Models.Entity.CompanySpace;
using Librebooks.Models.Entity.CustomerSpace;
using Librebooks.Models.Entity.DocumentSpace;
using Librebooks.Models.Entity.GeneralSpace;
using Librebooks.Models.Entity.IdentitySpace;
using Librebooks.Models.Entity.InventorySpace;
using Librebooks.Models.Entity.SalesSpace;
using Librebooks.Models.Entity.SupplierSpace;
using Librebooks.Models.Entity.SystemSpace;

using Microsoft.EntityFrameworkCore;

namespace Librebooks.Areas.Companies.Services
{
    public class CompanyStore (AppDbContext context, ILogger logger, SystemManager sysManager)
        : DbStoreBase(context, logger), ICompanyStore
    {
        private readonly SystemManager systemManager = sysManager;

        /***********************************************************************************************************************************
         ****** SELECT TRANSACTIONS
         ***********************************************************************************************************************************/
        public async Task<Company?> FindByIdAsync (int companyId)
            => await context!.Company!
                .FindAsync(companyId);

        public async Task<Company?> FindByNumberAsync (string companyNumber)
            => await context!.Company!
                .Where(p => p.UniqueNumber == companyNumber)
                .FirstOrDefaultAsync();

        public async Task<CompanyRegionalSetup?> FindRegionalSettingsAsync (int companyId)
            => await context!.CompanyRegionalSettings!
                .FindAsync(companyId);

        public async Task<CompanyLogo?> FindLogoAsync (int companyId)
            => await context!.CompanyLogo!
                .Where(p => p.CompanyId == companyId)
                .Include(p => p.Image)
                .FirstOrDefaultAsync();

        public async Task<IList<TaxType>> FindTaxTypesAsync (int companyId)
            => await context!.CompanyTaxType!
                .Where(p => p.CompanyId == companyId)
                .Include(p => p.TaxType)
                .Select(p => p.TaxType!)
                .ToListAsync();

        public async Task<TaxType?> FindTaxTypeByIdAsync (int companyId, int taxTypeId)
            => await context!.CompanyTaxType!
                .Where(p => p.CompanyId == companyId && p.TaxTypeId == taxTypeId)
                .Include(p => p.TaxType)
                .Select(p => p.TaxType)
                .FirstOrDefaultAsync();

        public async Task<TaxType?> FindDefaultTaxTypeAsync (int companyId)
            => await context!.CompanyDefaultTaxType!
                .Where(p => p.CompanyId == companyId)
                .Include(p => p.CompanyTaxType)
                    .ThenInclude(p => p!.TaxType)
                .Select(p => p.CompanyTaxType!.TaxType)
                .FirstOrDefaultAsync();

        public async Task<CompanyMailSetup?> FindMailSettingsAsync (int companyId)
            => await context!.CompanyMailSettings!
                .FindAsync(companyId);

        public async Task<BankAccount?> FindDefaultBankAccountAsync (int companyId)
         => await context!.CompanyDefaultBankAccount!
                .Where(p => p.CompanyId == companyId)
                .Include(p => p.BankAccount)
                .Select(p => p.BankAccount)
                .FirstOrDefaultAsync();

        public async Task<BankAccount?> FindBankAccountByIdAsync (int companyId, int bankAccountId)
            => await context!.BankAccount!
                .Where(p => p.CompanyId == companyId && bankAccountId == p.Id)
                .FirstOrDefaultAsync();

        public async Task<IList<BankAccount>> FindBankAccountsAsync (int companyId)
            => await context!.BankAccount!
                .Where(p => p.CompanyId == companyId)
                .ToListAsync();

        public async Task<Contact?> FindSalesPersonByIdAsync (int companyId, int salesPersonId)
            => await context!.SalesPerson!
                .Where(p => p.CompanyId == companyId && p.ContactId == salesPersonId)
                .Include(p => p.Contact)
                .Select(p => p.Contact)
                .FirstOrDefaultAsync();

        public async Task<Contact?> FindSalesPersonByUserIdAsync (int companyId, int userId)
            => await context!.SalesPerson!
                .Where(p => p.CompanyId == companyId && p.CompanyUserId == userId)
                .Include(p => p.Contact)
                .Select(p => p.Contact)
                .FirstOrDefaultAsync();

        public async Task<IList<User>> FindUsersAsync (int companyId)
            => await context!.CompanyUser!
                .Where(p => p.CompanyId == companyId)
                .Include(p => p.User)
                .Select(p => p.User!)
                .ToListAsync();

        /***********************************************************************************************************************************
         ****** INSERT TRANSACTIONS
         ***********************************************************************************************************************************/

        public async Task<TransactionResult<Company>> CreateAsync (Company company)
        {
            try
            {
                var result = await context!.AddAsync(company);
                context.SaveChanges();
                return TransactionResult<Company>.Success(result.Entity);
            }
            catch (Exception ex)
            {
                logger!.LogError("***DB Error with Exception occurred while trying to create Company:*** \n\n{message}", ex.Message);
                return TransactionResult<Company>.Failure();
            }
        }

        public async Task<TransactionResult<TaxType>> CreateTaxTypeAsync (Company company, TaxType taxType)
        {
            try
            {
                var result = await context.TaxType!.AddAsync(taxType);
                await context.CompanyTaxType!.AddAsync(new CompanyTaxType(company.Id, taxType.Id));
                await context.SaveChangesAsync();

                return TransactionResult<TaxType>.Success(result.Entity);
            }
            catch (Exception ex)
            {
                logger!.LogError("***DB Error occured with Exception while creating Company TaxType:*** \n\n{message}", ex.Message);
                return TransactionResult<TaxType>.Failure();
            }
        }

        public async Task<TransactionResult<Contact>> CreateSalesPersonAsync (Company company, Contact contact)
        {
            try
            {
                var result = await context.Contact!.AddAsync(contact);

                await context.SalesPerson!.AddAsync(new SalesPerson
                {
                    CompanyId = company.Id,
                    ContactId = contact.Id,
                });

                await context.SaveChangesAsync();

                return TransactionResult<Contact>.Success(result.Entity);
            }
            catch (Exception ex)
            {
                logger!.LogError("***DB Error occured with Exception while creating Company TaxType:*** \n\n{message}", ex.Message);
                return TransactionResult<Contact>.Failure();
            }
        }

        public async Task<TransactionResult<BankAccount>> CreateBankAccountAsync (Company company, BankAccount bankAccount)
        {
            try
            {
                bankAccount.CompanyId = company.Id;
                var result = await context.BankAccount!.AddAsync(bankAccount);
                await context.SaveChangesAsync();
                return TransactionResult<BankAccount>.Success(result.Entity);
            }
            catch (Exception ex)
            {
                logger!.LogError("***DB Error occured with Exception while creating Company Bank Account:*** \n\n{message}", ex.Message);
                return TransactionResult<BankAccount>.Failure();
            }
        }

        public async Task<TransactionResult<BankAccount>> CreateDefaultBankAccountAsync (Company company, BankAccount bankAccount)
        {
            try
            {
                var result = await context.CompanyDefaultBankAccount!
                    .AddAsync(new CompanyDefaultBankAccount(company.Id, bankAccount.Id));
                await context.SaveChangesAsync();
                return TransactionResult<BankAccount>.Success(bankAccount);
            }
            catch (Exception ex)
            {
                logger!.LogError("***DB Error occured with Exception while creating Company Default Bank Account:*** \n\n{message}", ex.Message);
                return TransactionResult<BankAccount>.Failure();
            }
        }

        public async Task<TransactionResult<CompanyImage>> CreateLogoAsync (Company company, CompanyImage image)
        {
            try
            {
                var result = await context.CompanyImage!.AddAsync(image);
                await context.SaveChangesAsync();
                await UpdateLogoAsync(result.Entity);
                return TransactionResult<CompanyImage>.Success(result.Entity);
            }
            catch (Exception)
            {
                return TransactionResult<CompanyImage>.Failure();
            }
        }

        /***********************************************************************************************************************************
         ****** UPDATE TRANSACTIONS
         ***********************************************************************************************************************************/
        public async Task<TransactionResult<CompanyRegionalSetup>> UpdateRegionalSettingsAsync (CompanyRegionalSetup regionalSettings)
        {
            try
            {
                var result = context!.CompanyRegionalSettings!.Update(regionalSettings);
                await context.SaveChangesAsync();
                return TransactionResult<CompanyRegionalSetup>.Success(result.Entity);
            }
            catch (Exception ex)
            {
                logger!.LogError("***DB Error occured with Exception while trying to update CompanyRegionalSettings:*** \n\n{message}", ex.Message);
                return TransactionResult<CompanyRegionalSetup>.Failure();
            }
        }

        public async Task<TransactionResult<CompanyDefaultBankAccount>> UpdateDefaultTaxTypeAsync (CompanyDefaultTaxType defaultTaxType)
        {
            try
            {
                context!.CompanyDefaultTaxType!.Update(defaultTaxType);
                await context!.SaveChangesAsync();
                return TransactionResult<CompanyDefaultBankAccount>.Success();
            }
            catch (Exception ex)
            {
                logger!.LogError("***DB Error occurred with Exception while trying to update CompanyDefaultTaxType:*** \n\n{message}", ex.Message);
                return TransactionResult<CompanyDefaultBankAccount>.Failure();
            }
        }

        public async Task<TransactionResult<TaxType>> UpdateTaxTypeAsync (TaxType taxType)
        {
            try
            {
                var result = context!.TaxType!.Update(taxType);
                await context.SaveChangesAsync();
                return TransactionResult<TaxType>.Success(result.Entity);
            }
            catch (Exception ex)
            {
                logger!.LogError("***DB Error occurred with Exception while trying to update CompanyDefaultTaxType:*** \n\n{message}", ex.Message);
                return TransactionResult<TaxType>.Failure();
            }
        }

        public async Task<TransactionResult<CompanyImage>> UpdateLogoAsync (CompanyImage companyImage)
        {
            try
            {
                var result = await context!.CompanyImage!.AddAsync(companyImage);
                var companyLogo = await context.CompanyLogo!.FindAsync(companyImage.CompanyId!);

                if (companyLogo == null)
                {
                    await context!.CompanyLogo!.AddAsync(new CompanyLogo(companyImage.CompanyId!, companyImage.Id));
                }
                else
                {
                    companyLogo.ImageId = companyImage.CompanyId;
                    context!.CompanyLogo!.Update(companyLogo);
                }

                await context!.SaveChangesAsync();
                return TransactionResult<CompanyImage>.Success(result.Entity);
            }
            catch (Exception ex)
            {
                logger!.LogError("***DB Error occurred with Exception while trying to Update Company Logo:*** \n\n{message}", ex.Message);
                return TransactionResult<CompanyImage>.Failure();
            }
        }

        public async Task<TransactionResult> UpdateMailSettingsAsync (CompanyMailSetup companyMailSettings)
        {
            try
            {
                context!.CompanyMailSettings!.Update(companyMailSettings);
                await context!.SaveChangesAsync();
                return TransactionResult.Success;
            }
            catch (Exception)
            {
                return TransactionResult.Failure();
            }
        }

        public async Task<TransactionResult<SupplierSetup>> UpdateSupplierSetupAsync (SupplierSetup supplierSetup)
        {
            try
            {
                var result = context.SupplierSetup!.Update(supplierSetup);
                await context.SaveChangesAsync();
                return TransactionResult<SupplierSetup>.Success(result.Entity);
            }
            catch (Exception)
            {
                return TransactionResult<SupplierSetup>.Failure();
            }
        }

        public async Task<TransactionResult<CustomerSetup>> UpdateCustomerSetupAsync (CustomerSetup customerSetup)
        {
            try
            {
                var result = context.CustomerSetup!.Update(customerSetup);
                await context.SaveChangesAsync();
                return TransactionResult<CustomerSetup>.Success(result.Entity);
            }
            catch (Exception)
            {
                return TransactionResult<CustomerSetup>.Failure();
            }
        }

        public async Task<TransactionResult<ItemSetup>> UpdateItemSetupAsync (ItemSetup itemSetup)
        {
            try
            {
                var result = context.ItemSetup!.Update(itemSetup);
                await context.SaveChangesAsync();
                return TransactionResult<ItemSetup>.Success(result.Entity);
            }
            catch (Exception)
            {
                return TransactionResult<ItemSetup>.Failure();
            }
        }

        public async Task<TransactionResult<DocumentSetup>> UpdateDocumentSetupAsync (DocumentSetup documentSetup)
        {
            try
            {
                var result = context.DocumentSetup!.Update(documentSetup);
                await context.SaveChangesAsync();
                return TransactionResult<DocumentSetup>.Success(result.Entity);
            }
            catch (Exception)
            {
                return TransactionResult<DocumentSetup>.Failure();
            }
        }

        public async Task<TransactionResult<BankAccount>> UpdateBankAccountAsync (BankAccount bankAccount)
        {
            try
            {
                var result = context.BankAccount!.Update(bankAccount);
                await context.SaveChangesAsync();
                return TransactionResult<BankAccount>.Success(result.Entity);
            }
            catch (Exception)
            {
                return TransactionResult<BankAccount>.Failure();
            }

        }


        /***********************************************************************************************************************************
         ****** DELETE TRANSACTIONS
         ***********************************************************************************************************************************/
        public async Task<TransactionResult> DeleteSalesPersonAsync (SalesPerson salesPerson)
        {
            try
            {
                context.SalesPerson!.Remove(salesPerson);
                context.Contact!.Remove(salesPerson.Contact!);
                await context!.SaveChangesAsync();

                return TransactionResult.Success;
            }
            catch (Exception ex)
            {
                logger!.LogError("***DB Error occurred with Exception while trying to remove Sales Person:*** \n\n{message}", ex.Message);
                return TransactionResult.Failure();
            }
        }

        public async Task<TransactionResult> DeleteAsync (Company company)
        {

            try
            {
                context.Company!.Remove(company);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger!.LogError("***DB Error occurred with Exception while removing Company:*** \n\n{message}", ex.Message);
                return TransactionResult.Failure();
            }
            return TransactionResult.Success;
        }

        public async Task<TransactionResult> DeleteTaxTypeAsync (CompanyTaxType companyTaxType)
        {
            ArgumentNullException.ThrowIfNull(companyTaxType.TaxType, nameof(companyTaxType.TaxType));

            if (companyTaxType.TaxType!.System)
                return TransactionResult.Failure(TransactionError.Create("", "Cannot remove a system tax type."));

            try
            {
                context.CompanyTaxType!.Remove(companyTaxType);
                context.TaxType!.Remove(companyTaxType.TaxType);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger!.LogError("***DB Error occurred with Exception while trying to remove Sales Person:*** \n\n{message}", ex.Message);
                return TransactionResult.Failure();
            }

            return TransactionResult.Success;
        }

        public async Task<TransactionResult> DeleteBankAccountAsync (BankAccount bankAccount)
        {
            try
            {
                context.BankAccount!.Remove(bankAccount);
                await context.SaveChangesAsync();
                return TransactionResult.Success;
            }
            catch (Exception)
            {
                return TransactionResult.Failure();
            }
        }

        public async Task<TransactionResult> DeleteLogoAsync (CompanyLogo companyLogo)
        {
            try
            {
                context.CompanyLogo!.Remove(companyLogo);
                await context.SaveChangesAsync();
                return TransactionResult.Success;
            }
            catch (Exception)
            {
                return TransactionResult.Failure();
            }
        }
    }
}
