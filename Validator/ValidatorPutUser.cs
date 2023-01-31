using FluentValidation;
using SistemaBancario.Models;
using System;
using System.Linq;

namespace SistemaBancario.Validator
{
    public class ValidatorPutUser : AbstractValidator<Usuario>
    {
        public ValidatorPutUser()
        {

            RuleFor(p => p.Nome).NotNull().NotEmpty();
            RuleFor(p => p.Cpf).NotEmpty();
            RuleFor(p => p.Email).NotNull().NotEmpty().EmailAddress();
            //RuleFor(p => p.Usuario.Email).Must(verificaDuplicidade).WithMessage("e-mail ja cadastrado");

        }



    }
}
