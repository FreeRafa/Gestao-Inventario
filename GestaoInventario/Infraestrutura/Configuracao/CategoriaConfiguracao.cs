using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestaoInventario.Modelo.Entidades;

namespace GestaoInventario.Infraestrutura.Configuracao
{
    public class CategoriaConfiguracao : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(c => c.Nome)
                .IsUnique();

            builder.Property(c => c.Descricao)
                .HasMaxLength(500);
        }
    }
}