using AutoMapper;
using FluentValidation;
using LocadoraCarros.Application.ViewModels;
using LocadoraCarros.Domain.Entidades;
using LocadoraCarros.Domain.Repositorios;
using MediatR;

namespace LocadoraCarros.Application.Veiculos.Comandos.Criar
{
    public class CriarVeiculoComandoHandler : IRequestHandler<CriarVeiculoComando, VeiculoViewModel>
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;
        private readonly IMapper _mapper;
        private IValidator<CriarVeiculoComando> _validator;

        public CriarVeiculoComandoHandler(IVeiculoRepositorio veiculoRepositorio, IMapper mapper, IValidator<CriarVeiculoComando> validator)
        {
            _veiculoRepositorio = veiculoRepositorio;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<VeiculoViewModel> Handle(CriarVeiculoComando request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var veiculo = _mapper.Map<Veiculo>(request);
            await _veiculoRepositorio.Adicionar(veiculo);

            return _mapper.Map<VeiculoViewModel>(veiculo);
        }
    }
}
