using MBA.Modulo2.Api.Extensions;
using MBA.Modulo2.Api.ViewModels;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MBA.Modulo2.Api.Controllers;

[Route("api/[controller]")]
public class AuthController(INotifier notifier,
                      SignInManager<ApplicationUser> signInManager,
                      IClienteService clienteService,
                      UserManager<ApplicationUser> userManager,
                      IOptions<AppSettings> appSettings) : MainController(notifier)
{
    private readonly IClienteService _clienteService = clienteService;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly AppSettings _appSettings = appSettings.Value;


    [HttpPost("newAccount")]
    public async Task<ActionResult> NewAccount(RegisterUserViewModel registerUser)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var identityUser = new ApplicationUser
        {
            UserName = registerUser.Email,
            Email = registerUser.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(identityUser, registerUser.Password);
        if (result.Succeeded)
        {

            var clienteId = Guid.NewGuid();
            Cliente cliente = new()
            {
                Id = clienteId,
                Nome = registerUser.Nome,
                ApplicationUserId = identityUser.Id,
                CriadoEm = DateTime.UtcNow
            };


            await _clienteService.AdicionaAsync(cliente);

            var claimsToAdd = new[]
            {
                new Claim("Produtos", "VI"),//vizualizar
                new Claim("Favoritos", "AD"),// add em favoritos
                new Claim("Favoritos", "RM"),// remover de favoritos
                new Claim("Perfil", "ED") //editar perfil
            };

            foreach (var claim in claimsToAdd)
            {
                await _userManager.AddClaimAsync(identityUser, claim);
            }

            await _signInManager.SignInAsync(identityUser, false);

            var vendedor = await Pegarendedor(Guid.Parse(_userManager.GetUserId(User)));

            return CustomResponse(await GenerateJwt(identityUser.Email, vendedor.Id));
        }
        foreach (var error in result.Errors)
        {
            ReportError(error.Description);
        }

        return CustomResponse(registerUser);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginUserViewModel loginUser)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

        if (result.Succeeded)
        {
            var vendedor = await Pegarendedor(Guid.Parse(_userManager.GetUserId(User)));
            return CustomResponse(await GenerateJwt(loginUser.Email, vendedor.Id));
        }
        if (result.IsLockedOut)
        {
            ReportError("User temporarily blocked due to invalid attempts");
            return CustomResponse(loginUser);
        }

        ReportError("Incorrect Username or Password");
        return CustomResponse(loginUser);
    }

    private async Task<LoginResponseViewModel> GenerateJwt(string email, Guid clienteId)
    {
        var userJwt = await _userManager.FindByEmailAsync(email);
        var claims = await _userManager.GetClaimsAsync(userJwt);
        var userRoles = await _userManager.GetRolesAsync(userJwt);


        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userJwt.Id.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, userJwt.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim("role", userRole));
        }

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Emissor,
            Audience = _appSettings.ValidoEm,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var encodedToken = tokenHandler.WriteToken(token);

        var response = new LoginResponseViewModel
        {
            ClienteId = clienteId,
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
            UserToken = new UserTokenViewModel
            {
                Id = userJwt.Id.ToString(),
                Email = userJwt.Email,
                Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
            }
        };
        return response;
    }


    private async Task<Cliente> Pegarendedor(Guid id)
    {
        return await _clienteService.PegarClintePorAspNetUserIdAsync(id);
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - DateTimeOffset.UnixEpoch).TotalSeconds);
}
