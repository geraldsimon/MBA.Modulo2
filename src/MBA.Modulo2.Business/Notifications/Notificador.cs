using MBA.Modulo2.Business.Services.Interface;

namespace MBA.Modulo2.Business.Notifications;

public class Notificador : INotifier
{
    private readonly List<Notificacao> _notificacoes;

    public Notificador()
    {
        _notificacoes = [];
    }

    public void Handle(Notificacao notificacao)
    {
        _notificacoes.Add(notificacao);
    }

    public List<Notificacao> GetNotifications()
    {
        return _notificacoes;
    }

    public bool HaveNotification()
    {
        return _notificacoes.Count != 0;
    }
}