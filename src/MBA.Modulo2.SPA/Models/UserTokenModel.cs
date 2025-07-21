using System.Security.Claims;

namespace MBA.Modulo2.Spa.Models
{
    public class UserTokenModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
