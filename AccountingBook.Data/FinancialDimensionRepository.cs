
using AccountingBook.Core.Financial;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountingBook.Data
{
    public class FinancialDimensionRepository
    {
        private readonly AppDbContext _db;
        public FinancialDimensionRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<FinancialDimension>> GetFinancialDimensions()
        {
            var financialDimesions = await _db.FinancialDimensions.ToListAsync();

            return financialDimesions;
        }

        public async Task<FinancialDimension> AddFinancialDimension(FinancialDimension financialDimension)
        {
            await _db.FinancialDimensions.AddAsync(financialDimension);
            await _db.SaveChangesAsync();
            return financialDimension;
        }

        public async Task<FinancialDimension> GetFinancialDimensionByCode(string code)
        {
            var foundedFinancialDimension = await _db.FinancialDimensions.FirstOrDefaultAsync(f => f.Code.ToLower() == code.ToLower());
            return foundedFinancialDimension;
        }
        public async Task<FinancialDimension> GetFinancialDimensionById(Guid Id)
        {
            var foundedFinancialDimension = await _db.FinancialDimensions.FirstOrDefaultAsync(f => f.Id == Id);
            return foundedFinancialDimension;
        }

        public FinancialDimension UpdateFinancialDimension(FinancialDimension financialDimension)
        {
            var updatedFinancialDimension = _db.FinancialDimensions.Update(financialDimension);
            _db.SaveChanges();
            return updatedFinancialDimension.Entity;
        }

        public async Task<bool> DeleteFinancialDimension(Guid id)
        {
            var founedFinancialDimension = await _db.FinancialDimensions.FindAsync(id);
            _db.FinancialDimensions.Remove(founedFinancialDimension);
            return _db.SaveChanges() > 0;


        }
    }
}
