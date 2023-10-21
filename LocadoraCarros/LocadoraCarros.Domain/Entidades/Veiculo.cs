using LocadoraCarros.Domain.Enum;

namespace LocadoraCarros.Domain.Entidades
{
    public class Veiculo : Entidade
    {
        public EModeloVeiculo Modelo { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Placa { get; private set; }
        public EStatusVeiculo Status { get; private set;}

        public void AtualizarStatus(EStatusVeiculo status)
        {
            Status = status;
        }
    }
}
