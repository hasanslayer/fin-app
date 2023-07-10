using AccountingBook.Core.Financial;
using AccountingBook.Data;
using AccountingBook.Web.Dtos.AccountConfigDto;
using AccountingBook.Web.Dtos.MainAccountConfigDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AccountingBook.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MainAccountConfigController : ControllerBase
  {
    private readonly MainAccountConfigRepository _repo;

    public MainAccountConfigController(MainAccountConfigRepository repo)
    {
      _repo = repo;
    }

    [Route("GetMainAccountConfigList")]
    [HttpGet]
    public async Task<IActionResult> GetMainAccountConfigList()
    {
      var mainAccountConfigs = await _repo.GetMainAccountConfigList();
      var mainAccountConfigsDto = mainAccountConfigs.Select(x => new MainAccountConfigDto
      {
        Id = x.Id,
        PatternValue = x.PatternValue,
        RegexPattern = x.RegexPattern,
        AccountConfigsDtos = x.AccountConfigs.OrderBy(ac => ac.Sort).Select(ac => new AccountConfigDto
        {
          Id = ac.Id,
          Sort = ac.Sort,
          IsAllowNull = ac.IsAllowNull,
          DimensionId = ac.FinancialDimensionId,
          DimensionName = ac.FinancialDimension.Name,
          MainAccountConfigId = ac.MainAccountConfigId
        }).ToList()
      }).ToList();

      return Ok(mainAccountConfigsDto);
    }

    [Route("GetMainAccountConfigById")]
    [HttpGet]
    public async Task<IActionResult> GetMainAccountConfigById(Guid id)
    {
      var mainAccountConfig = await _repo.GetMainAccountConfigById(id);
      var mainAccountConfigDto = new MainAccountConfigDto
      {
        Id = mainAccountConfig.Id,
        PatternValue = mainAccountConfig.PatternValue,
        RegexPattern = mainAccountConfig.RegexPattern,
        AccountConfigsDtos = mainAccountConfig.AccountConfigs.Select(ac => new AccountConfigDto
        {
          Id = ac.Id,
          Sort = ac.Sort,
          IsAllowNull = ac.IsAllowNull,
          DimensionId = ac.FinancialDimensionId,
          DimensionName = ac.FinancialDimension.Name,
          MainAccountConfigId = ac.MainAccountConfigId
        }).ToList()
      };

      return Ok(mainAccountConfigDto);

    }


    [Route("AddMainAccountConfig")]
    [HttpPost]
    public async Task<IActionResult> AddMainAccountConfig(AddMainAccountConfigDto addMainAccountConfigDto)
    {
      var mainAccountConfig = new MainAccountConfig
      {
        PatternValue = addMainAccountConfigDto.PatternValue,
        RegexPattern = addMainAccountConfigDto.RegexPattern,
      };

      if (!string.IsNullOrEmpty(addMainAccountConfigDto.RegexPattern) && !string.IsNullOrEmpty(addMainAccountConfigDto.PatternValue))
      {
        Match match = Regex.Match(addMainAccountConfigDto.PatternValue, addMainAccountConfigDto.RegexPattern);
        if (match.Success)
        {
          var newMainAccountConfig = await _repo.AddMainAccountConfig(mainAccountConfig);
          var mainAccountConfigDto = new MainAccountConfigDto
          {
            Id = newMainAccountConfig.Id,
            PatternValue = newMainAccountConfig.PatternValue,
            RegexPattern = newMainAccountConfig.RegexPattern
          };

          return Ok(mainAccountConfigDto);
        }
        else
        {
          return BadRequest("The value you entered does not match your pattern");
        }
      }
      else
      {
        return BadRequest("an error occurred");
      }



    }

    [Route("AddAccountConfigToMainAccountConfig")]
    [HttpPost]
    public async Task<IActionResult> AddAccountConfigToMainAccountConfig(Guid mainAccountConfigId, List<Guid> accountConfigIds)
    {
      var mainAccountConfig = await _repo.AddAccountConfigToMainAccountConfig(mainAccountConfigId, accountConfigIds);

      var mainAccountConfigDto = new MainAccountConfigDto
      {
        Id = mainAccountConfigId,
        PatternValue = mainAccountConfig.PatternValue,
        RegexPattern = mainAccountConfig.RegexPattern,
        AccountConfigsDtos = mainAccountConfig.AccountConfigs.Select(ac => new AccountConfigDto
        {
          Id = ac.Id,
          Sort = ac.Sort,
          IsAllowNull = ac.IsAllowNull,
          DimensionId = ac.FinancialDimensionId,
          DimensionName = ac.FinancialDimension.Name,
          MainAccountConfigId = ac.MainAccountConfigId
        }).ToList()
      };

      return Ok(mainAccountConfigDto);
    }

    [Route("DeleteMainAccountConfig")]
    [HttpDelete]
    public async Task<IActionResult> DeleteAccountConfig(Guid id)
    {
      var founded = await _repo.GetMainAccountConfigById(id);

      if (founded != null)
      {

        var isDeleted = await _repo.DeleteMainAccountConfig(id);

        return Ok(isDeleted);
      }
      else
      {
        return NotFound("not founded");
      }
    }
  }
}
