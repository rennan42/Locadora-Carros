using LocadoraCarros.Domain.Entidades;
using LocadoraCarros.Domain.Enum;
using LocadoraCarros.Tests.Shared.Builders;

namespace LocadoraCarros.UnitTests.Domain
{
    public class VeiculoTestes
    {
        [Trait("Unit", "Veiculo")]
        [Fact(DisplayName = "Dominio - Devera atualizar status do veiculo.")]
        public void DeveraAtualizarStatusVeiculo()
        {
            const EStatusVeiculo statusAtualizado = EStatusVeiculo.ALUGADO;
            var veiculo = new VeiculoBuilder().Create();

            veiculo.AtualizarStatus(statusAtualizado);

            Assert.True(veiculo.Status == statusAtualizado);
        }
    }
}
