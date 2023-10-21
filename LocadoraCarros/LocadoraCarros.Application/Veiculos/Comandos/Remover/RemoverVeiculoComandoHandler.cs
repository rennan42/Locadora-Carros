using LocadoraCarros.Domain.Repositorios;
using MediatR;

namespace LocadoraCarros.Application.Veiculos.Comandos.Remover
{
    public class RemoverVeiculoComandoHandler : IRequestHandler<RemoverVeiculoComando>
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;

        public RemoverVeiculoComandoHandler(IVeiculoRepositorio veiculoRepositorio)
        {
            _veiculoRepositorio = veiculoRepositorio;
        }

        public async Task Handle(RemoverVeiculoComando request, CancellationToken cancellationToken)
        {
            await _veiculoRepositorio.Remover(request.Id);
        }
    }
}
