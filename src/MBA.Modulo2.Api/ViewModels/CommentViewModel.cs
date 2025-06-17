using System.Text.Json.Serialization;

namespace MBA.Modulo2.Api.ViewModels;

public class CommentViewModel
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid PostId { get; set; }
    [JsonIgnore]
    public Guid SellerId { get; set; }
}