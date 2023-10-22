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
                              .FirstOrDefaultAsync(p => p.Placa == placa);
        }

        public async Task<IList<Veiculo>> ListarPorStatus(EStatusVeiculo status)
        {
            return await DbSet.AsNoTracking()
                              .Where(p => p.Status == status)
                              .ToListAsync();
        }

        public async Task<bool> AtualizarStatus(string placa, EStatusVeiculo status)
        {
            var veiculo = await DbSet.FirstOrDefaultAsync(p => p.Placa == placa);

            if (veiculo == null)
                return false;

            veiculo.AtualizarStatus(status);
            await Db.SaveChangesAsync();

            return true;
        }

        public async Task<string> ConsultarPlacaPorId(long id)
        {
            return await DbSet.AsNoTracking()
                              .Where(p => p.Id == id)
                              .Select(p => p.Placa)
                              .FirstAsync();
        }
    }
}
