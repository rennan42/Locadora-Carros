using LocadoraCarros.Application.ViewModels;
using LocadoraCarros.Domain.Enum;
using MediatR;

namespace LocadoraCarros.Application.Veiculos.Consultas.ListarVeiculosPorModelo
{
    public class ListarVeiculosPorModeloComando : IRequest<IList<VeiculoViewModel>>
    {
        public EModeloVeiculo Modelo { get; set; }
    }
}
