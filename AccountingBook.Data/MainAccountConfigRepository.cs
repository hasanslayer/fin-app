using AccountingBook.Core.Financial;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingBook.Data
{
    public class MainAccountConfigRepository
    {
        private readonly AppDbContext _db;

        public MainAccountConfigRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<MainAccountConfig>> GetMainAccountConfigList()
        {
            var mainAccountConfigList = await _db.MainAccountConfigs.Include(x => x.AccountConfigs).ThenInclude(ac => ac.FinancialDimension).ToListAsync();

            return mainAccountConfigList;
        }

        public async Task<MainAccountConfig> GetMainAccountConfigByPatternValue(string patternValue)
        {
            var mainAccountConfig = await _db.MainAccountConfigs.Include(x => x.AccountConfigs).ThenInclude(ac => ac.FinancialDimension).FirstOrDefaultAsync(ma => ma.PatternValue == patternValue);

            return mainAccountConfig;
        }

        public async Task<MainAccountConfig> GetMainAccountConfigById(Guid id)
        {
            var mainAccountConfig = await _db.MainAccountConfigs.Include(x => x.AccountConfigs).ThenInclude(ac => ac.FinancialDimension).FirstOrDefaultAsync(x => x.Id == id);

            return mainAccountConfig;
        }

        public async Task<MainAccountConfig> AddMainAccountConfig(MainAccountConfig mainAccountConfig)
        {
            var newMainAccountConfig = await _db.MainAccountConfigs.AddAsync(mainAccountConfig);
            await _db.SaveChangesAsync();
            return newMainAccountConfig.Entity;
        }

        public async Task<MainAccountConfig> AddAccountConfigToMainAccountConfig(Guid mainAccountConfigId, List<Guid> accountConfigIds)
        {
            List<AccountConfig> accountConfigList = new List<AccountConfig>();

            foreach (var accountConfigId in accountConfigIds)
            {
                var foundedAccountConfig = await _db.AccountConfigs.FindAsync(accountConfigId);
                if (foundedAccountConfig != null)
                {
                    foundedAccountConfig.MainAccountConfigId = mainAccountConfigId;
                    accountConfigList.Add(foundedAccountConfig);
                }
            }

            await _db.AccountConfigs.AddRangeAsync(accountConfigList);
            await _db.SaveChangesAsync();
            var mainAccountConfig = await _db.MainAccountConfigs.Include(ma => ma.AccountConfigs).ThenInclude(ac => ac.FinancialDimension).FirstOrDefaultAsync(ma => ma.Id == mainAccountConfigId);

            return mainAccountConfig;
        }

        public async Task<MainAccountConfig> RemoveAccountConfigFromMainAccountConfig(Guid mainAccountConfigId, List<Guid> accountConfigIds)
        {
            List<AccountConfig> accountConfigList = new List<AccountConfig>();

            foreach (var accountConfigId in accountConfigIds)
            {
                var foundedAccountConfig = await _db.AccountConfigs.FindAsync(accountConfigId);
                if (foundedAccountConfig != null)
                {
                    accountConfigList.Add(foundedAccountConfig);
                }
            }

            _db.AccountConfigs.RemoveRange(accountConfigList);
            await _db.SaveChangesAsync();
            var mainAccountConfig = await _db.MainAccountConfigs.Include(ma => ma.AccountConfigs).FirstOrDefaultAsync(ma => ma.Id == mainAccountConfigId);

            return mainAccountConfig;
        }

        public async Task<bool> DeleteMainAccountConfig(Guid id)
        {
            var accountConfig = await _db.MainAccountConfigs.FindAsync(id);
            _db.MainAccountConfigs.Remove(accountConfig);
            return _db.SaveChanges() > 0;
        }
    }
}
