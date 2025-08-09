using MBA.Modulo2.Data;
using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace MBA.Modulo2.App.Configuration;

public static class DbMigrationHelperExtension
{
    public static void UseDbMigrationHelper(this WebApplication app)
    {
        DbMigrationHelpers.EnsureSeedData(app).Wait();
    }
}

public static class DbMigrationHelpers
{
    public static async Task EnsureSeedData(WebApplication serviceScope)
    {
        var services = serviceScope.Services.CreateScope().ServiceProvider;
        await EnsureSeedData(services);
    }

    public static async Task EnsureSeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (env.IsDevelopment())
        {
            await context.Database.MigrateAsync();

            await EnsureSeedAdminUserAsync(serviceProvider);

            await EnsureSeedProdutos(context, serviceProvider);
        }
    }

    private static async Task EnsureSeedProdutos(AppDbContext context, IServiceProvider serviceProvider)
    {
        if (await context.Vendedores.AnyAsync())
            return;

        using var scope = serviceProvider.CreateScope();
        var _user = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var _email = "joaomelo@gmail.com";
        var _password = "Teste@123";

        var idUser = await CreateUserAsync(serviceProvider.CreateScope().ServiceProvider, _email, _password);

        var claimsToAdd = new[]
        {
            new Claim("Produtos", "VI"),
            new Claim("Produtos", "AD"),
            new Claim("Produtos", "ED"),
            new Claim("Produtos", "EX")
        };

        var user = await _user.FindByEmailAsync(_email);

        var existingClaims = await _user.GetClaimsAsync(user);


        var newClaims = claimsToAdd.Where(claim => !existingClaims.Any(c => c.Type == claim.Type && c.Value == claim.Value));

        foreach (var claim in newClaims)
        {
            await _user.AddClaimAsync(user, claim);
        }
        var idVendedor = Guid.NewGuid();
        await context.Vendedores.AddAsync(new Vendedor()
        {
            Id = idVendedor,
            ApplicationUserId = idUser,
            Nome = "Joao Melo",
            CriadoEm = DateTime.UtcNow,
        });

        await context.Clientes.AddAsync(new Cliente()
        {
            Id = Guid.NewGuid(),
            ApplicationUserId = idUser,
            Nome = "Joao Melo",
            CriadoEm = DateTime.UtcNow,
        });

        await context.SaveChangesAsync();

        await context.Categorias.AddAsync(new Categoria()
        {
            Id = Guid.NewGuid(),
            Nome = "LapTop",
            Descricao = "LapTop",
        });

        await context.SaveChangesAsync();

        Guid guidCategoria = await context.Categorias.Where(c => c.Nome == "LapTop").Select(c => c.Id).FirstOrDefaultAsync();

        await CreateProdutos(context, idVendedor, guidCategoria);
        await CreateProdutos(context, idVendedor, guidCategoria);
        await CreateProdutos(context, idVendedor, guidCategoria);
        await CreateProdutos(context, idVendedor, guidCategoria);
        await CreateProdutos(context, idVendedor, guidCategoria);
    }

    private static async Task EnsureSeedAdminUserAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var _user = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var adminEmail = "Admin@gmail.com";
        var adminPassword = "Teste@123";

        var adminUser = await _user.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
            };

            await _user.CreateAsync(adminUser, adminPassword);


        }

        var claimsToAdd = new[]
        {
            new Claim("Categorias", "VI"),
            new Claim("Categorias", "AD"),
            new Claim("Categorias", "ED"),
            new Claim("Categorias", "EX"),

            new Claim("Vendedores", "VI"),
            new Claim("Vendedores", "AD"),
            new Claim("Vendedores", "ED"),
            new Claim("Vendedores", "EX"),

            new Claim("Produtos", "MVI"),
            new Claim("Produtos", "MED"),
            new Claim("Produtos", "TS"),
        };

        var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var manager = await _userManager.FindByEmailAsync("Admin@gmail.com");
        var existingClaims = await _userManager.GetClaimsAsync(adminUser);

        var newClaims = claimsToAdd.Where(claim => !existingClaims.Any(c => c.Type == claim.Type && c.Value == claim.Value));

        foreach (var claim in newClaims)
        {
            await _user.AddClaimAsync(manager, claim);
        }
    }

    private static async Task CreateProdutos(AppDbContext context, Guid idUser, Guid guidCategoria)
    {
        await context.Produtos.AddAsync(new Produto()
        {
            Id = Guid.NewGuid(),
            Nome = "Amazon Fire Max 11 tablet",
            Preco = 229.99M,
            Descricao = "Amazon Fire Max 11 tablet (newest model) vivid 11” display, all-in-one for streaming, reading, and gaming, 14-hour battery life, optional stylus and keyboard, 64 GB, Gray",
            VendedorId = idUser,
            Estoque = 10,
            Imagem = "71troH2aUJL._AC_SX679_.jpg",
            CategoriaId = guidCategoria,
            Ativo = true
        });

        await context.SaveChangesAsync();

        await context.Produtos.AddAsync(new Produto()
        {
            Id = Guid.NewGuid(),
            Nome = "Lenovo IdeaPad 15.6”",
            Preco = 229.99M,
            Descricao = "Lenovo IdeaPad 15.6” FHD Touchscreen Laptop, 40GB RAM 2.5TB Storage (2TB SSD+512GB Docking Station Set), 6-Cores Intel Core i3, Windows 11 Pro with Microsoft Office Included, Plusera Earphones",
            VendedorId = idUser,
            Estoque = 10,
            Imagem = "71S+P-5tdpL._AC_SL1500_.jpg",
            CategoriaId = guidCategoria,
            Ativo = true
        });

        await context.SaveChangesAsync();

        await context.Produtos.AddAsync(new Produto()
        {
            Id = Guid.NewGuid(),
            Nome = "HP 14\" Ultral Light Laptop",
            Preco = 229.99M,
            Descricao = "HP 14\" Ultral Light Laptop for Students and Business, Intel Quad-Core, 8GB RAM, 192GB Storage(64GB eMMC+128GB Ghost Manta SD Card), 1 Year Office 365, USB C, Win 11 S",
            VendedorId = idUser,
            Estoque = 10,
            Imagem = "81divYKpeTL._AC_SL1500_.jpg",
            CategoriaId = guidCategoria,
            Ativo = true
        });

        await context.SaveChangesAsync();

        await context.Posts.AddAsync(new Post()
        {
            Id = Guid.NewGuid(),
            Titulo = "Sample Post",
            Conteudo = "This is a sample post content.",
            VendedorId = idUser,
            CriadoEm = DateTime.UtcNow,
            Comentarios = new List<Comentario>()
            {
                new Comentario()
                {
                    Id = Guid.NewGuid(),
                    Conteudo = "This is a sample comment.",
                    CriadoEm = DateTime.UtcNow,
                    VendedorId = idUser
                },
                new Comentario()
                {
                    Id = Guid.NewGuid(),
                    Conteudo = "This is a sample comment 2.",
                    CriadoEm = DateTime.UtcNow,
                    VendedorId = idUser
                }
            }
        });

        await context.Posts.AddAsync(new Post()
        {
            Id = Guid.NewGuid(),
            Titulo = "Sample Post",
            Conteudo = "This another sample post content kkkk.",
            VendedorId = idUser,
            CriadoEm = DateTime.UtcNow,
            Comentarios = new List<Comentario>()
            {
                new Comentario()
                {
                    Id = Guid.NewGuid(),
                    Conteudo = "This is a sample comment kkk.",
                    CriadoEm = DateTime.UtcNow,
                    VendedorId = idUser
                },
                new Comentario()
                {
                    Id = Guid.NewGuid(),
                    Conteudo = "This is a sample comment 2. kk",
                    CriadoEm = DateTime.UtcNow,
                    VendedorId = idUser
                }
            }
        });

        await context.SaveChangesAsync();
    }

    private static async Task<Guid> CreateUserAsync(IServiceProvider services, string email, string passsword)
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
        };

        await userManager.CreateAsync(user, passsword);
        Guid userId = userManager.Users.FirstOrDefault(x => x.Email == email).Id;

        return userId;
    }
}