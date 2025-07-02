using System.Security.Claims;

namespace MBA.Modulo2.App.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool CategoriasPermissao(this ClaimsPrincipal user, string requiredPermission)
        {
            var claim = user.Claims.FirstOrDefault(c => c.Type == "Categorias")?.Value;
            var permissoes = claim?.Split(',') ?? Array.Empty<string>();
            return permissoes.Contains(requiredPermission);
        }
    }
}
