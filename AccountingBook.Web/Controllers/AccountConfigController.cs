using AccountingBook.Core.Financial;
using AccountingBook.Data;
using AccountingBook.Web.Dtos.AccountConfigDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingBook.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountConfigController : ControllerBase
  {
    private readonly AccountConfigRepository _repo;

    public AccountConfigController(AccountConfigRepository repo)
    {
      _repo = repo;
    }

    [Route("GetAccountConfigList")]
    [HttpGet]
    public async Task<IActionResult> GetAccountConfigList()
    {
      var accountConfigs = await _repo.GetAccountConfigList();
      var accountConfigsDto = accountConfigs.Select(x => new AccountConfigDto
      {
        Id = x.Id,
        IsAllowNull = x.IsAllowNull,
        DimensionId = x.FinancialDimensionId,
        DimensionName = x.FinancialDimension.Name,
        MainAccountConfigId = x.MainAccountConfigId,
        Sort = x.Sort
      }).ToList();

      return Ok(accountConfigsDto);
    }
    [Route("GetAccountConfigById")]
    [HttpGet]
    public async Task<IActionResult> GetAccountConfigById(Guid id)
    {
      var accountConfig = await _repo.GetAccountConfigById(id);

      if (accountConfig == null)
      {
        return NotFound("not founded");
      }

      var accountConfigsDto = new AccountConfigDto
      {
        Id = accountConfig.Id,
        IsAllowNull = accountConfig.IsAllowNull,
        DimensionId = accountConfig.FinancialDimensionId,
        DimensionName = accountConfig.FinancialDimension.Name,
        MainAccountConfigId = accountConfig.MainAccountConfigId,
        Sort = accountConfig.Sort
      };

      return Ok(accountConfigsDto);
    }

    [Route("AddAccountConfig")]
    [HttpPost]
    public async Task<IActionResult> AddAccountConfig(AddAccountConfigDto addAccountConfigDto)
    {
      var isExist = await _repo.IsAccountExist(addAccountConfigDto.FinancialDimensionId,addAccountConfigDto.MainAccountConfigId);
      if (isExist)
      {
        return BadRequest("Can't create the same dimension more than once");
      }

      AccountConfig accountConfig = new AccountConfig()
      {
        FinancialDimensionId = addAccountConfigDto.FinancialDimensionId,
        MainAccountConfigId = addAccountConfigDto.MainAccountConfigId,
        IsAllowNull = addAccountConfigDto.IsAllowNull,
        Sort = addAccountConfigDto.Sort
      };
      var newAccountConfig = await _repo.AddAccountConfig(accountConfig);

      var accountConfigsDto = newAccountConfig.Select(x => new AccountConfigDto
      {
        Id = x.Id,
        IsAllowNull = x.IsAllowNull,
        DimensionId = x.FinancialDimensionId,
        DimensionName = x.FinancialDimension.Name,
        MainAccountConfigId = x.MainAccountConfigId,
        Sort = x.Sort
      }).ToList();

      return Ok(accountConfigsDto);
    }

    [Route("UpdateAccountConfig")]
    [HttpPost]
    public async Task<IActionResult> UpdateAccountConfig(EditAccountConfigDto editAccountConfigDto)
    {
      var founded = await _repo.GetAccountConfigById(editAccountConfigDto.Id);
      if (founded != null)
      {
        founded.FinancialDimensionId = editAccountConfigDto.FinancialDimensionId;
        founded.MainAccountConfigId = editAccountConfigDto.MainAccountConfigId;
        founded.Sort = editAccountConfigDto.Sort;
        founded.IsAllowNull = editAccountConfigDto.IsAllowNull;

        var updatedAccountConfig = await _repo.UpdateAccountConfig(founded);

        var updatedAccountConfigDto = updatedAccountConfig.Select(x => new AccountConfigDto
        {
          Id = x.Id,
          IsAllowNull = x.IsAllowNull,
          DimensionId = x.FinancialDimensionId,
          DimensionName = x.FinancialDimension.Name,
          MainAccountConfigId = x.MainAccountConfigId,
          Sort = x.Sort
        }).ToList();

        return Ok(updatedAccountConfigDto);
      }
      else
      {
        return NotFound("Not Founded");
      }
    }

    [Route("DeleteAccountConfig")]
    [HttpDelete]
    public async Task<IActionResult> DeleteAccountConfig(Guid id)
    {
      var founded = await _repo.GetAccountConfigById(id);

      if (founded != null)
      {

        var isDeleted = await _repo.DeleteAccountConfig(id);

        return Ok(isDeleted);
      }
      else
      {
        return NotFound("not founded");
      }
    }

  }
}
