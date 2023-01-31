using System;

namespace SistemaBancario.Models
{
    public class TransferenciaView
    {            
        public int Numero_Conta_Origem { get; set; }
        public int Numero_Conta_Destino { get; set; }
        public double Valor { get; set; }

        public string Senha { get; set; }
    }
}
