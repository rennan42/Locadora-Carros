using FluentValidation;
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
        private IValidator<RemoverVeiculoComando> _validator;

        public RemoverVeiculoComandoHandler(IVeiculoRepositorio veiculoRepositorio, IPublisher publisher, IValidator<RemoverVeiculoComando> validator)
        {
            _veiculoRepositorio = veiculoRepositorio;
            _publisher = publisher;
            _validator = validator;
        }

        public async Task Handle(RemoverVeiculoComando request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
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
