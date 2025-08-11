using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.Shared.ViewModels;

public class CadastroViewModel
{
    [Required(ErrorMessage ="Nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Email é obrigatório.")]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Telefone é obrigatório.")]
    public string Telefone { get; set; }


    [Required(ErrorMessage = "Senha é obrigatório.")]
    [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at {2} and at max {1} characters long.")]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword", ErrorMessage = "A senha e a confirmação de senha devem ser iguais.")]
    public string Senha { get; set; }

    [Required(ErrorMessage = "Confirmação de Senha é obrigatória.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirme a senha")]
    public string ConfirmaSenha { get; set; }
}
