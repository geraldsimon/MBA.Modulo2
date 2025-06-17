using MBA.Modulo2.Business.Services.Interface;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MBA.Modulo2.Business.Services.Implamentation;
public class AspNetUser(IHttpContextAccessor accessor) : IUser
{
    private readonly IHttpContextAccessor _accessor = accessor;

    public string Name => _accessor.HttpContext.User.Identity.Name;

    public Guid GetUserId()
    {
        return IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
    }

    public string GetUserEmail()
    {
        return IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : "";
    }

    public bool IsAuthenticated()
    {
        return _accessor.HttpContext.User.Identity.IsAuthenticated;
    }

    public bool IsInRole(string role)
    {
        return _accessor.HttpContext.User.IsInRole(role);
    }

    public IEnumerable<Claim> GetClaimsIdentity()
    {
        return _accessor.HttpContext.User.Claims;
    }
}

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentException(nameof(principal));
        }

        var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
        return claim?.Value;
    }

    public static string GetUserEmail(this ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentException(nameof(principal));
        }

        var claim = principal.FindFirst(ClaimTypes.Email);
        return claim?.Value;
    }
}