﻿namespace MBA.Modulo2.Data.Models;

public class Vendedor
{
    public Guid Id { get; set; }
    public Guid ApplicationUserId { get; set; }
    public string Nome { get; set; } = null!;
    public bool Ativo { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public ICollection<Produto> Produtos { get; set; } = [];
}