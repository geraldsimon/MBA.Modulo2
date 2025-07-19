using System.Text.Json.Serialization;

namespace MBA.Modulo2.Api.ViewModels;

public class ComentarioViewModel
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid PostId { get; set; }
    [JsonIgnore]
    public Guid VendedorId { get; set; }
}