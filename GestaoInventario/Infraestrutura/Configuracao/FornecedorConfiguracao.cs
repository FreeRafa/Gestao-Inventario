using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestaoInventario.Modelo.Entidades;

namespace GestaoInventario.Infraestrutura.Configuracao
{
    public class FornecedorConfiguracao : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("Fornecedor");
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(f => f.Nif)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(f => f.Nif)
                .IsUnique();

            builder.Property(f => f.Telefone)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(f => f.Email)
                .HasMaxLength(150);

            builder.HasIndex(f => f.Email)
                .IsUnique();
        }
    }
}