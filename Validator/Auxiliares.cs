using SistemaBancario.Models;
using System;
using System.Linq;

namespace SistemaBancario.Validator
{
    public class Auxiliares
    {
       static public bool verificaDuplicidadeEmail(string email)
        {
            var contaCliente = Banco.listarContas().FindAll(contaCliente => contaCliente.Usuario.Email == email).ToList();

            if (contaCliente.Count > 0)
            {
                return true;
            }
            return false;
        }
       static public bool verificaDuplicidadeCpf(string cpf)
        {
            var contaCliente = Banco.listarContas().FindAll(contaCliente => contaCliente.Usuario.Cpf == cpf).ToList();
            if (contaCliente.Count > 0)
            {
                return true;
            }
            return false;
        }

        internal static bool verificaDuplicidadeEmail(object email)
        {
            throw new NotImplementedException();
        }
    }
}
