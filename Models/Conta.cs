using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SistemaBancario.Models
{
    public class Conta 
    {
        private static int cotadorDeContas = 1;
        //private static List<Conta> listagem=new List<Conta>();

        private int numeroConta;
        private double saldo=0;
        private Usuario usuario;

        public Conta( Usuario usuario)
        {
            this.NumeroConta = CotadorDeContas;           
            this.Usuario = usuario;
            CotadorDeContas += 1;
        }
        public Conta()
        {
            
        }

        public static int CotadorDeContas { get => cotadorDeContas; private set => cotadorDeContas = value; }
        public int NumeroConta { get =>numeroConta; set => numeroConta = value; }
        public double Saldo { get => saldo; set => saldo = value; }
        public Usuario Usuario { get => usuario; set => usuario = value; }
       
      

        
    }
    
}
