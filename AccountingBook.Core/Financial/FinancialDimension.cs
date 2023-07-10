using System;
using System.Collections.Generic;
using System.Text;

namespace AccountingBook.Core.Financial
{
    public class FinancialDimension
    {
        public FinancialDimension()
        {
            FinancialDimensionValues = new HashSet<FinancialDimensionValue>();
            AccountConfigs = new HashSet<AccountConfig>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public ICollection<FinancialDimensionValue> FinancialDimensionValues { get; set; }
        public ICollection<AccountConfig> AccountConfigs { get; set; }

    }
}
