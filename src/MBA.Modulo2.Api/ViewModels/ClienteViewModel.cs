using MBA.Modulo2.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.Api.ViewModels
{
    public class ClienteViewModel
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "The name field is mandatory.")]
        public string Name { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Produto> Favoritos { get; set; } = [];
    }
}
