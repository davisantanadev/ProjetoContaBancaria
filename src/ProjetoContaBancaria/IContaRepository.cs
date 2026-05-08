using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaBancaria
{
    public interface IContaRepository
    {
        void procurarPorNumero(int numero);
        void listarTodas();
        void Cadastrar(Conta conta);
        void Atualizar(Conta conta);
        void Deletar(int numero);
        void Sacar(int numero, float valor);
        void Depositar(int numero, float valor);
        void Transferir(int numeroOrigem, int numeroDestino, float valor);
    }
}
