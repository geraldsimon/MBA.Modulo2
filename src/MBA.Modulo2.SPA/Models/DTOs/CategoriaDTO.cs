using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.Spa.Models.DTOs
{
    public class CategoriaDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}