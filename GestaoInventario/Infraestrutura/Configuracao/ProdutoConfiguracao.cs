using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestaoInventario.Modelo.Entidades;

namespace GestaoInventario.Infraestrutura.Configuracao
{
    internal class ProdutoConfiguracao : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Codigo)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(p => p.Codigo)
                .IsUnique();

            builder.Property(p => p.PrecoUnitario)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.QuantidadeEmStock)
                .IsRequired();

            builder.Property(p => p.StockMinimo)
                .IsRequired();

            builder.HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaId);
        }
    }
}