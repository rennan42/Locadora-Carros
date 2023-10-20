using LocadoraCarros.Domain.Enum;

namespace LocadoraCarros.Application.ViewModels
{
    public class VeiculoViewModel
    {
        public long Id { get; set; }
        public EModeloVeiculo Modelo { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Placa { get; set; }
        public EStatusVeiculo Status { get; set; }
    }
}
