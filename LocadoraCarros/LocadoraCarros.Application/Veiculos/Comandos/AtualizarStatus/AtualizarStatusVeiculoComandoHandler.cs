using AutoMapper;
using FluentValidation;
using LocadoraCarros.Application.Veiculos.Comandos.Criar;
using LocadoraCarros.Application.Veiculos.Eventos.AlterarStatusVeiculo;
using LocadoraCarros.Domain.Entidades;
using LocadoraCarros.Domain.Enum;
using LocadoraCarros.Domain.Repositorios;
using MediatR;

namespace LocadoraCarros.Application.Veiculos.Comandos.AtualizarStatus
{
    public class AtualizarStatusVeiculoComandoHandler : IRequestHandler<AtualizarStatusVeiculoComando, bool>
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;
        private IValidator<AtualizarStatusVeiculoComando> _validator;
        private readonly IPublisher _publisher;

        public AtualizarStatusVeiculoComandoHandler(IVeiculoRepositorio veiculoRepositorio, IValidator<AtualizarStatusVeiculoComando> validator, IPublisher publisher)
        {
            _veiculoRepositorio = veiculoRepositorio;
            _validator = validator;
            _publisher = publisher;
        }

        public async Task<bool> Handle(AtualizarStatusVeiculoComando request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var statusAtualizado = await _veiculoRepositorio.AtualizarStatus(request.Placa, request.Status);

            if (statusAtualizado)
            {
                await _publisher.Publish(new AlterarStatusVeiculoEvent
                {
                    Acao = request.Status == EStatusVeiculo.ALUGADO ? EAcaoVeiculoEvento.LOCADO : EAcaoVeiculoEvento.DEVOLVIDO,
                    Placa = request.Placa,
                }, cancellationToken);

                return statusAtualizado;
            }

            return statusAtualizado;

        }
    }
}
