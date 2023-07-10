using AccountingBook.Core.Financial;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingBook.Data
{
    public class FinancialDimensionValueRepository
    {
        private readonly AppDbContext _db;
        public FinancialDimensionValueRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<FinancialDimensionValue>> GetFinancialDimensionValues()
        {
            var financialDimesions = await _db.FinancialDimensionValues.Include(x => x.DimensionType).ToListAsync();

            return financialDimesions;
        }

        public async Task<FinancialDimensionValue> AddFinancialDimensionValue(FinancialDimensionValue financialDimensionValue)
        {
            await _db.FinancialDimensionValues.AddAsync(financialDimensionValue);
            await _db.SaveChangesAsync();
            _db.Entry(financialDimensionValue).Reference(fd => fd.DimensionType).Load();
            return financialDimensionValue;
        }

        public async Task<FinancialDimensionValue> GetFinancialDimensionValueById(Guid Id)
        {

            FinancialDimensionValue financialDimensionValue = await _db.FinancialDimensionValues.FirstOrDefaultAsync(f => f.Id == Id);
            return financialDimensionValue;

        }
        public async Task<List<FinancialDimensionValue>> GetFinancialDimensionValuesByDimensionId(Guid dimensionId)
        {

            List<FinancialDimensionValue> financialDimensionValues = await _db.FinancialDimensionValues.Where(f => f.DimensionTypeId == dimensionId).Include(dv => dv.DimensionType).ToListAsync();
            return financialDimensionValues;

        }
        public async Task<FinancialDimensionValue> GetFinancialDimensionValueByDimensionIdAndValue(Guid dimensionId, string value, Guid dimensionValueId)
        {
            FinancialDimensionValue financialDimensionValue = null;
            if (dimensionValueId != null || dimensionValueId != Guid.Empty)
            {
                financialDimensionValue = await _db.FinancialDimensionValues.FirstOrDefaultAsync(f => f.DimensionTypeId == dimensionId && f.Value == value && f.Id != dimensionValueId);
                return financialDimensionValue;
            }


            financialDimensionValue = await _db.FinancialDimensionValues.FirstOrDefaultAsync(f => f.DimensionTypeId == dimensionId && f.Value == value);
            return financialDimensionValue;

        }

        public FinancialDimensionValue UpdateFinancialDimensionValue(FinancialDimensionValue financialDimensionValue)
        {
            var updatedFinancialDimensionValue = _db.FinancialDimensionValues.Update(financialDimensionValue);
            _db.SaveChanges();
            _db.Entry(financialDimensionValue).Reference(fd => fd.DimensionType).Load();
            return updatedFinancialDimensionValue.Entity;
        }

        public async Task<bool> DeleteFinancialDimensionValue(Guid id)
        {
            var financialDimensionValue = await _db.FinancialDimensionValues.FindAsync(id);
            _db.FinancialDimensionValues.Remove(financialDimensionValue);
            return _db.SaveChanges() > 0;

        }


    }
}
