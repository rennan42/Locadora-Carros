using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using LocadoraCarros.Application.Veiculos.Comandos.Criar;
using LocadoraCarros.Application.Veiculos.Eventos.AlterarStatusVeiculo;
using LocadoraCarros.Application.ViewModels;
using LocadoraCarros.Domain.Entidades;
using LocadoraCarros.Domain.Repositorios;
using LocadoraCarros.Tests.Shared.Builders;
using MediatR;
using Moq;

namespace LocadoraCarros.UnitTests.Veiculos.Comandos
{
    public class CriarVeiculoComandoTestes
    {
        [Trait("Unit", "Veiculo")]
        [Fact(DisplayName = "Comando - Devera criar veículo.")]
        public async Task DeveraCriarVeiculo()
        {
            var veiculo = new VeiculoBuilder().Create();
            var validatorMock = new Mock<IValidator<CriarVeiculoComando>>();
            var veiculoRepositorioMock = new Mock<IVeiculoRepositorio>();
            var publisherMock = new Mock<IPublisher>();
            var mapperMock = new Mock<IMapper>();
            var comando = new CriarVeiculoComando();

            mapperMock.Setup(m => m.Map<Veiculo>(It.IsAny<CriarVeiculoComando>()))
            .Returns(veiculo);

            mapperMock.Setup(m => m.Map<VeiculoViewModel>(It.IsAny<Veiculo>()))
            .Returns(new VeiculoViewModel());

            veiculoRepositorioMock.Setup(r => r.Adicionar(It.IsAny<Veiculo>()))
                                  .Returns(Task.FromResult(veiculo));

            publisherMock.Setup(p => p.Publish(It.IsAny<AlterarStatusVeiculoEvent>(), CancellationToken.None))
                         .Returns(Task.CompletedTask);

            var handler = new CriarVeiculoComandoHandler(veiculoRepositorioMock.Object, mapperMock.Object, validatorMock.Object, publisherMock.Object);

            var result = await handler.Handle(comando, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<VeiculoViewModel>(result);
            veiculoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Veiculo>()), Times.Once);
            publisherMock.Verify(p => p.Publish(It.IsAny<AlterarStatusVeiculoEvent>(), CancellationToken.None), Times.Once);
            mapperMock.Verify(m => m.Map<Veiculo>(comando), Times.Once);
            mapperMock.Verify(m => m.Map<VeiculoViewModel>(veiculo), Times.Once);

        }
    }
}
