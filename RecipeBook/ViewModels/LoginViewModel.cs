using System.ComponentModel.DataAnnotations;

namespace RecipeBook.ViewModels
{
  public class LoginViewModel
  {
    [Required(ErrorMessage = "* Please fill the Email Address field.")]
    [EmailAddress]
    [Display(Name = "Email Address")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "* Please fill out the Password field.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
  }
}