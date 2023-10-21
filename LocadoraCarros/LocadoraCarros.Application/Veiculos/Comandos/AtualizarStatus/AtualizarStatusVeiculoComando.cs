using LocadoraCarros.Domain.Enum;
using MediatR;

namespace LocadoraCarros.Application.Veiculos.Comandos.AtualizarStatus
{
    public class AtualizarStatusVeiculoComando : IRequest<bool>
    {
        public string Placa { get; set; }
        public EStatusVeiculo Status { get; set; }
    }
}
