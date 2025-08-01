using System.IdentityModel.Tokens.Jwt;

namespace MBA.Modulo2.Business.Functions
{
    public static class FuncoesGerais
    {
        public static Guid PegarOIdDoUsuarioPeloToken(string authorizationHeader)
        {
            var token = authorizationHeader.Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            if (Guid.TryParse(userId, out Guid userGuid))
            {
                return userGuid;
            }
            else
            {
                throw new InvalidOperationException("Invalid GUID in token.");
            }
        }


        public static Guid PegarOIdDoClientePeloToken(string authorizationHeader)
        {
            var token = authorizationHeader.Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "clienteId")?.Value;

            if (Guid.TryParse(userId, out Guid userGuid))
            {
                return userGuid;
            }
            else
            {
                throw new InvalidOperationException("Invalid GUID in token.");
            }
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length > maxLength ? string.Concat(value.AsSpan(0, maxLength), "...") : value;
        }
    }
}
