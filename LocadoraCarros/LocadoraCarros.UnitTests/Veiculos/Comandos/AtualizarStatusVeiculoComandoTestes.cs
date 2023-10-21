using FluentValidation;
using LocadoraCarros.Application.Veiculos.Comandos.AtualizarStatus;
using LocadoraCarros.Application.Veiculos.Eventos.AlterarStatusVeiculo;
using LocadoraCarros.Domain.Enum;
using LocadoraCarros.Domain.Repositorios;
using MediatR;
using Moq;

namespace LocadoraCarros.UnitTests.Veiculos.Comandos
{
    public class AtualizarStatusVeiculoComandoTestes
    {
        [Trait("Unit", "Veiculo")]
        [Fact(DisplayName = "Comando - Devera atualizar status do veículo.")]
        public async Task DeveraAtualizarStatusVeiculo()
        {
            var validatorMock = new Mock<IValidator<AtualizarStatusVeiculoComando>>();
            var veiculoRepositorioMock = new Mock<IVeiculoRepositorio>();
            var publisherMock = new Mock<IPublisher>();
            var comando = new AtualizarStatusVeiculoComando();

            veiculoRepositorioMock.Setup(r => r.AtualizarStatus(It.IsAny<string>(), It.IsAny<EStatusVeiculo>()))
                                  .Returns(Task.FromResult(true));

            publisherMock.Setup(p => p.Publish(It.IsAny<AlterarStatusVeiculoEvent>(), CancellationToken.None))
                         .Returns(Task.CompletedTask);

            var handler = new AtualizarStatusVeiculoComandoHandler(veiculoRepositorioMock.Object,  validatorMock.Object, publisherMock.Object);

            var result = await handler.Handle(comando, CancellationToken.None);

            Assert.True(result);
            Assert.IsType<bool>(result);
            veiculoRepositorioMock.Verify(r => r.AtualizarStatus(It.IsAny<string>(), It.IsAny<EStatusVeiculo>()), Times.Once);
            publisherMock.Verify(p => p.Publish(It.IsAny<AlterarStatusVeiculoEvent>(), CancellationToken.None), Times.Once);

        }

        [Trait("Unit", "Veiculo")]
        [Fact(DisplayName = "Cadastro - não devera atualizar status do veículo.")]
        public async Task NaoDeveraAtualizarStatusVeiculoPlacaInexistente()
        {
            var validatorMock = new Mock<IValidator<AtualizarStatusVeiculoComando>>();
            var veiculoRepositorioMock = new Mock<IVeiculoRepositorio>();
            var publisherMock = new Mock<IPublisher>();
            var comando = new AtualizarStatusVeiculoComando();

            veiculoRepositorioMock.Setup(r => r.AtualizarStatus(It.IsAny<string>(), It.IsAny<EStatusVeiculo>()))
                                  .Returns(Task.FromResult(false));

            publisherMock.Setup(p => p.Publish(It.IsAny<AlterarStatusVeiculoEvent>(), CancellationToken.None))
                         .Returns(Task.CompletedTask);

            var handler = new AtualizarStatusVeiculoComandoHandler(veiculoRepositorioMock.Object, validatorMock.Object, publisherMock.Object);

            var result = await handler.Handle(comando, CancellationToken.None);

            Assert.False(result);
            Assert.IsType<bool>(result);
            veiculoRepositorioMock.Verify(r => r.AtualizarStatus(It.IsAny<string>(), It.IsAny<EStatusVeiculo>()), Times.Once);
            publisherMock.Verify(p => p.Publish(It.IsAny<AlterarStatusVeiculoEvent>(), CancellationToken.None), Times.Never);

        }
    }
}
