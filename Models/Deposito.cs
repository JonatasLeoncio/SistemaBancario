using System;
using System.Collections.Generic;

namespace SistemaBancario.Models
{
    public class Deposito
    {
        public Deposito() { }
        public Deposito(DepositoView dp)
        {
            this.Data = DateTime.Now;
            this.Numero_Conta = dp.Numero_Conta;
            this.Valor = dp.Valor;
        }

        public DateTime Data { get; set; }
        public int Numero_Conta { get; set; }
        public double Valor { get; set; }
        
    }
}
