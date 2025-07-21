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
        public static bool VendedoresPermissao(this ClaimsPrincipal user, string requiredPermission)
        {
            var claim = user.Claims.FirstOrDefault(c => c.Type == "Vendedores")?.Value;
            var permissoes = claim?.Split(',') ?? Array.Empty<string>();
            return permissoes.Contains(requiredPermission);
        }


        public static bool ProdutosPermissao(this ClaimsPrincipal user, string requiredPermission)
        {
            var claim = user.Claims.FirstOrDefault(c => c.Type == "Produtos")?.Value;
            var permissoes = claim?.Split(',') ?? Array.Empty<string>();
            return permissoes.Contains(requiredPermission);
        }

        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

    }
}
