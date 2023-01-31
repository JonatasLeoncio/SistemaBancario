using System;

namespace SistemaBancario.Models
{
    public class Transferencia
    {
        public Transferencia(TransferenciaView transferencia)
        {
            Data = DateTime.Now;
            Numero_Conta_Origem = transferencia.Numero_Conta_Origem;
            Numero_Conta_Destino = transferencia.Numero_Conta_Destino;

            Senha = transferencia.Senha;
            
            Valor = transferencia.Valor;
        }

        public DateTime Data { get; set; }
        public int Numero_Conta_Origem { get; set; }
        public int Numero_Conta_Destino { get; set; }

        public string Senha { get; set; }
        public double Valor { get; set; }
    }
}
