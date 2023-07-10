using System.ComponentModel.DataAnnotations;

namespace AccountingBook.Web.Dtos.FinancialDto
{
  public class AddFinancialDimensionDto
  {
    [Required]
    public string Name { get; set; }
    [Required]
    public string Code { get; set; }
  }
}
