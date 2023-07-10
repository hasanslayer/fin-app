using System.ComponentModel.DataAnnotations;

namespace AccountingBook.Web.Dtos.MainAccountConfigDto
{
  public class AddMainAccountConfigDto
  {
    [Required]
    public string RegexPattern { get; set; } = "^[1-9]{1}..[1-9]{1}";
    [Required]
    public string PatternValue { get; set; }
  }
}
