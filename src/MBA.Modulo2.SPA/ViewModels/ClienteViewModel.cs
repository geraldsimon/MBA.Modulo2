namespace MBA.Modulo2.Spa.ViewModels
{
    public class ClienteViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; } = null!;

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    }
}