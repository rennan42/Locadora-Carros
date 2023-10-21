using AutoMapper;
using LocadoraCarros.Application.ViewModels;
using LocadoraCarros.Domain.Repositorios;
using MediatR;

namespace LocadoraCarros.Application.Veiculos.Consultas.ListarVeiculosPorStatus
{
    public class ListarVeiculosPorStatusConsultaHandler : IRequestHandler<ListarVeiculosPorStatusConsulta, IList<VeiculoViewModel>>
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;
        private readonly IMapper _mapper;

        public ListarVeiculosPorStatusConsultaHandler(IVeiculoRepositorio veiculoRepositorio, IMapper mapper)
        {
            _veiculoRepositorio = veiculoRepositorio;
            _mapper = mapper;
        }

        public async Task<IList<VeiculoViewModel>> Handle(ListarVeiculosPorStatusConsulta request, CancellationToken cancellationToken)
        {
            var veiculos = await _veiculoRepositorio.ListarPorStatus(request.Status);

            return _mapper.Map<IList<VeiculoViewModel>>(veiculos);
        }
    }
}
