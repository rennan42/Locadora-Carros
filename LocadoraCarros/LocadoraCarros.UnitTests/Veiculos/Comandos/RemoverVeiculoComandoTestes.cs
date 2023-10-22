using FluentValidation;
using LocadoraCarros.Application.Veiculos.Comandos.Remover;
using LocadoraCarros.Application.Veiculos.Eventos.AlterarStatusVeiculo;
using LocadoraCarros.Domain.Repositorios;
using MediatR;
using Moq;

namespace LocadoraCarros.UnitTests.Veiculos.Comandos
{
    public class RemoverVeiculoComandoTestes
    {
        [Trait("Unit", "Veiculo")]
        [Fact(DisplayName = "Comando - Devera remover veículo.")]
        public async Task DeveraRemoverVeiculo()
        {
            var validatorMock = new Mock<IValidator<RemoverVeiculoComando>>();
            var veiculoRepositorioMock = new Mock<IVeiculoRepositorio>();
            var publisherMock = new Mock<IPublisher>();
            var comando = new RemoverVeiculoComando();

            veiculoRepositorioMock.Setup(r => r.ConsultarPlacaPorId(It.IsAny<long>()))
                                  .Returns(Task.FromResult("RIO2A99"));

            veiculoRepositorioMock.Setup(r => r.Remover(It.IsAny<long>()))
                                  .Returns(Task.CompletedTask);

            publisherMock.Setup(p => p.Publish(It.IsAny<AlterarStatusVeiculoEvent>(), CancellationToken.None))
                         .Returns(Task.CompletedTask);

            var handler = new RemoverVeiculoComandoHandler(veiculoRepositorioMock.Object, publisherMock.Object, validatorMock.Object);

            await handler.Handle(comando, CancellationToken.None);

            veiculoRepositorioMock.Verify(r => r.ConsultarPlacaPorId(It.IsAny<long>()), Times.Once);
            veiculoRepositorioMock.Verify(r => r.Remover(It.IsAny<long>()), Times.Once);
            publisherMock.Verify(p => p.Publish(It.IsAny<AlterarStatusVeiculoEvent>(), CancellationToken.None), Times.Once);

        }
    }
}
