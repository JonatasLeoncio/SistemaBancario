using System;
using System.Collections.Generic;

namespace SistemaBancario.Models
{
    public class Banco
    {
       
        public Banco()
        {
            Nome = "Cubos";
            Numero = "123";
            Agencia = "0001";
            Senha = "123";

        }
        public string Nome { get; set; }
        public string Numero { get; set; }
        public string Agencia { get; set; }
        public string Senha { get; set; }
        
        private static List<Conta> listagemContas = new List<Conta>();
        private static List<Saque> listagemSaques = new List<Saque>();
        private static List<Deposito> listagemDepositos = new List<Deposito>();
        private static List<Transferencia> listagemTransferencia = new List<Transferencia>();
        static public List<Conta> listarContas()
        {
            return listagemContas;
        }
        static public List<Deposito> listarDepositos()
        {
            return listagemDepositos;
        }
       
        static public List<Saque> listarSaques()
        {
            return listagemSaques;
        }

        static public List<Transferencia> listarTransferencia()
        {
            return listagemTransferencia;
        }

        static public Conta adicionarContas(Usuario user)
        {
            Conta conta = new Conta(user);
            listagemContas.Add(conta);
            return conta;
        }
        static public Conta alterarConta(Conta contaUsuario, int index)
        {
            listagemContas[index] = contaUsuario;
            return contaUsuario;
        }
        static public void excluirConta(int id)
        {
            Console.WriteLine(id);
            listagemContas.RemoveAt(id);

        }
        static public Deposito adicionarDeposito(Deposito depositoConta,int index)
        {           
            listagemDepositos.Add(depositoConta);
            listagemContas[index].Saldo += depositoConta.Valor;
            return depositoConta;
        }

        static public object adicionarSaque(Saque saqueConta, int index)
        {
            listagemSaques.Add(saqueConta);
            listagemContas[index].Saldo -= saqueConta.Valor;
            return saqueConta;
        }

        static public List<Transferencia> adicionarTransferencia(Transferencia transferir)
        {
            int indexOrigem = listarContas().FindIndex(contaClienteOrigem => contaClienteOrigem.NumeroConta == transferir.Numero_Conta_Origem);
            //int indexOrigem =listarContas().IndexOf(contaClienteOrigem);

           int indexDestinatario = listarContas().FindIndex(contaClienteDestinatario => contaClienteDestinatario.NumeroConta == transferir.Numero_Conta_Destino);
            //int indexDestinatario = listarContas().IndexOf(contaClienteDestinatario);

            listagemTransferencia.Add(transferir);
            listagemContas[indexOrigem].Saldo -= transferir.Valor;
            listagemContas[indexDestinatario].Saldo += transferir.Valor;
            return listagemTransferencia;
        }
    }
}
