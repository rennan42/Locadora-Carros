using LocadoraCarros.Domain.Entidades;
using LocadoraCarros.Domain.Repositorios;

namespace LocadoraCarros.Infrastructure.Repositorios
{
    public class VeiculoRepositorio : Repositorio<Veiculo>, IVeiculoRepositorio
    {
        public VeiculoRepositorio(Contexto contexto) : base(contexto)
        {

        }
    }
}
