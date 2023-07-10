using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingBook.Core.Enums;
using AccountingBook.Core.Financial;

namespace AccountingBook.Web.Dtos
{
  public class LookupItem
  {
    public LookupItem(long id, string name, AccountType accountType, int accountCode, Account parentAccount)
    {
      Id = id;
      Name = name;
      AccountType = accountType;
      AccountCode = accountCode;
      ParentAccountName = parentAccount.AccountName;
    }
    public long Id { get; set; }
    public string Name { get; set; }
    public AccountType AccountType { get; set; }
    public int AccountCode { get; set; }
    public string ParentAccountName { get; set; }
  }
}
