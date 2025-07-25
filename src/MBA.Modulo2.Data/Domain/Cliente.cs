﻿using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Data.Domain
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }

        public string Nome { get; set; } = null!;

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        public IEnumerable<Favorito> Favoritos { get; set; }
    }
}
