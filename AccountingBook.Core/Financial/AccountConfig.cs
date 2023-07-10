using System;
using System.Collections.Generic;
using System.Text;

namespace AccountingBook.Core.Financial
{
    public class AccountConfig
    {
        public Guid Id { get; set; }


        public bool IsAllowNull { get; set; }

        public int Sort { get; set; }

        public FinancialDimension FinancialDimension { get; set; }
        public Guid FinancialDimensionId { get; set; }


        public MainAccountConfig MainAccountConfig { get; set; }
        public Guid MainAccountConfigId { get; set; }
    }
}
