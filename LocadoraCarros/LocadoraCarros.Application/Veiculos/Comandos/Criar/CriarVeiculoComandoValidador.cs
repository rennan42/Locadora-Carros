using FluentValidation;
using LocadoraCarros.Domain.Enum;
using System.Text.RegularExpressions;

namespace LocadoraCarros.Application.Veiculos.Comandos.Criar
{
    public class CriarVeiculoComandoValidador : AbstractValidator<CriarVeiculoComando>
    {
        private readonly ICollection<EModeloVeiculo> _modelosVeiculos = new List<EModeloVeiculo> {EModeloVeiculo.HATCH, EModeloVeiculo.SEDAN, EModeloVeiculo.SUV };
        private readonly ICollection<EStatusVeiculo> _statusVeiculos = new List<EStatusVeiculo> { EStatusVeiculo.DISPONIVEL, EStatusVeiculo.ALUGADO };
        private const string pattern = @"^[A-Z]{3}\d{1}[A-Z]{1}\d{2}$";
        public CriarVeiculoComandoValidador()
        {
            RuleFor(p => p.Modelo)
                .NotEmpty()
                .WithMessage("Campo Modelo é obrigatório.")
                .Must(ValidarSeModeloExiste)
                .WithMessage("Modelo não permitido, escolha um modelo válido: 1-Hatch, 2-Sedan, 3-SUV.");

            RuleFor(p => p.Placa)
                .NotNull()
                .WithMessage("Campo Placa é obrigatório.")
                .Must(ValidarFormatoPlaca)
                .WithMessage("Formato da placa inválido.");

            RuleFor(p => p.DataCadastro)
                .NotNull()
                .WithMessage("Campo Data é obrigatório.")
                .Must(ValidarInferiorDataAtual)
                .WithMessage("Data não pode ser inferior a data atual.");

            RuleFor(p => p.Status)
                .NotEmpty()
                .WithMessage("Campo Status é obrigatório.")
                .Must(ValidarSeStatusExiste)
                .WithMessage("Status não permitido, escolha um modelo válido: 1-Disponivel, 2-Alugado.");

        }

        private bool ValidarSeStatusExiste(EStatusVeiculo status)
        {
            return _statusVeiculos.Contains(status);

        }

        private bool ValidarInferiorDataAtual(DateTime data)
        {
            return data > DateTime.Now;
        }

        private bool ValidarFormatoPlaca(string placa)
        {
            return Regex.IsMatch(placa, pattern);
        }

        private bool ValidarSeModeloExiste(EModeloVeiculo modelo)
        {
            return _modelosVeiculos.Contains(modelo);
        }
    }
}
