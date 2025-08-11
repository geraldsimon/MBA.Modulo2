namespace MBA.Modulo2.Data.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public Guid VendedorId { get; set; }  
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
    }
}