using Bogus;
using LocadoraCarros.Domain.Entidades;
using LocadoraCarros.Domain.Enum;

namespace LocadoraCarros.Tests.Shared.Builders
{
    public class VeiculoEventoBuilder
    {
        private EAcaoVeiculoEvento? acao;
        private DateTime? data;
        private string? placa;
        public VeiculoEvento Create()
        {
            return new Faker<VeiculoEvento>()
                .RuleFor(p => p.Acao, f => acao ?? (EAcaoVeiculoEvento)f.Random.Int(0, 3))
                .RuleFor(p => p.Data, f => data ?? DateTime.Now)
                .RuleFor(p => p.PlacaVeiculo, f => placa ?? "RIO2B25")
                .Generate();
        }

        public IList<VeiculoEvento> CreateMany(int quantidade = 2)
        {
            return new Faker<VeiculoEvento>()
                .RuleFor(p => p.Acao, f => acao ?? (EAcaoVeiculoEvento)f.Random.Int(0, 3))
                .RuleFor(p => p.Data, f => data ?? DateTime.Now)
                .RuleFor(p => p.PlacaVeiculo, f => placa ?? "RIO2B25")
                .Generate(quantidade);
        }


        public VeiculoEventoBuilder ComDataCadastro(DateTime data)
        {
            this.data = data;
            return this;
        }

        public VeiculoEventoBuilder ComPlaca(string placa)
        {
            this.placa = placa;
            return this;
        }

        public VeiculoEventoBuilder ComAcao(EAcaoVeiculoEvento acao)
        {
            this.acao = acao;
            return this;
        }
    }
}
