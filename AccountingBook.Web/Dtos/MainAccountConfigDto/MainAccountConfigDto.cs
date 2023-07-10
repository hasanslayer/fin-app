using AccountingBook.Core.Financial;
using AccountingBook.Web.Dtos.AccountConfigDto;
using System;
using System.Collections.Generic;

namespace AccountingBook.Web.Dtos.MainAccountConfigDto
{
  public class MainAccountConfigDto
  {
    public Guid Id { get; set; }

    public string PatternValue { get; set; }
    public string RegexPattern { get; set; }

    public ICollection<AccountConfigDto.AccountConfigDto> AccountConfigsDtos { get; set; }
  }
}
