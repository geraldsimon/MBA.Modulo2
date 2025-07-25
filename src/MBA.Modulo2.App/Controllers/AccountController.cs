using MBA.Modulo2.App.ViewModels;
using MBA.Modulo2.Business.Services.Implementacao;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MBA.Modulo2.App.Controllers;

public class AccountController(IVendedorService VendedorService,
                               IClienteService clienteService,
                               SignInManager<ApplicationUser> signInManager,
                               UserManager<ApplicationUser> userManager) : Controller
{
    private readonly IVendedorService _VendedorService = VendedorService;
    private readonly IClienteService _clienteService = clienteService;
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

                Vendedor vendedor = new()
                {
                    Id = (Guid)userId,
                    Nome = model.Nome,
                };
                await _VendedorService.AdicionaAsync(vendedor);

                Cliente cliente = new()
                {
                    Id = (Guid)userId,
                    Nome = model.Nome,
                    CriadoEm = DateTime.UtcNow
                };
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
