using GestaoInventario.Infraestrutura.Data;
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


using var serviceProvider = services.BuildServiceProvider();
using var scope = serviceProvider.CreateScope();

//var context = scope.ServiceProvider.GetRequiredService<GestaoInventarioContext>();

//try
//{
//    context.Database.OpenConnection();
//    Console.WriteLine("Ligação feita com sucesso!");
//}
//catch (Exception ex)
//{
//    Console.WriteLine("Erro ao ligar: " + ex.Message);
//}

var context = scope.ServiceProvider.GetRequiredService<GestaoInventarioContext>();

try
{
    var categorias = await context.Categoria.ToListAsync();
    Console.WriteLine($"Ligação e mapeamento OK. {categorias.Count} categoria(s) encontrada(s).");
}
catch (Exception ex)
{
    Console.WriteLine("Erro ao consultar: " + ex.Message);
}


//using (SqlConnection conn = new SqlConnection(connectionString))
//{
//    try
//    {
//        conn.Open();
//        Console.WriteLine("Ligação feita com sucesso!");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine("Erro ao ligar: " + ex.Message);
//    }
//}

