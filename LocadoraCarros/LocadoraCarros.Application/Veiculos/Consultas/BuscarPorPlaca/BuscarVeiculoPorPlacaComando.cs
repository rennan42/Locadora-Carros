using LocadoraCarros.Application.ViewModels;
using MediatR;

namespace LocadoraCarros.Application.Veiculos.Consultas.BuscarPorPlaca
{
    public class BuscarVeiculoPorPlacaComando : IRequest<VeiculoViewModel>
    {
        public string Placa { get; set; }
    }
}
