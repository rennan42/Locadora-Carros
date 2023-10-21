using LocadoraCarros.Application.ViewModels;
using LocadoraCarros.Domain.Enum;
using MediatR;

namespace LocadoraCarros.Application.Veiculos.Consultas.ListarVeiculosPorStatus
{
    public class ListarVeiculosPorStatusConsulta : IRequest<IList<VeiculoViewModel>>
    {
        public EStatusVeiculo Status { get; set; }
    }
}
