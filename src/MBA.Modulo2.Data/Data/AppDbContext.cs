using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MBA.Modulo2.Data;
public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid,
    IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Favorito>(entity =>
        {
            entity.HasKey(f => new { f.ClienteId, f.ProdutoId });

            entity.HasOne(f => f.Cliente)
                .WithMany(c => c.Favoritos)
                .HasForeignKey(f => f.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(f => f.Produto)
                .WithMany(p => p.Favoritos)
                .HasForeignKey(f => f.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<IdentityUserClaim<Guid>>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });
    }
}