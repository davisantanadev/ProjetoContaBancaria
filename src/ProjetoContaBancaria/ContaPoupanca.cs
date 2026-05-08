using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaBancaria
{
    public class ContaPoupanca : Conta
    {
        private DateTime aniversario;
        public DateTime getAniversario()
        {
            return aniversario;
        }
        public void setAniversario(DateTime aniversario)
        {
            this.aniversario = aniversario;
        }
        public override bool sacar(float valor)
        {
            if (valor <= getSaldo())
            {
                setSaldo(getSaldo() - valor);
                return true;
            }
            return false;
        }  
        public void Visualizar()
        {
            Console.WriteLine("===== Conta Poupança =====");
            Console.WriteLine($"Número: {getNumero()}");
            Console.WriteLine($"Agência: {getAgencia()}");
            Console.WriteLine($"Titular: {getTiular()}");
            Console.WriteLine($"Aniversário do Titular: {getAniversario().ToString("dd/MM/yyyy")}");
            Console.WriteLine($"Saldo: {getSaldo()}");
        }
    }
}
