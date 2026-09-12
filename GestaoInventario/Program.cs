using GestaoInventario.Apresentacao.Menu;
using GestaoInventario.Apresentacao.Menu.MenuFluxo;
using GestaoInventario.Apresentacao.Menu.MenuGestao;
using GestaoInventario.Infraestrutura.Data;
using GestaoInventario.Infraestrutura.Repositorio;
using GestaoInventario.Modelo.Interfaces;
using GestaoInventario.Servico;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString = config.GetConnectionString("GestaoInventario")!;

var services = new ServiceCollection();

services.AddDbContext<GestaoInventarioContext>(options =>
    options.UseSqlServer(connectionString));

// Repositórios
services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
services.AddScoped<IFornecedorRepositorio, FornecedorRepositorio>();
services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
services.AddScoped<IMovimentoStockRepositorio, MovimentoStockRepositorio>();

// Serviços
services.AddScoped<CategoriaServico>();
services.AddScoped<FornecedorServico>();
services.AddScoped<ProdutoServico>();
services.AddScoped<MovimentoStockServico>();

// Menus
services.AddScoped<MenuCategoria>();
services.AddScoped<MenuFornecedor>();
services.AddScoped<MenuProduto>();
services.AddScoped<MenuMovimentoStock>();
services.AddScoped<MenuRelatorio>();
services.AddScoped<MenuPrincipal>();

using var serviceProvider = services.BuildServiceProvider();
using var scope = serviceProvider.CreateScope();

var menuPrincipal = scope.ServiceProvider.GetRequiredService<MenuPrincipal>();
await menuPrincipal.ExibirAsync();