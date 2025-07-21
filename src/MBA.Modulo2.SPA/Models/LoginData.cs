namespace MBA.Modulo2.Spa.Models
{
    public class LoginData
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; } // Em segundos
        public UserToken UserToken { get; set; }
    }
}
