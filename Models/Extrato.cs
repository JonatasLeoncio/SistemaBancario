using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;

namespace SistemaBancario.Models
{
    public class Extrato
    {
        public Extrato(List<Deposito> depositosAtual, List<Saque> saquesAtual, List<Transferencia> transferenciasEnviadasAtual, List<Transferencia> transferenciasRecebidasAtual)
        {
            this.depositosAtual = depositosAtual;
            this.saquesAtual = saquesAtual;
            this.transferenciasEnviadasAtual = transferenciasEnviadasAtual;
            this.transferenciasRecebidasAtual = transferenciasRecebidasAtual;
        }

        public List<Deposito> depositosAtual { get; private set; }
        public List<Saque> saquesAtual { get; private set; }
        public List<Transferencia> transferenciasEnviadasAtual { get; private set; }
        public List<Transferencia> transferenciasRecebidasAtual { get; private set; }

      static  public Extrato estratoAtual(int numerodaConta)
        {
            var dp = Banco.listarDepositos().Where(x => x.Numero_Conta == numerodaConta).ToList();
            /*       //ou
            var dp = Banco.listarDepositos().Where((x) => {
                return x.Numero_Conta == numerodaConta;
            }).ToList();*/

            var ltSaque = Banco.listarSaques().FindAll(x => x.Numero_Conta == numerodaConta);

            var transEnv = Banco.listarTransferencia().FindAll(x => x.Numero_Conta_Origem == numerodaConta);

            var transRec = Banco.listarTransferencia().FindAll(x => x.Numero_Conta_Destino == numerodaConta);
            /*   //ou
            var transRec = Banco.listarTransferencia().FindAll((x) => {
                return x.Numero_Conta_Destino == numerodaConta;
            });*/
            /* List< Transferencia> transEnv = new List<Transferencia>();
             List<Transferencia> transRec = new List<Transferencia>();

              var transacoes = Banco.listarTransferencia().Aggregate(new { transEnv, transRec }, (sum, item) =>
              {
                  if (item.Numero_Conta_Origem == numerodaConta)
                  {
                      sum.transEnv.Add(item);

                  }
                  if (item.Numero_Conta_Destino == numerodaConta)
                  {
                      sum.transRec.Add(item);
                  }
                  return sum;
              }); */
            /* List<Transferencia> transRec = new List<Transferencia>();
            List<Transferencia> transEnv = new List<Transferencia>();
            foreach (var item in Banco.listarTransferencia())
            {
                if (item.Numero_Conta_Origem == numerodaConta)
                {
                    transEnv.Add(item);
                }
                if (item.Numero_Conta_Destino == numerodaConta)
                {
                    transRec.Add(item);
                }
            }*/

            Extrato extrato = new Extrato(dp, ltSaque, transEnv, transRec);          

            /*depositosAtual = dp;
            saquesAtual = ltSaque;
            transferenciasEnviadasAtual = transEnv;
            transferenciasRecebidasAtual = transRec;*/

            return extrato;
        }
    }

}
