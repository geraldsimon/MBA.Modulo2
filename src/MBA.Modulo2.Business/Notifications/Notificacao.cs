namespace MBA.Modulo2.Business.Notifications
{
    public class Notificacao
    {
        public Notificacao(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}