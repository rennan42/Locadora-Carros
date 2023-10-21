using LocadoraCarros.Domain.Enum;

namespace LocadoraCarros.Domain.Entidades
{
    public class Veiculo : Entidade
    {
        public EModeloVeiculo Modelo { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Placa { get; private set; }
        public EStatusVeiculo Status { get; private set;}

        public Veiculo()
        {
                
        }
        public Veiculo(EModeloVeiculo modelo, DateTime dataCadastro, string placa, EStatusVeiculo status)
        {
            Modelo = modelo;
            DataCadastro = dataCadastro;
            Placa = placa;
            Status = status;
        }

        public void AtualizarStatus(EStatusVeiculo status)
        {
            Status = status;
        }
    }
}
