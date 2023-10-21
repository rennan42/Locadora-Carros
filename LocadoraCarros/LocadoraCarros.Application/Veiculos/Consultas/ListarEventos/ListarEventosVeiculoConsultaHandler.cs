using LocadoraCarros.Domain.Repositorios;
using MediatR;

namespace LocadoraCarros.Application.Veiculos.Consultas.ListarEventos
{
    public class ListarEventosVeiculoConsultaHandler : IRequestHandler<ListarEventosVeiculoConsulta, IList<string>>
    {
        private readonly IVeiculoEventoRepositorio _veiculoEventoRepositorio;

        public ListarEventosVeiculoConsultaHandler(IVeiculoEventoRepositorio veiculoEventoRepositorio)
        {
            _veiculoEventoRepositorio = veiculoEventoRepositorio;
        }

        public async Task<IList<string>> Handle(ListarEventosVeiculoConsulta request, CancellationToken cancellationToken)
        {
            var resposta = new List<string>();
            var eventos = await _veiculoEventoRepositorio.ListarEventos(request.Placa);

            foreach ( var evento in eventos )
            {
                resposta.Add(evento.GerarMensagemEvento());
            }

            return resposta;
        }
    }
}
