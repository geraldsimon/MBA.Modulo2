using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.App.ViewModels;

public class CadastroViewModel
{
    [Required(ErrorMessage ="Name is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Phone is required.")]
   // [RegularExpression(@"^\+1\d{10}$", ErrorMessage = "The phone must not be in the format (999) 8454-1234")]
    public string Phone { get; set; }


    [Required(ErrorMessage = "Password is required.")]
    [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at {2} and at max {1} characters long.")]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword", ErrorMessage = "Password does not match.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm Password is required.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }
}
