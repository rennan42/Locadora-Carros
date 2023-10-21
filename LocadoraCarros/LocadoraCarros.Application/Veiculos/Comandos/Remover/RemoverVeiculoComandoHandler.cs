using LocadoraCarros.Application.Veiculos.Eventos.AlterarStatusVeiculo;
using LocadoraCarros.Domain.Enum;
using LocadoraCarros.Domain.Repositorios;
using MediatR;

namespace LocadoraCarros.Application.Veiculos.Comandos.Remover
{
    public class RemoverVeiculoComandoHandler : IRequestHandler<RemoverVeiculoComando>
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;
        private readonly IPublisher _publisher;

        public RemoverVeiculoComandoHandler(IVeiculoRepositorio veiculoRepositorio, IPublisher publisher)
        {
            _veiculoRepositorio = veiculoRepositorio;
            _publisher = publisher;
        }

        public async Task Handle(RemoverVeiculoComando request, CancellationToken cancellationToken)
        {
            var placa = await _veiculoRepositorio.ConsultarPlacaPorId(request.Id);
            await _veiculoRepositorio.Remover(request.Id);

            await _publisher.Publish(new AlterarStatusVeiculoEvent
            {
                Acao = EAcaoVeiculoEvento.REMOVIDO,
                Placa = placa,
            }, cancellationToken);
        }
    }
}
