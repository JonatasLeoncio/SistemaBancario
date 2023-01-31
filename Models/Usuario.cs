using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaBancario.Models
{
    public class Usuario
    {       
        //[Required(ErrorMessage ="O campo nome é Obrigatório.")]
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime Data_nascimento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

    }
}
