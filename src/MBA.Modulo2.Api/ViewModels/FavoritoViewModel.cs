using MBA.Modulo2.Data.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using MBA.Modulo2.Data.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace MBA.Modulo2.Api.ViewModels
{
    public class FavoritoViewModel
    {
        [SwaggerIgnore]
        public DateTime DataAdicao { get; set; } = DateTime.Now;

        [ForeignKey(nameof(Produto))]
        public Guid ProdutoId { get; set; }


        [SwaggerIgnore]
        [ForeignKey(nameof(Cliente))]
        public Guid ClienteId { get; set; }
    }
}