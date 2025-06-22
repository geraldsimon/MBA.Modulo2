using MBA.Modulo2.Business.Notifications;

namespace MBA.Modulo2.Business.Services.Interface;

public interface INotifier
{
    bool HaveNotification();
    List<Notificacao> GetNotifications();
    void Handle(Notificacao notificacao);
}