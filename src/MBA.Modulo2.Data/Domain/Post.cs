namespace MBA.Modulo2.Data.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid VendedorId { get; set; }  
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
    }
}