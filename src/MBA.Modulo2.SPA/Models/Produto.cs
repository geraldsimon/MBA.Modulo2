
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MBA.Modulo2.Spa.Models;

public class Produto
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Nome { get; set; } = null!;

    public string Descricao { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Preco { get; set; }
    
    [Range(0, 999999.99)]
    public int Estoque { get; set; }

    public string Imagem { get; set; }
    public bool Ativo { get; set; }

    public Guid CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;

    public Guid VendedorId { get; set; }
    public Vendedor Vendedor { get; set; } = null!;

    public IEnumerable<Favorito> Favoritos { get; set; }
}