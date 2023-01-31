using System;

namespace SistemaBancario.Models
{
    public class Saque
    {
        public Saque(SaqueView saque)
        {
            this.Data = DateTime.Now;
            this.Numero_Conta = saque.Numero_Conta;
            this.Valor = saque.Valor;
            this.Senha = saque.Senha;

        }

        public DateTime Data { get; set; }
        public int Numero_Conta { get; set; }
        public double Valor { get; set; }
        public string Senha { get; set; }
    }
}
