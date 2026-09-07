using GestaoInventario.Infraestrutura.Configuracao;
using GestaoInventario.Modelo.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoInventario.Infraestrutura.Data
{
    public class GestaoInventarioContext : DbContext
    {
        public GestaoInventarioContext(DbContextOptions<GestaoInventarioContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produto { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<MovimentoStock> MovimentoStock { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoriaConfiguracao());
            modelBuilder.ApplyConfiguration(new FornecedorConfiguracao());
            modelBuilder.ApplyConfiguration(new ProdutoConfiguracao());
            modelBuilder.ApplyConfiguration(new MovimentoStockConfiguracao());
                                    
        }
    }
}
