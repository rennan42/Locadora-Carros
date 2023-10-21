using LocadoraCarros.Application.Veiculos.Eventos.AlterarStatusVeiculo;
using LocadoraCarros.Domain.Entidades;
using LocadoraCarros.Domain.Repositorios;
using LocadoraCarros.Tests.Shared.Builders;
using Moq;

namespace LocadoraCarros.UnitTests.Veiculos.Eventos
{
    public class AlterarStatusVeiculoEventTestes
    {
        [Trait("Unit", "Veiculo")]
        [Fact(DisplayName = "Evento - Devera Criar evento para o veículo.")]
        public async Task DeveraCriarEventoVeiculo()
        {
            var veiculos = new VeiculoEventoBuilder().CreateMany();
            var veiculoeventoRepositorioMock = new Mock<IVeiculoEventoRepositorio>();
            var comando = new AlterarStatusVeiculoEvent();

            veiculoeventoRepositorioMock.Setup(r => r.Adicionar(It.IsAny<VeiculoEvento>()))
                                  .Returns(Task.CompletedTask);

            var handler = new AlterarStatusVeiculoEventHandler(veiculoeventoRepositorioMock.Object);
            await handler.Handle(comando, CancellationToken.None);

            veiculoeventoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<VeiculoEvento>()), Times.Once);
        }
    }
}
