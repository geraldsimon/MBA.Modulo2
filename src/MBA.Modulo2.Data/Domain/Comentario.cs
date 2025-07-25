﻿namespace MBA.Modulo2.Data.Models
{
    public class Comentario
    {
        public Guid Id { get; set; }
        public string Conteudo { get; set; }
        public Guid VendedorId { get; set; } 
        public DateTime CriadoEm { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}