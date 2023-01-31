using FluentValidation;
using SistemaBancario.Models;
using System;

namespace SistemaBancario.Validator
{
    public class ValidatorDeposito : AbstractValidator<DepositoView>
    {
        public ValidatorDeposito()
        {
            RuleFor(p => p.Numero_Conta).NotNull().NotEmpty().WithMessage("numero da conta é obrigatorio");
            RuleFor(p => p.Valor).NotNull().NotEmpty().Must(verificaValor).WithMessage("Preencha um valor maior que zero");
        }

        private bool verificaValor(double valor)
        {
            if (valor > 0) { return true; } else { return false; };
        }
    }
}
