using MBA.Modulo2.Data.Domain;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Security.Claims;

namespace AppSemTemplate.Extensions;

public class CustomAuthorization
{
	public static async Task<bool> ValidarClaimsUsuarioAsync(HttpContext context, string claimName, string claimValue)
	{
		if (context.User.Identity == null) throw new InvalidOperationException();

		var userManager = context.RequestServices.GetService<UserManager<ApplicationUser>>();

		var userEmail = context.User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;

		if (!string.IsNullOrWhiteSpace(userEmail))
		{
			var user = await userManager.FindByEmailAsync(userEmail);

			if (user != null && await userManager.IsInRoleAsync(user, claimName))
				return true;
		}

		return context.User.Identity.IsAuthenticated &&
			   context.User.Claims.Any(c => c.Type == claimName && c.Value.Split(',').Contains(claimValue));
	}
}

public class RequisitoClaimFilter : IAuthorizationFilter
{
	private readonly Claim _claim;

	public RequisitoClaimFilter(Claim claim)
	{
		_claim = claim;
	}

	public async void OnAuthorization(AuthorizationFilterContext context)
	{
		try
		{
			if (context.HttpContext.User.Identity == null) throw new InvalidOperationException();

			if (!context.HttpContext.User.Identity.IsAuthenticated)
			{
				context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
				{
					area = "Identity",
					page = "/Account/Login",
					ReturnUrl = context.HttpContext.Request.Path.ToString()
				}));
				return;
			}

			if (!await CustomAuthorization.ValidarClaimsUsuarioAsync(context.HttpContext, _claim.Type, _claim.Value))
				context.Result = new StatusCodeResult(403);
		}
		catch (Exception)
		{
			context.Result = new StatusCodeResult(403);
		}
	}
}

public class ClaimsAuthorizeAttribute : TypeFilterAttribute
{
	public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequisitoClaimFilter))
	{
		Arguments = new object[] { new Claim(claimName, claimValue) };
	}
}