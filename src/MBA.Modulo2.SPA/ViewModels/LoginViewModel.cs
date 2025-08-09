using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.Spa.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Informe a senha")]
        public string Password { get; set; }
    }
}
