using Bogus.DataSets;
using LocadoraCarros.Domain.Enum;
using LocadoraCarros.Tests.Shared.Builders;

namespace LocadoraCarros.UnitTests.Domain
{
    public class VeiculoEventoTestes
    {
        [Trait("Unit", "VeiculoEvento")]
        [Fact(DisplayName = "Dominio - Devera criar um evento do veiculo.")]
        public void DeveraGerarMensagemEvento()
        {
            var data = DateTime.Now;
            var mensagemEsperada = $@"Veículo RIO2B99 foi salvo dia {data:dd//MM//yyyy}";
            var evento = new VeiculoEventoBuilder()
                                .ComPlaca("RIO2B99")
                                .ComAcao(EAcaoVeiculoEvento.SALVO)
                                .Create();

            var mensagem = evento.GerarMensagemEvento();

            Assert.True(mensagem == mensagemEsperada);
        }
    }
}
