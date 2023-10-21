using AutoMapper;
using LocadoraCarros.Application.ViewModels;
using LocadoraCarros.Domain.Repositorios;
using MediatR;

namespace LocadoraCarros.Application.Veiculos.Consultas.BuscarPorPlaca
{
    public class BuscarVeiculoPorPlacaConsultaHandler : IRequestHandler<BuscarVeiculoPorPlacaConsulta, VeiculoViewModel>
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;
        private readonly IMapper _mapper;

        public BuscarVeiculoPorPlacaConsultaHandler(IVeiculoRepositorio veiculoRepositorio, IMapper mapper)
        {
            _veiculoRepositorio = veiculoRepositorio;
            _mapper = mapper;
        }

        public async Task<VeiculoViewModel> Handle(BuscarVeiculoPorPlacaConsulta request, CancellationToken cancellationToken)
        {
            var veiculo = await _veiculoRepositorio.BuscarPorPlaca(request.Placa);

            return _mapper.Map<VeiculoViewModel>(veiculo);
        }
    }
}
