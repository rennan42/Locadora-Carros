using LocadoraCarros.Domain.Entidades;
using LocadoraCarros.Domain.Enum;
using LocadoraCarros.Domain.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace LocadoraCarros.Infrastructure.Repositorios
{
    public class VeiculoRepositorio : Repositorio<Veiculo>, IVeiculoRepositorio
    {
        public VeiculoRepositorio(Contexto contexto) : base(contexto)
        {

        }

        public async Task<IList<Veiculo>> ListarPorModelo(EModeloVeiculo modeloVeiculo)
        {
            return await DbSet.AsNoTracking()
                              .Where(p => p.Modelo == modeloVeiculo)
                              .ToListAsync();
        }

        public async Task<Veiculo?> BuscarPorPlaca(string placa)
        {
            return await DbSet.AsNoTracking()
                              .SingleOrDefaultAsync(p => p.Placa == placa);
        }

        public async Task<IList<Veiculo>> ListarPorStatus(EStatusVeiculo status)
        {
            return await DbSet.AsNoTracking()
                              .Where(p => p.Status == status)
                              .ToListAsync();
        }
    }
}
