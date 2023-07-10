using System;

namespace AccountingBook.Web.Dtos.FinancialDto
{
  public class ListFinancialDimensionDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
  }
}
