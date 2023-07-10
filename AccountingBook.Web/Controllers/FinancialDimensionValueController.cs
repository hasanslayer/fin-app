using AccountingBook.Core.Financial;
using AccountingBook.Data;
using AccountingBook.Web.Dtos.FinancialDimensionValueDto;
using AccountingBook.Web.Dtos.FinancialDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingBook.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FinancialDimensionValueController : ControllerBase
  {
    private readonly FinancialDimensionValueRepository _repo;

    public FinancialDimensionValueController(FinancialDimensionValueRepository repo)
    {
      _repo = repo;
    }

    [Route("GetFinancialDimensionValues")]
    [HttpGet]
    public async Task<IActionResult> GetFinancialDimensionValues()
    {
      var financialDimensionValuesList = await _repo.GetFinancialDimensionValues();
      var financailDimensionListDto = financialDimensionValuesList
        .Select(item => new FinancialDimensionValueDto
        {
          Id = item.Id,
          Value = item.Value,
          DimensionType = item.DimensionType.Code,
          DimensionId = item.DimensionId,
          FinancialDimensionId = item.DimensionTypeId
        }).ToList();

      return Ok(financailDimensionListDto);
    }

    [Route("GetFinancialDimensionValueByDimensionId")]
    [HttpGet]
    public async Task<IActionResult> GetFinancialDimensionValueByDimensionId(Guid dimensionId)
    {
      var financialDimensionValuesList = await _repo.GetFinancialDimensionValuesByDimensionId(dimensionId);
      var financailDimensionListDto = financialDimensionValuesList
        .Select(item => new FinancialDimensionValueDto
        {
          Id = item.Id,
          Value = item.Value,
          DimensionType = item.DimensionType.Code,
          DimensionId = item.DimensionId,
          FinancialDimensionId = item.DimensionTypeId
        }).ToList();

      return Ok(financailDimensionListDto);
    }

    [Route("CreateFinancialDimensionValue")]
    [HttpPost]
    public async Task<IActionResult> CreateFinancialDimensionValue(AddFinancialDimensionValueDto addFinancialDimensionValueDto)
    {
      var isFouned = await _repo.GetFinancialDimensionValueByDimensionIdAndValue(addFinancialDimensionValueDto.FinancialDimensionId, addFinancialDimensionValueDto.Value, Guid.Empty);

      if (isFouned != null)
      {
        return BadRequest("The Dimension is used before with the value");
      }
      FinancialDimensionValue mewFinancialDimensionValue = new FinancialDimensionValue()
      {
        DimensionId = addFinancialDimensionValueDto.DimensionId,
        DimensionTypeId = addFinancialDimensionValueDto.FinancialDimensionId,
        Value = addFinancialDimensionValueDto.Value
      };
      var newFinancialDimensionValue = await _repo.AddFinancialDimensionValue(mewFinancialDimensionValue);

      var newFinancialDimensionValueDto = new FinancialDimensionValueDto
      {
        Id = newFinancialDimensionValue.Id,
        Value = newFinancialDimensionValue.Value,
        DimensionId = newFinancialDimensionValue.DimensionId,
        DimensionType = newFinancialDimensionValue.DimensionType.Code,
        FinancialDimensionId = newFinancialDimensionValue.DimensionTypeId
      };

      return Ok(newFinancialDimensionValueDto);
    }

    [Route("UpdateFinancialDimensionValue")]
    [HttpPost]
    public async Task<IActionResult> UpdateFinancialDimensionValue(EditFinancialDimensionValueDto editFinancialDimensionValueDto)
    {
      var founded = await _repo.GetFinancialDimensionValueById(editFinancialDimensionValueDto.FinancialDimensionValueId);
      var foundedByDimensionIdAndValue = await _repo.GetFinancialDimensionValueByDimensionIdAndValue(editFinancialDimensionValueDto.FinancialDimensionId, editFinancialDimensionValueDto.Value, editFinancialDimensionValueDto.FinancialDimensionValueId);

      if (foundedByDimensionIdAndValue != null
        && foundedByDimensionIdAndValue.Id != editFinancialDimensionValueDto.FinancialDimensionValueId)
      {
        return BadRequest("try another value, the value you entered was used by this dimension");
      }

      if (founded != null)
      {

        founded.Value = editFinancialDimensionValueDto.Value;
        founded.DimensionId = editFinancialDimensionValueDto.DimensionId;
        founded.DimensionTypeId = editFinancialDimensionValueDto.FinancialDimensionId;
        founded.Id = editFinancialDimensionValueDto.FinancialDimensionValueId;

        var updatedFinancialDimensionValue = _repo.UpdateFinancialDimensionValue(founded);

        var updatedFinancialDimensionDto = new FinancialDimensionValueDto
        {
          Id = updatedFinancialDimensionValue.Id,
          DimensionId = updatedFinancialDimensionValue.DimensionId,
          DimensionType = updatedFinancialDimensionValue.DimensionType.Code,
          FinancialDimensionId = updatedFinancialDimensionValue.DimensionTypeId,
          Value = updatedFinancialDimensionValue.Value
        };

        return Ok(updatedFinancialDimensionDto);
      }
      else
      {
        return NotFound("not founded");
      }

    }

    [Route("DeleteFinancialDimensionValue")]
    [HttpDelete]
    public async Task<IActionResult> DeleteFinancialDimensionValue(Guid id)
    {
      var founded = await _repo.GetFinancialDimensionValueById(id);

      if (founded != null)
      {

        var isDeleted = await _repo.DeleteFinancialDimensionValue(id);

        return Ok(isDeleted);
      }
      else
      {
        return NotFound("not founded");
      }
    }
  }
}
