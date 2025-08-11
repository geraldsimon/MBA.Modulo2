using MBA.Modulo2.App.Configuration;
using MBA.Modulo2.App.ViewModels;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MBA.Modulo2.App.Controllers;

public class AccountController(INotifier notifier,
                               AppState appState,
                               UserManager<ApplicationUser> userManager,
                               IVendedorService vendedorService,
                               IClienteService clienteService,
                               SignInManager<ApplicationUser> signInManager,
                               IHttpContextAccessor httpContextAccessor) : MainController(notifier, appState, signInManager, vendedorService, httpContextAccessor)
{
    private readonly IVendedorService _vendedorService = vendedorService;
    private readonly IClienteService _clienteService = clienteService;
    private readonly AppState _appState = appState;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

            if (result.Succeeded)
            {
                _appState.UserStateId = Guid.Parse(_userManager.GetUserId(User));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Email or password is incorrect.");
                return View(model);
            }
        }
        return View(model);
    }


    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(CadastroViewModel model)
    {
        if (ModelState.IsValid)
        {

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Senha);


            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                var logedUser = await _userManager.GetUserAsync(User);
                var userId = logedUser?.Id;

                var vendedorId = Guid.NewGuid();
                Vendedor vendedor = new()
                {
                    Id = vendedorId,
                    Nome = model.Nome,
                    ApplicationUserId = user.Id,
                    CriadoEm = DateTime.UtcNow,
                };
                await _vendedorService.AdicionaAsync(vendedor);

                var clienteId = Guid.NewGuid();
                Cliente cliente = new()
                {
                    Id = clienteId,
                    Nome = model.Nome,
                    ApplicationUserId = user.Id,
                    CriadoEm = DateTime.UtcNow
                };

                var claimsToAdd = new[]
                {
                    new Claim("Produtos", "VI"),
                    new Claim("Produtos", "AD"),
                    new Claim("Produtos", "ED"),
                    new Claim("Produtos", "EX")
                };


                foreach (var claim in claimsToAdd)
                {
                    await _userManager.AddClaimAsync(user, claim);
                }

                _appState.UserStateId = (Guid)userId;
                _appState.VendedorStateId = vendedorId;

                await _clienteService.AdicionaAsync(cliente);
                await _signInManager.SignOutAsync();
            }


            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
