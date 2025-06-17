using MBA.Modulo2.Business.Notifications;
using MBA.Modulo2.Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MBA.Modulo2.Api.Controllers;


[ApiController]
public abstract class MainController(INotifier notifier) : ControllerBase
{
    private readonly INotifier _notifier = notifier;

    protected bool ValidOperation()
    {
        return !_notifier.HaveNotification();
    }
    protected ActionResult CustomResponse(object result = null)
    {
        if (ValidOperation())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        return BadRequest(new
        {
            success = false,
            errors = _notifier.GetNotifications().Select(n => n.Message)
        });
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotifyErrorModelInvalid(modelState);
        return CustomResponse();
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
        _notifier.Handle(new Notification(mensagem));
    }
}