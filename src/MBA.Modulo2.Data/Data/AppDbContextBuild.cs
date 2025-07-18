using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MBA.Modulo2.Data;
public class AppDbContextBuild : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public AppDbContextBuild(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }
    public DbSet<Vendedor> Sellers { get; set; }
    public DbSet<Categoria> Categories { get; set; } = null!;
    public DbSet<Produto> Products { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Comentario> Comments { get; set; } = null!;

    public DbSet<Cliente> Clientes { get; set; } = null!;

    public DbSet<Favorito> Favoritos { get; set; } = null!;
}