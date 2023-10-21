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

        public string GerarMensagemEvento()
        {
            string mensagemPorAcao = "";
            switch (Acao)
            {
                case EAcaoVeiculoEvento.SALVO:
                    mensagemPorAcao = $@"foi salvo dia {Data:dd//MM//yyyy}";
                    break;
                case EAcaoVeiculoEvento.DEVOLVIDO:
                    mensagemPorAcao = $@"foi devolvido dia {Data:dd//MM//yyyy}";
                    break;
                case EAcaoVeiculoEvento.LOCADO:
                    mensagemPorAcao = $@"foi locado dia {Data:dd//MM//yyyy}";
                    break;
                case EAcaoVeiculoEvento.REMOVIDO:
                    mensagemPorAcao = $@"foi removido dia {Data:dd//MM//yyyy}";
                    break;
                default:
                    break;
            }
            return $@"Veículo {PlacaVeiculo} {mensagemPorAcao}";
        }
    }
}
