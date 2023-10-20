using AutoMapper;
using LocadoraCarros.Application.ViewModels;
using LocadoraCarros.Domain.Repositorios;
using MediatR;

namespace LocadoraCarros.Application.Veiculos.Consultas.BuscarPorPlaca
{
    public class BuscarVeiculoPorPlacaComandoHandler : IRequestHandler<BuscarVeiculoPorPlacaComando, VeiculoViewModel>
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;
        private readonly IMapper _mapper;

        public BuscarVeiculoPorPlacaComandoHandler(IVeiculoRepositorio veiculoRepositorio, IMapper mapper)
        {
            _veiculoRepositorio = veiculoRepositorio;
            _mapper = mapper;
        }

        public async Task<VeiculoViewModel> Handle(BuscarVeiculoPorPlacaComando request, CancellationToken cancellationToken)
        {
            var veiculo = await _veiculoRepositorio.BuscarPorPlaca(request.Placa);

            return _mapper.Map<VeiculoViewModel>(veiculo);
        }
    }
}
