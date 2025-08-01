using MBA.Modulo2.App.Configuration;
using MBA.Modulo2.App.Extensions;
using MBA.Modulo2.Business.Notifications;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MBA.Modulo2.App.Controllers
{
    public abstract class MainController : Controller
    {
        private readonly AppState _appState;
        private readonly INotifier _notifier;
        private readonly IVendedorService _vendedorService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        protected MainController(INotifier notifier, AppState appState, SignInManager<ApplicationUser> signInManager, IVendedorService vendedorService)
        {

            _appState = appState;
            _signInManager = signInManager;
            _vendedorService = vendedorService;
            _notifier = notifier;


            //InitializeAppState().Wait();
        }

        private async Task InitializeAppState()
        {
            if (User?.Identity?.IsAuthenticated== true)
            {
                _appState.UserStateId = Guid.Parse(_signInManager.Context.User.GetUserId());
                if (_appState.VendedorStateId == null)
                {
                    var vendedor = await _vendedorService.PegarVendedorPorAspNetUserIdAsync((Guid)_appState.UserStateId);
                    _appState.VendedorStateId = (Guid?)vendedor.Id;
                }
            }
        }

        protected bool ValidOperation()
        {
            return !_notifier.HaveNotification();
        }
        protected IActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
            {
                // Sucesso: redireciona ou retorna a view atual com resultado, se necessário
                return View(result); // ou RedirectToAction(...) se quiser redirecionar
            }

            // Adiciona os erros ao ModelState para exibir na View
            foreach (var error in _notifier.GetNotifications())
            {
                ModelState.AddModelError(string.Empty, error.Message);
            }

            // Retorna a mesma View com os erros no ModelState
            return View(); // retorna a View atual (usará o modelo que foi postado)
        }

        protected IActionResult CustomResponse(ModelStateDictionary modelState, object model = null)
        {
            if (!modelState.IsValid)
                NotifyErrorModelInvalid(modelState);

            return CustomResponse(model);
        }

        protected void NotifyErrorModelInvalid(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                ReportError(errorMsg);
            }
        }

        protected void ReportError(string mensagem)
        {
            _notifier.Handle(new Notificacao(mensagem));
        }
    }
}