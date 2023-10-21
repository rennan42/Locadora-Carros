using LocadoraCarros.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraCarros.Infrastructure.Mapping
{
    public class VeiculoEventoMapping : IEntityTypeConfiguration<VeiculoEvento>
    {
        public void Configure(EntityTypeBuilder<VeiculoEvento> builder)
        {
            builder.ToTable("VeiculoEventos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Acao)
                   .IsRequired();

            builder.Property(p => p.Data)
                   .IsRequired();                   

            builder.Property(p => p.PlacaVeiculo)
                  .IsRequired()
                  .HasColumnType("varchar(7)"); 
        }
    }
}
