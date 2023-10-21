using FluentValidation;
using LocadoraCarros.Domain.Enum;

namespace LocadoraCarros.Application.Veiculos.Comandos.AtualizarStatus
{
    public class AtualizarStatusVeiculoComandoValidador : AbstractValidator<AtualizarStatusVeiculoComando>
    {
        private readonly ICollection<EStatusVeiculo> _statusVeiculos = new List<EStatusVeiculo> { EStatusVeiculo.DISPONIVEL, EStatusVeiculo.ALUGADO };

        public AtualizarStatusVeiculoComandoValidador()
        {
            RuleFor(p => p.Status)
              .NotNull()
              .WithMessage("Campo Status é obrigatório.")
              .Must(ValidarSeStatusExiste)
              .WithMessage("Não foi possível atualizar a placa para o status informado.");
        }

        private bool ValidarSeStatusExiste(EStatusVeiculo status)
        {
            return _statusVeiculos.Contains(status);
        }
    }
}
