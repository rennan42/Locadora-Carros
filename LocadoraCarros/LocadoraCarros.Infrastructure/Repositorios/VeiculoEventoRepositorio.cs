using LocadoraCarros.Domain.Entidades;
using LocadoraCarros.Domain.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace LocadoraCarros.Infrastructure.Repositorios
{
    public class VeiculoEventoRepositorio : Repositorio<VeiculoEvento>, IVeiculoEventoRepositorio
    {
        public VeiculoEventoRepositorio(Contexto contexto) : base(contexto)
        {

        }

        public async Task<IList<VeiculoEvento>> ListarEventos(string placa)
        {
            return await DbSet.AsNoTracking()
                              .Where(p => p.PlacaVeiculo == placa)
                              .OrderBy(p => p.Data)
                              .ToListAsync();
        }
    }
}
