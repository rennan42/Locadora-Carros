using LocadoraCarros.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraCarros.Infrastructure.Mapping
{
    public class VeiculoMapping : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.ToTable("Veiculos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Modelo)
                   .IsRequired()
                   .HasColumnType("varchar(100)");

            builder.Property(p => p.Placa)
                   .IsRequired()
                   .HasColumnType("varchar(100)");

            builder.Property(p => p.DataCadastro)
                  .IsRequired();

            builder.Property(p => p.Status)
                  .IsRequired();
        }
    }
}
