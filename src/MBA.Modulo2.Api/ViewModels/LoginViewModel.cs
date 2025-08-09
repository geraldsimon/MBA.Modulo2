using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.Shared.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "O Email é obrigatório.")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}
