using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.App.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
