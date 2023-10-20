using AutoMapper;
using LocadoraCarros.Application.ViewModels;
using LocadoraCarros.Domain.Repositorios;
using MediatR;

namespace LocadoraCarros.Application.Veiculos.Consultas.ListarVeiculosPorModelo
{
    internal class ListarVeiculosPorModeloComandoHandler : IRequestHandler<ListarVeiculosPorModeloComando, IList<VeiculoViewModel>>
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;
        private readonly IMapper _mapper;

        public ListarVeiculosPorModeloComandoHandler(IVeiculoRepositorio veiculoRepositorio, IMapper mapper)
        {
            _veiculoRepositorio = veiculoRepositorio;
            _mapper = mapper;
        }

        public async Task<IList<VeiculoViewModel>> Handle(ListarVeiculosPorModeloComando request, CancellationToken cancellationToken)
        {
            var veiculos = await _veiculoRepositorio.ListarPorModelo(request.Modelo);

            return _mapper.Map<IList<VeiculoViewModel>>(veiculos);
        }
    }
}
