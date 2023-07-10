using AccountingBook.Core.Financial;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingBook.Data
{
    public class AccountConfigRepository
    {
        private readonly AppDbContext _db;

        public AccountConfigRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<AccountConfig>> GetAccountConfigList()
        {
            var accountConfig = await _db.AccountConfigs.Include(ac => ac.FinancialDimension).OrderBy(ac => ac.Sort).ToListAsync();

            return accountConfig;
        }
        public async Task<AccountConfig> GetAccountConfigById(Guid id)
        {
            var accountConfig = await _db.AccountConfigs.Include(ac => ac.FinancialDimension).FirstOrDefaultAsync(ac => ac.Id == id);

            return accountConfig;
        }

        public async Task<List<AccountConfig>> AddAccountConfig(AccountConfig account)
        {
            await _db.AccountConfigs.AddAsync(account);
            await _db.SaveChangesAsync();

            var accountConfig = await _db.AccountConfigs.Where(ac => ac.MainAccountConfigId == account.MainAccountConfigId).Include(ac => ac.FinancialDimension).OrderBy(ac => ac.Sort).ToListAsync();

            return accountConfig;
        }
        public async Task<List<AccountConfig>> UpdateAccountConfig(AccountConfig account)
        {
            _db.AccountConfigs.Update(account);
            await _db.SaveChangesAsync();

            var accountConfig = await _db.AccountConfigs.Include(ac => ac.FinancialDimension).OrderBy(ac => ac.Sort).ToListAsync();

            return accountConfig;
        }

        public async Task<bool> IsAccountExist(Guid financialDimensionId,Guid mainAccountConfigId)
        {
            bool isExist = await _db.AccountConfigs.AnyAsync(ac => ac.FinancialDimensionId == financialDimensionId && ac.MainAccountConfigId == mainAccountConfigId);

            return isExist;
        }

        public async Task<bool> DeleteAccountConfig(Guid id)
        {
            var accountConfig = await _db.AccountConfigs.FindAsync(id);
            _db.AccountConfigs.Remove(accountConfig);
            return _db.SaveChanges() > 0;


        }
    }
}
