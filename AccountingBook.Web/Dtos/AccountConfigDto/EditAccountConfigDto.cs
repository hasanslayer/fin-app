using System;

namespace AccountingBook.Web.Dtos.AccountConfigDto
{
  public class EditAccountConfigDto
  {
    public Guid Id { get; set; }

    public Guid FinancialDimensionId { get; set; }

    public Guid MainAccountConfigId { get; set; }

    public bool IsAllowNull { get; set; }



    public int Sort { get; set; }
  }
}
