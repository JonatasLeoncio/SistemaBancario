using FluentValidation;
using SistemaBancario.Models;
using System;

namespace SistemaBancario.Validator
{
    public class ValidatorTranferencia : AbstractValidator<TransferenciaView>
    {
        public ValidatorTranferencia(Conta cliente)
        {
            RuleFor(t => t.Numero_Conta_Origem).NotNull().NotEmpty().WithMessage("numero da conta de origem é obrigatorio");
            RuleFor(t => t.Numero_Conta_Destino).NotNull().NotEmpty().WithMessage("numero da conta de  Destinatario é obrigatorio");
            RuleFor(t => t.Senha).NotNull().NotEmpty().WithMessage("senha é Obrigatório");
            RuleFor(t => t.Valor).NotNull().NotEmpty().Must(verificaValor).WithMessage("Preencha um valor maior que zero");

        }

        private bool verificaValor(double valor)
        {
            if (valor > 0) { return true; } else { return false; };
        }
    }
}
