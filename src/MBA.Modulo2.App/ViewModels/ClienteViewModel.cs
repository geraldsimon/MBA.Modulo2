namespace MBA.Modulo2.App.ViewModels
{
    public class ClienteViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; } = null!;

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    }
}
