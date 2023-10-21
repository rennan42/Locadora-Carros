using LocadoraCarros.Domain.Enum;
using MediatR;

namespace LocadoraCarros.Application.Veiculos.Eventos.AlterarStatusVeiculo
{
    public class AlterarStatusVeiculoEvent : INotification
    {
        public string Placa { get; set; }
        public EAcaoVeiculoEvento Acao { get; set; }
    }
}
