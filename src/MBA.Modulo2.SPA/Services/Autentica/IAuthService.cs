using MBA.Modulo2.Spa.Models;

namespace MBA.Modulo2.Spa.Services.Autentica
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);  
        Task<LoginResult> novocadastro(CadastroUserModel cadastroUserModel);
        Task Logout();
    }
}
