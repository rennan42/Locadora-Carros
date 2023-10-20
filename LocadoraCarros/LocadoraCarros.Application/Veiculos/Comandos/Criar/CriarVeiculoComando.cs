using LocadoraCarros.Application.ViewModels;
using LocadoraCarros.Domain.Enum;
using MediatR;

namespace LocadoraCarros.Application.Veiculos.Comandos.Criar
{
    public class CriarVeiculoComando : IRequest<VeiculoViewModel>
    {
        public EModeloVeiculo Modelo { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Placa { get; set; }
        public EStatusVeiculo Status { get; set; }
    }
}
