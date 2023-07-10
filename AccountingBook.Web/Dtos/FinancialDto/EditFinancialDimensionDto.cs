using System;
using System.ComponentModel.DataAnnotations;

namespace AccountingBook.Web.Dtos.FinancialDto
{
  public class EditFinancialDimensionDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
  }
}
