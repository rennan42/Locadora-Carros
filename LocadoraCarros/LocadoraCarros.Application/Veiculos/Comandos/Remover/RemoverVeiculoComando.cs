using MediatR;

namespace LocadoraCarros.Application.Veiculos.Comandos.Remover
{
    public class RemoverVeiculoComando : IRequest
    {
        public long Id { get; set; }
    }
}
