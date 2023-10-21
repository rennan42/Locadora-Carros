using LocadoraCarros.Domain.Enum;

namespace LocadoraCarros.Domain.Entidades
{
    public class VeiculoEvento : Entidade
    {
        public DateTime Data { get; private set; }
        public string PlacaVeiculo { get; private set; }
        public EAcaoVeiculoEvento Acao { get; private set; }

        public VeiculoEvento(DateTime data, string placaVeiculo, EAcaoVeiculoEvento acao)
        {
            Data = data;
            PlacaVeiculo = placaVeiculo;
            Acao = acao;
        }
        public VeiculoEvento()
        {
                
        }
    }
}
