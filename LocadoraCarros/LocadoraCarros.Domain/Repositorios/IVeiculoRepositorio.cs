using LocadoraCarros.Domain.Entidades;
using LocadoraCarros.Domain.Enum;

namespace LocadoraCarros.Domain.Repositorios
{
    public interface IVeiculoRepositorio : IRepositorio<Veiculo>
    {
        Task<IList<Veiculo>> ListarPorModelo(EModeloVeiculo modeloVeiculo);
        Task<Veiculo?> BuscarPorPlaca(string placa);
        Task<IList<Veiculo>> ListarPorStatus(EStatusVeiculo status);
        Task<bool> AtualizarStatus(string placa, EStatusVeiculo status);
    }
}
