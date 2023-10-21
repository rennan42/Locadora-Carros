using FluentValidation;
using LocadoraCarros.Domain.Enum;
using LocadoraCarros.Domain.Repositorios;

namespace LocadoraCarros.Application.Veiculos.Comandos.Remover
{
    public class RemoverVeiculoComandoValidador : AbstractValidator<RemoverVeiculoComando>
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;

        public RemoverVeiculoComandoValidador(IVeiculoRepositorio veiculoRepositorio)
        {
            _veiculoRepositorio = veiculoRepositorio;

            RuleFor(p => p.Id)
                .MustAsync(ValidarPlacaEStatus)
                .WithMessage("Não foi possível continuar com a remoção deste veículo.");
        }

        private async Task<bool> ValidarPlacaEStatus(long id, CancellationToken token)
        {
            var veiculo = await _veiculoRepositorio.ObterPorId(id);

            return veiculo.Status != EStatusVeiculo.ALUGADO || DateTime.Now >= veiculo.DataCadastro.AddDays(15);
        }
    }
}
