using LocadoraCarros.Domain.Enum;

namespace LocadoraCarros.Domain.Entidades
{
    public class Veiculo : Entidade
    {
        public string Modelo { get; set; }
        public string DataCadastro { get; set; }
        public string Placa { get; set; }
        public StatusVeiculo Status { get; set;}
    }
}
