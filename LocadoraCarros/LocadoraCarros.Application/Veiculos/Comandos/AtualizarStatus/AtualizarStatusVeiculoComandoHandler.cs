using AutoMapper;
using FluentValidation;
using LocadoraCarros.Application.Veiculos.Comandos.Criar;
using LocadoraCarros.Domain.Repositorios;
using MediatR;

namespace LocadoraCarros.Application.Veiculos.Comandos.AtualizarStatus
{
    public class AtualizarStatusVeiculoComandoHandler : IRequestHandler<AtualizarStatusVeiculoComando, bool>
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;
        private IValidator<AtualizarStatusVeiculoComando> _validator;

        public AtualizarStatusVeiculoComandoHandler(IVeiculoRepositorio veiculoRepositorio, IValidator<AtualizarStatusVeiculoComando> validator)
        {
            _veiculoRepositorio = veiculoRepositorio;
            _validator = validator;
        }

        public async Task<bool> Handle(AtualizarStatusVeiculoComando request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            return await _veiculoRepositorio.AtualizarStatus(request.Placa, request.Status);
        }
    }
}
