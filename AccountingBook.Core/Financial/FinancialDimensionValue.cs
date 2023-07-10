using System;
using System.Collections.Generic;
using System.Text;

namespace AccountingBook.Core.Financial
{
    public class FinancialDimensionValue
    {
        public Guid Id { get; set; }


        public string DimensionId { get; set; } // BU01 / CIT001

        public FinancialDimension DimensionType { get; set; }
        public Guid DimensionTypeId { get; set; }

        public string Value { get; set; }

    }
}
