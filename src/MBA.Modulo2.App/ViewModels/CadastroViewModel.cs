using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.App.ViewModels;

public class CadastroViewModel
{
    [Required(ErrorMessage = "Nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Email é obrigatório.")]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Telefone é obrigatório.")]
   // [RegularExpression(@"^\+1\d{10}$", ErrorMessage = "The phone must not be in the format (999) 8454-1234")]
    public string Telefone { get; set; }


    [Required(ErrorMessage = "Senha é obrigatório.")]
    [StringLength(40, MinimumLength = 8, ErrorMessage = "O {0} deve ter no máximo {2} e {1} caracteres de comprimento.")]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword", ErrorMessage = "Password does not match.")]
    public string Senha { get; set; }

    [Required(ErrorMessage = "Confirmação da senha é obrigatório.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirmação da senha")]
    public string ConfirmPassword { get; set; }
}
