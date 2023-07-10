using System;

namespace AccountingBook.Web.Dtos.FinancialDimensionValueDto
{
  public class FinancialDimensionValueDto
  {
    public Guid Id { get; set; }
    /// <summary>
    /// code of financial dimension
    /// </summary>
    public string DimensionType { get; set; }
    public Guid FinancialDimensionId { get; set; }
    public string DimensionId { get; set; }
    public string Value { get; set; }
  }
}
