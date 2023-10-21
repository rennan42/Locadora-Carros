using MediatR;

namespace LocadoraCarros.Application.Veiculos.Consultas.ListarEventos
{
    public class ListarEventosVeiculoConsulta : IRequest<IList<string>>
    {
        public string Placa { get; set; }
    }
}
