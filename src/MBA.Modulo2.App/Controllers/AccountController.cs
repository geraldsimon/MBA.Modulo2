using MBA.Modulo2.App.Configuration;
using MBA.Modulo2.App.ViewModels;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MBA.Modulo2.App.Controllers;

public class AccountController(IVendedorService vendedorService,
                               IClienteService clienteService,
                               AppState appState,
                               SignInManager<ApplicationUser> signInManager,
                               UserManager<ApplicationUser> userManager) : Controller
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
                if (_appState.UserStateId == null)
                {
                    _appState.UserStateId = Guid.Parse(_userManager.GetUserId(User));
                    var vendedor = await _vendedorService.PegarVendedorPorAspNetUserIdAsync((Guid)_appState.UserStateId);
                    _appState.VendedorStateId = vendedor.Id;

                    var cliente = await _clienteService.PegarClintePorAspNetUserIdAsync((Guid)_appState.UserStateId);
                    _appState.ClienteStateId = cliente.Id;
                }

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
                
                _appState.UserStateId = (Guid)userId;
                _appState.VendedorStateId = vendedorId;
                _appState.ClienteStateId = clienteId;

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
