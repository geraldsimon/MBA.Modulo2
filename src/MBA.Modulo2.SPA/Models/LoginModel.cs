using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.Spa.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage ="Informe a senha")]
        public string? Password { get; set; }
    }
}
