using System;

namespace AccountingBook.Web.Dtos.AccountConfigDto
{
  public class AccountConfigDto
  {
    public Guid Id { get; set; }

    public string DimensionName { get; set; }
    public Guid DimensionId { get; set; }


    public bool IsAllowNull { get; set; }

    public int Sort { get; set; }
    public Guid MainAccountConfigId { get;  set; }
  }
}
