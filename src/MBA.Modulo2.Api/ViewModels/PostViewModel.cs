using System.Text.Json.Serialization;

namespace MBA.Modulo2.Api.ViewModels;

public class PostViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Conteudo { get; set; }
    [JsonIgnore] 
    public Guid VendedorId { get; set; }
    public List<ComentarioViewModel> Comentarios { get; set; }
}