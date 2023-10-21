using LocadoraCarros.Application.Veiculos.Consultas.ListarEventos;
using LocadoraCarros.Domain.Repositorios;
using LocadoraCarros.Tests.Shared.Builders;
using Moq;

namespace LocadoraCarros.UnitTests.Veiculos.Consultas
{
    public class ListarEventosVeiculoConsultaTestes
    {
        [Trait("Unit", "Veiculo")]
        [Fact(DisplayName = "Consulta - Devera Consultar eventos dos veículos.")]
        public async Task DeveraConsultarEventosVeiculo()
        {
            var veiculo = new VeiculoEventoBuilder().CreateMany();
            var veiculoeventoRepositorioMock = new Mock<IVeiculoEventoRepositorio>();
            var comando = new ListarEventosVeiculoConsulta();


            veiculoeventoRepositorioMock.Setup(r => r.ListarEventos(It.IsAny<string>()))
                                  .ReturnsAsync(veiculo);

            var handler = new ListarEventosVeiculoConsultaHandler(veiculoeventoRepositorioMock.Object);
            var result = await handler.Handle(comando, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<List<string>>(result);
            veiculoeventoRepositorioMock.Verify(r => r.ListarEventos(It.IsAny<string>()), Times.Once);
        }
    }
}
