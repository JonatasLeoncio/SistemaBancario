using Microsoft.AspNetCore.Mvc;
using SistemaBancario.Models;
using SistemaBancario.Validator;
using System;
using System.Linq;


namespace SistemaBancario.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase 
    {              

        [HttpGet("contas/listar/{senhaBanco}")]
        public ActionResult GetListarContas(string senhaBanco)
        {         

            Banco banco = new Banco();
            if (senhaBanco == banco.Senha && Request.Method == "GET")
            {
                var result = Banco.listarContas().ToList();
                return StatusCode(200, result);
            }
            return StatusCode(40, new
            {
                messagem = "Usuario não autenticado"
            });
        }
        [HttpGet("contas/saldo{numeroConta},{senhaConta}")]
        public ActionResult GetSaldo(int numeroConta, string senhaConta)
        {
            var contaCliente = Banco.listarContas().Find(contaCliente => contaCliente.NumeroConta == numeroConta);
            //int index = Banco.listarContas().IndexOf(contaCliente);
            if (contaCliente == null)
            {
                return NotFound(new { mesagem = "não existe conta com esse numero" });
            }
            if (contaCliente.Usuario.Senha != senhaConta)
            {
                return Unauthorized(new { mesagem = "Senha Incorreta" });
            }
            return Ok(new { saldo = contaCliente.Saldo });
        }

        [HttpPost("contas/criar")]
        public ActionResult PostAdicionar([FromBody] Usuario usuarioRecebido)
        {
                 
            var validator = new ValidatorPostUser();
            var results = validator.Validate(usuarioRecebido);
            if (!results.IsValid)
            {
                //return BadRequest(results.Errors);
                return BadRequest(new { results.Errors.FirstOrDefault().ErrorMessage });
            }
            if (Auxiliares.verificaDuplicidadeEmail(usuarioRecebido.Email))
            {
                return BadRequest(new { message = "ja existe cadastro com esse email" });
            }
            if (Auxiliares.verificaDuplicidadeCpf(usuarioRecebido.Cpf))
            { return BadRequest(new { message = "ja existe cadastro com esse Cpf" }); }
            var result = Banco.adicionarContas(usuarioRecebido);

            return Ok(result);
        }

        [HttpPut("contas/atualizar/{id}")]
        public ActionResult PutAtualizar([FromBody] Usuario usuarioPut, int id)
        {

            var validator = new ValidatorPutUser();
            var results = validator.Validate(usuarioPut);
            if (!results.IsValid)
            {
                return BadRequest(new { results.Errors.FirstOrDefault().ErrorMessage });
            }
            //Console.WriteLine(id);            
            var contaCliente = Banco.listarContas().Find(contaCliente => contaCliente.NumeroConta == id);
            
            if (contaCliente != null)
            {
                if (contaCliente.Usuario.Cpf != usuarioPut.Cpf)
                {
                    if (Auxiliares.verificaDuplicidadeCpf(usuarioPut.Cpf))
                    {
                        return BadRequest(new { message = "ja existe cadastro com esse Cpf." });
                    }
                }

                if (contaCliente.Usuario.Email != usuarioPut.Email)
                {
                    if (Auxiliares.verificaDuplicidadeEmail(usuarioPut.Email))
                    {
                        return BadRequest(new { message = "ja existe cadastro com esse email" });
                    }
                }

                int _index = Banco.listarContas().IndexOf(contaCliente);

                contaCliente.Usuario.Nome = usuarioPut.Nome;
                contaCliente.Usuario.Cpf = usuarioPut.Cpf;
                contaCliente.Usuario.Telefone = usuarioPut.Telefone;
                contaCliente.Usuario.Data_nascimento = usuarioPut.Data_nascimento;
                contaCliente.Usuario.Email = usuarioPut.Email;
                contaCliente.Usuario.Senha = usuarioPut.Senha;

                Banco.alterarConta(contaCliente, _index);
                return StatusCode(200, new { mensagem = "Conta alterada com Sucesso." });
            }
            else
            {
                return BadRequest(new { message = "Conta não existe" });
            }
        }

        [HttpDelete("contas/excluir/{id}")]
        public ActionResult DeleteConta(int id)
        {

            var contaCliente = Banco.listarContas().Find(contaCliente => contaCliente.NumeroConta == id);
            if (contaCliente != null)
            {
                int _index = Banco.listarContas().IndexOf(contaCliente);
                Banco.excluirConta(_index);
                return Ok(new { messagem = "Excluido" });
            }
            else
            {
                return BadRequest(new { message = "Conta não existe" });
            }

        }

        //_________________________________________Depositos       

        [HttpPost("transacoes/depositar")]
        public ActionResult PostDepositar([FromBody] DepositoView deposito)
        {
            var validator = new ValidatorDeposito();
            var results = validator.Validate(deposito);
            if (!results.IsValid)
            {
                //return BadRequest(results.Errors);
                return BadRequest(new { results.Errors.FirstOrDefault().ErrorMessage });
            }
            var contaCliente = Banco.listarContas().Find(contaCliente => contaCliente.NumeroConta == deposito.Numero_Conta);

            int _index = Banco.listarContas().IndexOf(contaCliente);
            if (contaCliente != null)
            {
                Deposito depositar = new Deposito(deposito);

                // depositar.Data = DateTime.Now;
                var resp = Banco.adicionarDeposito(depositar, _index);

                return Ok(resp);
            }
            else
            {
                Console.WriteLine("não");
                return NotFound(new { mesagem = "não existe conta com esse numero" });
            }

        }
        //___________________________________________saques       

        [HttpPost("transacoes/sacar")]
        public ActionResult PostSacar([FromBody] SaqueView saque)
        {
            var validator = new ValidatorSaque();
            var results = validator.Validate(saque);
            if (!results.IsValid)
            {
                //return BadRequest(results.Errors);
                return BadRequest(new { results.Errors.FirstOrDefault().ErrorMessage });
            }

            var contaCliente = Banco.listarContas().Find(contaCliente => contaCliente.NumeroConta == saque.Numero_Conta && contaCliente.Usuario.Senha == saque.Senha);
            if(contaCliente == null) { return NotFound(new { mesagem = "senha ou nuero da conta estão incorreto." }); }


            int _index = Banco.listarContas().IndexOf(contaCliente);
            if (contaCliente.Saldo < saque.Valor)
            {
                return BadRequest(new { message = "saldo insuficiente" });
            }
            if (contaCliente != null)
            {
                Saque sacar = new Saque(saque);
                var resp = Banco.adicionarSaque(sacar, _index);

                return Ok(new { mesagem = $"Saque de valor {sacar.Valor} na data {sacar.Data} efetuado com sucesso" });
            }
            else
            {
                Console.WriteLine("não");
                return NotFound(new { mesagem = "não existe conta com esse numero" });
            }

        }
        //---------------------------------transferencia

        [HttpPost("transacoes/Tranferir")]
        public ActionResult PostTransferir([FromBody] TransferenciaView transferir)
        {
            var contaCliente = Banco.listarContas().Find(contaCliente => contaCliente.NumeroConta == transferir.Numero_Conta_Origem && contaCliente.Usuario.Senha == transferir.Senha);
            if (contaCliente == null) { return NotFound(new { mesagem = "não existe conta de Origem com esse numero" }); }

            var contaClienteDestino = Banco.listarContas().Find(contaClienteDestino => contaClienteDestino.NumeroConta== transferir.Numero_Conta_Destino);

            if (contaClienteDestino == null) { return NotFound(new { mesagem = "não existe conta de Destino com esse numero" }); }

            var validator = new ValidatorTranferencia(contaCliente);
            var results = validator.Validate(transferir);
            if (!results.IsValid)
            {
                //return BadRequest(results.Errors);
                return BadRequest(new { results.Errors.FirstOrDefault().ErrorMessage });
            }

            if (contaCliente.Saldo < transferir.Valor)
            {
                return BadRequest(new { message = "saldo insuficiente" });
            }

            Transferencia tranferencia = new Transferencia(transferir);
            Banco.adicionarTransferencia(tranferencia);
            return Ok(new { mesagem = "transferencia efetuada com sucesso." });
        }

        //---------------------------------Extrato

        [HttpGet("transacoes/Extrato")]
        public ActionResult GetExtrato(int numerodaConta)
        {
            var contaCliente = Banco.listarContas().Find(contaCliente => contaCliente.NumeroConta == numerodaConta);

            if (contaCliente!=null)
            {
                return Ok(new { Extrato = Extrato.estratoAtual(numerodaConta) });
            }
            return NotFound(new { mesagem = "não existe conta com esse numero" });          

        }
    }
}
