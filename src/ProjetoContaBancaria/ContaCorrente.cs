using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaBancaria
{
    public class ContaCorrente : Conta
    {
        private float limite;

        public float getLimite()
        {
            return limite;
        }
        public void setLimite(float limite)
        {
            if (limite <= 0)
                throw new Exception("O limite deve ser maior que zero");
            this.limite = limite;
        }
        public override bool sacar(float valor)
        {
            if (valor <= getSaldo() + limite)
            {
                setSaldo(getSaldo() - valor);
                return true;
            }
            return false;
        }
        public void visualizar()
        {
            Console.WriteLine($"===== Conta Corrente =====");
            Console.WriteLine($"Número: {getNumero()}");
            Console.WriteLine($"Agência: {getAgencia()}");
            Console.WriteLine($"Tipo: {getTipo()}");
            Console.WriteLine($"Titular: {getTiular()}");
            Console.WriteLine($"Saldo: {getSaldo()}");
            Console.WriteLine($"Limite: {getLimite()}");
        }
    }
}
