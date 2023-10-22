using Bogus;
using LocadoraCarros.Domain.Entidades;
using LocadoraCarros.Domain.Enum;

namespace LocadoraCarros.Tests.Shared.Builders
{
    public class VeiculoBuilder
    {
        private EModeloVeiculo? modelo;
        private DateTime? dataCadastro;
        private string? placa;
        private EStatusVeiculo? status;
        public Veiculo Create()
        {
            return new Faker<Veiculo>()
                .RuleFor(p => p.Modelo, f => modelo ?? (EModeloVeiculo)f.Random.Int(0,2))
                .RuleFor(p => p.DataCadastro, f => dataCadastro ?? DateTime.Now)
                .RuleFor(p => p.Status, f => status ?? (EStatusVeiculo)f.Random.Int(0, 1))
                .RuleFor(p => p.Placa, f => placa ?? "RIO2B25")
                .Generate();
        }
        public IList<Veiculo> CreateMany(int quantidade = 2)
        {
            return new Faker<Veiculo>()
                .RuleFor(p => p.Modelo, f => modelo ?? (EModeloVeiculo)f.Random.Int(0, 2))
                .RuleFor(p => p.DataCadastro, f => dataCadastro ?? DateTime.Now)
                .RuleFor(p => p.Status, f => status ?? (EStatusVeiculo)f.Random.Int(0, 1))
                .RuleFor(p => p.Placa, f => placa ?? "RIO2B25")
                .Generate(quantidade);
        }

        public VeiculoBuilder ComModelo(EModeloVeiculo modelo)
        {
            this.modelo = modelo;
            return this;
        }

        public VeiculoBuilder ComDataCadastro(DateTime dataCadastro)
        {
            this.dataCadastro = dataCadastro;
            return this;
        }

        public VeiculoBuilder ComPlaca(string placa)
        {
            this.placa = placa;
            return this;
        }

        public VeiculoBuilder ComStatus(EStatusVeiculo status)
        {
            this.status = status;
            return this;
        }
    }
}

