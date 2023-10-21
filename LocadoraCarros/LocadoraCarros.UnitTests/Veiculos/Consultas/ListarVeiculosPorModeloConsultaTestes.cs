using AutoMapper;
using LocadoraCarros.Application.Veiculos.Consultas.ListarVeiculosPorModelo;
using LocadoraCarros.Application.ViewModels;
using LocadoraCarros.Domain.Entidades;
using LocadoraCarros.Domain.Enum;
using LocadoraCarros.Domain.Repositorios;
using LocadoraCarros.Tests.Shared.Builders;
using Moq;

namespace LocadoraCarros.UnitTests.Veiculos.Consultas
{
    public class ListarVeiculosPorModeloConsultaTestes
    {
        [Trait("Unit", "Veiculo")]
        [Fact(DisplayName = "Consulta - Devera Consultar veículos por modelo.")]
        public async Task DeveraConsultarVeiculosPorModelo()
        {
            var veiculo = new VeiculoBuilder().CreateMany();
            var veiculoRepositorioMock = new Mock<IVeiculoRepositorio>();
            var mapperMock = new Mock<IMapper>();
            var comando = new ListarVeiculosPorModeloConsulta();

            mapperMock.Setup(m => m.Map<IList<VeiculoViewModel>>(It.IsAny<IList<Veiculo>>()))
                      .Returns(new List<VeiculoViewModel>());

            veiculoRepositorioMock.Setup(r => r.ListarPorModelo(It.IsAny<EModeloVeiculo>()))
                                  .ReturnsAsync(veiculo);

            var handler = new ListarVeiculosPorModeloConsultaHandler(veiculoRepositorioMock.Object, mapperMock.Object);
            var result = await handler.Handle(comando, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<List<VeiculoViewModel>>(result);
            veiculoRepositorioMock.Verify(r => r.ListarPorModelo(It.IsAny<EModeloVeiculo>()), Times.Once);
            mapperMock.Verify(m => m.Map<IList<VeiculoViewModel>>(veiculo), Times.Once);
        }
    }
}
