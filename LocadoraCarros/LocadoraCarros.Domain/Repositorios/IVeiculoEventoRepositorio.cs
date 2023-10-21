using LocadoraCarros.Domain.Entidades;

namespace LocadoraCarros.Domain.Repositorios
{
    public interface IVeiculoEventoRepositorio : IRepositorio<VeiculoEvento>
    {
        Task<IList<VeiculoEvento>> ListarEventos(string placa);
    }
}
