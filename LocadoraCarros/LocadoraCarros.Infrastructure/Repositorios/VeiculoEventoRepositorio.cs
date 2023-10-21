using LocadoraCarros.Domain.Entidades;
using LocadoraCarros.Domain.Repositorios;

namespace LocadoraCarros.Infrastructure.Repositorios
{
    public class VeiculoEventoRepositorio : Repositorio<VeiculoEvento>, IVeiculoEventoRepositorio
    {
        public VeiculoEventoRepositorio(Contexto contexto) : base(contexto)
        {

        }
    }
}
