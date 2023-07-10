using AccountingBook.Core.Financial;
using AccountingBook.Data;
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
  public class FinancialDimensionController : ControllerBase
  {
    private readonly FinancialDimensionRepository _repo;

    public FinancialDimensionController(FinancialDimensionRepository repo)
    {
      _repo = repo;
    }

    [Route("GetFinancialDimension")]
    [HttpGet]
    public async Task<IActionResult> GetFinancialDimension()
    {
      var financialDimensionList = await _repo.GetFinancialDimensions();
      var financailDimensionListDto = financialDimensionList.Select(x => new FinancialDimensionDto { Id = x.Id, Name = x.Name, Code = x.Code }).ToList();

      return Ok(financailDimensionListDto);
    }

    [Route("CreateFinancialDimension")]
    [HttpPost]
    public async Task<IActionResult> CreateFinancialDimension(AddFinancialDimensionDto addFinancialDimesionDto)
    {
      var founded = await _repo.GetFinancialDimensionByCode(addFinancialDimesionDto.Code);

      if (founded != null)
      {
        return BadRequest("The Code is entered before");
      }
      FinancialDimension mewNinancialDimension = new FinancialDimension()
      {
        Name = addFinancialDimesionDto.Name,
        Code = addFinancialDimesionDto.Code,
      };
      var newFinancialDimension = await _repo.AddFinancialDimension(mewNinancialDimension);

      var newDimensionDto = new FinancialDimensionDto
      {
        Id = newFinancialDimension.Id,
        Name = newFinancialDimension.Name,
        Code = newFinancialDimension.Code,
      };

      return Ok(newDimensionDto);
    }

    [Route("UpdateFinancialDimension")]
    [HttpPost]
    public async Task<IActionResult> UpdateFinancialDimension(EditFinancialDimensionDto editFinancialDimesionDto)
    {
      var founded = await _repo.GetFinancialDimensionById(editFinancialDimesionDto.Id);
      var foundedByCode = await _repo.GetFinancialDimensionByCode(editFinancialDimesionDto.Code);

      if (foundedByCode != null && founded.Id != foundedByCode.Id)
      {
        return BadRequest("try another code, the code you entered was used before");
      }

      if (founded != null)
      {

        founded.Name = editFinancialDimesionDto.Name;
        founded.Code = editFinancialDimesionDto.Code;

        var updatedFinancialDimension = _repo.UpdateFinancialDimension(founded);

        var updatedFinancialDimensionDto = new FinancialDimensionDto
        {
          Id = updatedFinancialDimension.Id,
          Name = updatedFinancialDimension.Name,
          Code = updatedFinancialDimension.Code,
        };

        return Ok(updatedFinancialDimensionDto);
      }
      else
      {
        return NotFound("not founded");
      }

    }

    [Route("DeleteFinancialDimension")]
    [HttpDelete]
    public async Task<IActionResult> DeleteFinancialDimension(Guid id)
    {
      var founded = await _repo.GetFinancialDimensionById(id);

      if (founded != null)
      {

        var isDeleted = await _repo.DeleteFinancialDimension(id);

        return Ok(isDeleted);
      }
      else
      {
        return NotFound("not founded");
      }
    }
  }
}
