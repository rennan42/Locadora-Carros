using AutoMapper;
using LocadoraCarros.Application.Veiculos.Consultas.BuscarPorPlaca;
using LocadoraCarros.Application.ViewModels;
using LocadoraCarros.Domain.Entidades;
using LocadoraCarros.Domain.Repositorios;
using LocadoraCarros.Tests.Shared.Builders;
using Moq;

namespace LocadoraCarros.UnitTests.Veiculos.Consultas
{
    public class BuscarVeiculoPorPlacaConsultaTestes
    {
        [Trait("Unit", "Veiculo")]
        [Fact(DisplayName = "Consulta - Devera Consultar veículo por placa.")]
        public async Task DeveraBuscarVeiculoPorPlaca()
        {
            var veiculo = new VeiculoBuilder().Create();
            var veiculoRepositorioMock = new Mock<IVeiculoRepositorio>();
            var mapperMock = new Mock<IMapper>();
            var comando = new BuscarVeiculoPorPlacaConsulta();

            mapperMock.Setup(m => m.Map<VeiculoViewModel>(It.IsAny<Veiculo>()))
           .Returns(new VeiculoViewModel());

            veiculoRepositorioMock.Setup(r => r.BuscarPorPlaca(It.IsAny<string>()))
                                  .ReturnsAsync(veiculo);

            var handler = new BuscarVeiculoPorPlacaConsultaHandler(veiculoRepositorioMock.Object, mapperMock.Object);
            var result = await handler.Handle(comando, CancellationToken.None);

            veiculoRepositorioMock.Verify(r => r.BuscarPorPlaca(It.IsAny<string>()), Times.Once);
            mapperMock.Verify(m => m.Map<VeiculoViewModel>(veiculo), Times.Once);
        }
    }
}
