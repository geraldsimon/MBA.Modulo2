using MBA.Modulo2.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.Api.ViewModels
{
    public class ClienteViewModel
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string Nome { get; set; } = null!;

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    }
}
