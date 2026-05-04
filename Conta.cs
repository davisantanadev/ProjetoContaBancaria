using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaBancaria
{
    public abstract class Conta
    {
        private int numero;
        private int agencia;
        private int tipo;
        private String titular;
        private float saldo;
        
        public int getNumero()
        {
            return numero;
        }
        public int getAgencia()
        {
            return agencia;
        }
        public int getTipo()
        {
            return tipo;
        }
        public String getTiular()
        {
            return titular;
        }
        public float getSaldo()
        {
            return saldo;
        }
        public void setNumero(int numero)
        {
            if (numero <= 0)
                throw new Exception("O número da conta não pode ser menor ou igual a zero");
            this.numero = numero;
        }
        public void setAgencia(int agencia)
        {
            if (agencia <= 0)
                throw new Exception("A agência da conta não pode ser menor ou igual a zero");
            this.agencia = agencia;
        }
        public void setTipo(int tipo)
        {
            if (tipo <= 0)
                throw new Exception("O tipo da conta não pode ser menor ou igual a zero");
            this.tipo = tipo;
        }
        public void setTitular(String titular)
        {
            if (string.IsNullOrWhiteSpace(titular))
                throw new Exception("O titular deve conter um nome.");
            if (!NomeValido(titular))
                throw new Exception("O titular não pode conter números ou caracteres inválidos.");
            this.titular = titular;
        }
        public void setSaldo(float saldo)
        {
            if (saldo < 0)
                throw new Exception("O saldo não pode ser negativo");
            this.saldo = saldo;
        }
        public abstract bool sacar(float valor);
        public void depositar(float valor)
        {
            if (valor <= 0)
                throw new Exception("O valor a ser depositado deve ser maior que zero");    
            saldo += valor;
        }
        private static bool NomeValido(string nome)
        {
            foreach (char c in nome)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                    continue;
                if (c == '-')
                    continue;
                return false;
            }
            return true;
        }
    }
}
