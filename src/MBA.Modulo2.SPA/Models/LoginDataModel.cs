namespace MBA.Modulo2.Spa.Models
{
    public class LoginDataModel
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; } // Em segundos
        public UserTokenModel UserToken { get; set; }
    }
}
