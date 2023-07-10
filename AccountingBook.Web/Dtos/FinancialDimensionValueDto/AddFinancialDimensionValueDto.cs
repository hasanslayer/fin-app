using System;

namespace AccountingBook.Web.Dtos.FinancialDimensionValueDto
{
  public class AddFinancialDimensionValueDto
  {
    /// <summary>
    /// code of financial dimension
    /// </summary>
    public string DimensionType { get; set; }
    public Guid FinancialDimensionId { get; set; }
    public string DimensionId { get; set; } // CIT001 / BU01
    public string Value { get; set; } // Cust01
  }
}
