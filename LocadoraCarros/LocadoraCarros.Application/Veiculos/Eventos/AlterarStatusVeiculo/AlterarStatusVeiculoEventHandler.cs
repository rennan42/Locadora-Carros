using LocadoraCarros.Domain.Entidades;
using LocadoraCarros.Domain.Repositorios;
using MediatR;

namespace LocadoraCarros.Application.Veiculos.Eventos.AlterarStatusVeiculo
{
    public class AlterarStatusVeiculoEventHandler : INotificationHandler<AlterarStatusVeiculoEvent>
    {
        private readonly IVeiculoEventoRepositorio _veiculoEventoRepositorio;

        public AlterarStatusVeiculoEventHandler(IVeiculoEventoRepositorio veiculoEventoRepositorio)
        {
            _veiculoEventoRepositorio = veiculoEventoRepositorio;
        }

        public async Task Handle(AlterarStatusVeiculoEvent notification, CancellationToken cancellationToken)
        {
            var evento = new VeiculoEvento(DateTime.Now, notification.Placa, notification.Acao);

            await _veiculoEventoRepositorio.Adicionar(evento);
        }
    }
}
