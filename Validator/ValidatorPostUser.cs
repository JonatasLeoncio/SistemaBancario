using FluentValidation;
using SistemaBancario.Models;
using System;
using System.Linq;

namespace SistemaBancario.Validator
{
    public class ValidatorPostUser : AbstractValidator<Usuario>
    {
        public ValidatorPostUser()
        {
            // CascadeMode = CascadeMode.Stop;

            RuleFor(p => p.Nome).NotNull().NotEmpty().MinimumLength(2).MaximumLength(150);
            // RuleFor(p => p.Cpf).NotNull().NotEmpty().MinimumLength(11).MaximumLength(11);           
            // RuleFor(p => p.Data_nascimento).NotNull().NotEmpty().LessThan(DateTime.Now).GreaterThan(DateTime.Now.AddYears(-140));
            RuleFor(p => p.Email).NotNull().NotEmpty().EmailAddress();


        }

    }
}
