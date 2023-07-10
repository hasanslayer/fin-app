using System;

namespace AccountingBook.Web.Dtos.FinancialDimensionValueDto
{
  public class EditFinancialDimensionValueDto
  {
    public Guid FinancialDimensionValueId { get; set; }
    /// <summary>
    /// code of financial dimension
    /// </summary>
    public string DimensionType { get; set; }
    public Guid FinancialDimensionId { get; set; }
    public string DimensionId { get; set; } // CIT001 / BU01
    public string Value { get; set; } // Cust01
  }
}
