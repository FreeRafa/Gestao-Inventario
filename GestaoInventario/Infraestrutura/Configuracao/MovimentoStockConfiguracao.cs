using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GestaoInventario.Modelo.Entidades;

namespace GestaoInventario.Infraestrutura.Configuracao
{
    public class MovimentoStockConfiguracao : IEntityTypeConfiguration<MovimentoStock>
    {
        public void Configure(EntityTypeBuilder<MovimentoStock> builder)
        {
            builder.ToTable("MovimentoStock");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.TipoMovimento)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(m => m.Quantidade)
                .IsRequired();

            builder.Property(m => m.Data)
                .IsRequired();

            builder.Property(m => m.Observacao)
                .HasMaxLength(500);

            builder.HasOne(m => m.Produto)
                .WithMany(p => p.Movimentos)
                .HasForeignKey(m => m.ProdutoId);

            builder.HasOne(m => m.Fornecedor)
                .WithMany(f => f.Movimentos)
                .HasForeignKey(m => m.FornecedorId)
                .IsRequired(false);
        }
    }
}
