using LocadoraCarros.Application.ViewModels;
using MediatR;

namespace LocadoraCarros.Application.Veiculos.Consultas.BuscarPorPlaca
{
    public class BuscarVeiculoPorPlacaConsulta : IRequest<VeiculoViewModel>
    {
        public string Placa { get; set; }
    }
}
