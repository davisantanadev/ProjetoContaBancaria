using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaBancaria
{
    public class ContaController : IContaRepository
    {
        private List<Conta> contas = new List<Conta>();
        private bool NomeValido(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) return false;
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
        public void procurarPorNumero(int numero)
        {
            try
            {
                foreach (Conta i in contas)
                {
                    if (i.getNumero() == numero)
                    {
                        Console.WriteLine($"Informações da Conta ({numero}) ");
                        Console.WriteLine($"Número: {i.getNumero()}");
                        Console.WriteLine($"Agência: {i.getAgencia()}");
                        string tipoDescricao = i.getTipo() == 1 ? "1 - Corrente" : i.getTipo() == 2 ? "2 - Poupança" : i.getTipo().ToString();
                        Console.WriteLine($"Tipo: {tipoDescricao}");
                        Console.WriteLine($"Titular: {i.getTiular()}");
                        if (i is ContaPoupanca)
                        {
                            ContaPoupanca p = (ContaPoupanca)i;
                            Console.WriteLine($"Aniversário do Titular: {p.getAniversario().ToString("dd/MM/yyyy")}");
                        }
                        if (i is ContaCorrente cc)
                        {
                            Console.WriteLine($"Limite: {cc.getLimite()}");
                        }
                        Console.WriteLine($"Saldo: {i.getSaldo()}");
                    }
                }
            }
            catch (Exception erroOcorrido)
            {
                Console.WriteLine("Erro:" + erroOcorrido.Message);
            }                   
        }
        public void listarTodas()
        {
            Console.WriteLine("===== Todas as contas cadastradas =====\n");
            foreach (Conta i in contas)
            {              
                Console.WriteLine($"\nNúmero: {i.getNumero()}");
                Console.WriteLine($"Agência: {i.getAgencia()}");
                string tipoDescricao = i.getTipo() == 1 ? "1 - Corrente" : i.getTipo() == 2 ? "2 - Poupança" : i.getTipo().ToString();
                Console.WriteLine($"Tipo: {tipoDescricao}");
                Console.WriteLine($"Titular: {i.getTiular()}");
                if (i is ContaPoupanca)
                {
                    ContaPoupanca p = (ContaPoupanca)i;
                    Console.WriteLine($"Aniversário do Titular: {p.getAniversario().ToString("dd/MM/yyyy")}");
                }
                if (i is ContaCorrente cc)
                {
                    Console.WriteLine($"Limite: {cc.getLimite()}");
                }
                Console.WriteLine($"Saldo: {i.getSaldo()}");
            }
        }
        public void Cadastrar(Conta conta)
        {
            int numConta = gerarNumero();
            conta.setNumero(numConta);
            Console.WriteLine($"Número da conta gerado automaticamente: {numConta}\n");

            int numAgencia;
            while (true)
            {
                Console.Write("Digite o número da agência (até 4 digitos): ");
                string entradaAgencia = Console.ReadLine();

                if(entradaAgencia.Length <= 4 && int.TryParse(entradaAgencia, out numAgencia))
                {
                    conta.setAgencia(numAgencia);
                    Console.WriteLine("Agência inserida com sucesso!\n");
                    break;
                }
                Console.WriteLine("A agência deve conter 4 digitos e não pode possuir letras. Por favor, insira corretamente.\n");
            }

            while (true)
            {
                Console.Write("Digite o nome do titular: ");
                string nomeTitular = Console.ReadLine();
                if (NomeValido(nomeTitular))
                {
                    conta.setTitular(nomeTitular);
                    Console.WriteLine("Nome do titular inserido com sucesso!\n");
                    break;
                }
                Console.WriteLine("O nome do titular não pode conter números nem caracteres especiais. Tente novamente.\n");
            }
          
            if (conta is ContaPoupanca)
            {
                ContaPoupanca poup = (ContaPoupanca)conta;
                while (true)
                {
                    Console.Write("Digite o dia de aniversário (1-31): ");
                    string entradaDia = Console.ReadLine();
                    Console.Write("Digite o mês de aniversário (1-12): ");
                    string entradaMes = Console.ReadLine();
                    Console.Write("Digite o ano de aniversário: ");
                    string entradaAno = Console.ReadLine();
                    if (int.TryParse(entradaDia, out int dia) && dia >= 1 && dia <= 31 &&
                        int.TryParse(entradaMes, out int mes) && mes >= 1 && mes <= 12 &&
                        int.TryParse(entradaAno, out int ano))
                    {
                        try
                        {
                            DateTime aniversario = new DateTime(ano, mes, dia);
                            poup.setAniversario(aniversario);
                            Console.WriteLine("Aniversário registrado.\n");
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Data inválida. Tente novamente.\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Erro! Informe números válidos para dia, mês e ano.\n");
                    }
                }
            }
            else if (conta is ContaCorrente)
            {
                ContaCorrente corr = (ContaCorrente)conta;
                while (true)
                {
                    Console.Write("Informe o limite da conta corrente (ex: 500.00): ");
                    string entradaLimite = Console.ReadLine();
                    if (entradaLimite != null && float.TryParse(entradaLimite, out float limite))
                    {
                        corr.setLimite(limite);
                        Console.WriteLine("Limite registrado.\n");
                        break;
                    }
                    Console.WriteLine("Erro! Informe um número válido para o limite.\n");
                }
            }
            contas.Add(conta);
            Console.WriteLine("Conta Cadastrada com Sucesso!");
        }
        public void Atualizar(Conta conta)
        {
            int opcao;
            do
            {
                Console.WriteLine("Opções para atualizar:\n" +
                "1 - Número da conta\n" +
                "2 - Agência\n" +
                "3 - Titular\n" +
                "4 - Aniversário do Titular\n" +
                "5 - Limite da Conta Corrente\n" +
                "6 - Sair\n");

                while (true)
                {
                    Console.Write("Digite a opção: ");
                    try
                    {
                        opcao = int.Parse(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Erro! Digite apenas números.\n");
                    }
                }

                switch (opcao)
                {
                    case 1:
                        int numeroDaContaAtualizado;
                        Console.Write("Informe o novo número da conta: ");
                        string entradaNum = Console.ReadLine();
                        if (entradaNum.Length == 9 && int.TryParse(entradaNum, out numeroDaContaAtualizado))
                        {
                            conta.setNumero(numeroDaContaAtualizado);
                            Console.WriteLine("Número da conta atualizado com sucesso!\n");
                            break;
                        }
                        Console.WriteLine("Erro! A conta deve conter 9 digitos e não pode possuir letras. Por favor, insira corretamente.\n");                      
                        break;
                    case 2:
                        int numeroDaAgenciaAtualizado;
                        Console.Write("Informe o novo número da agência: ");
                        string entradaAgencia = Console.ReadLine();

                        if (entradaAgencia.Length <= 4 && int.TryParse(entradaAgencia, out numeroDaAgenciaAtualizado))
                        {
                            conta.setAgencia(numeroDaAgenciaAtualizado);
                            Console.WriteLine("Agência atualizada com sucesso!\n");
                            break;
                        }
                        Console.WriteLine("Erro! A agência deve conter 4 digitos e não pode possuir letras. Por favor, insira corretamente.\n");
                        break;
                    case 3:
                        Console.Write("Informe o novo nome do titular: ");
                        string nomeDoTitularAtualizado = Console.ReadLine();
                        if (NomeValido(nomeDoTitularAtualizado))
                        {
                            conta.setTitular(nomeDoTitularAtualizado);
                            Console.WriteLine("Nome do titular atualizado.");
                        }
                        else
                        {
                            Console.WriteLine("Erro! O nome não pode conter números ou caracteres inválidos.");
                        }
                        break;
                    case 4:
                        if (conta is ContaPoupanca)
                        {
                            ContaPoupanca p = (ContaPoupanca)conta;
                            while (true)
                            {
                                Console.Write("Informe o novo dia de aniversário (1-31): ");
                                string entradaDia = Console.ReadLine();
                                Console.Write("Informe o novo mês de aniversário (1-12): ");
                                string entradaMes = Console.ReadLine();
                                Console.Write("Informe o novo ano de aniversário: ");
                                string entradaAno = Console.ReadLine();
                                if (int.TryParse(entradaDia, out int dia) && dia >= 1 && dia <= 31 &&
                                    int.TryParse(entradaMes, out int mes) && mes >= 1 && mes <= 12 &&
                                    int.TryParse(entradaAno, out int ano))
                                {
                                    try
                                    {
                                        DateTime aniversario = new DateTime(ano, mes, dia);
                                        p.setAniversario(aniversario);
                                        Console.WriteLine("Aniversário do titular atualizado.");
                                        break;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Data inválida. Tente novamente.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Erro! Informe números válidos para dia, mês e ano.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Operação inválida: apenas contas poupança possuem aniversário!!!");
                        }
                        break;
                    case 5:
                        if (conta is ContaCorrente)
                        {
                            ContaCorrente cc = (ContaCorrente)conta;
                            while (true)
                            {
                                Console.Write("Informe o novo limite da conta corrente: ");
                                string entradaLimite = Console.ReadLine();
                                if (entradaLimite != null && float.TryParse(entradaLimite, out float limite) && limite > 0)
                                {
                                    cc.setLimite(limite);
                                    Console.WriteLine("Limite da conta corrente atualizado com sucesso.");
                                    break;
                                }
                                Console.WriteLine("Erro! Informe um valor de limite válido, maior que zero.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Operação inválida: apenas contas corrente possuem limite.");
                        }
                        break;
                    case 6:
                        break;
                    default:
                        Console.WriteLine("Digite uma opção válida. [1 a 6]");
                        break;
                }
            } while (opcao != 6);
            Console.WriteLine("Conta atualizada no sistema.");      
        }  
        public void Sacar(int numero, float valor)
        {
            foreach (Conta c in contas)
            {
                if (c.getNumero() == numero)
                {
                    bool sucesso = c.sacar(valor);
                    Console.WriteLine(sucesso ? "Saque realizado." : "Saque falhou (saldo/limite insuficiente).");
                    return;
                }
            }
            Console.WriteLine("Conta não encontrada.");
        }
        public void Depositar(int numero, float valor)
        {
            foreach (Conta c in contas)
            {
                if (c.getNumero() == numero)
                {
                    c.depositar(valor);
                    Console.WriteLine("Depósito realizado com sucesso!");
                    return;
                }
            }
            Console.WriteLine("Conta não encontrada.");
        }
        public void Deletar(int numero)
        {
            int i = 0;
            while (i < contas.Count)
            {
                if (contas[i].getNumero() == numero)
                {
                    contas.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
            Console.WriteLine("Operação de remoção concluída no sistema!");
        }
        public void Transferir(int numeroOrigem, int numeroDestino, float valor)
        {

            Conta origem = null;
            Conta destino = null;
            foreach (Conta c in contas)
            {
                if (c.getNumero() == numeroOrigem) origem = c;
                if (c.getNumero() == numeroDestino) destino = c;
            }

            if (origem == null || destino == null)
            {
                Console.WriteLine("Conta de origem ou destino não encontrada.");
                return;
            }

            if (origem is ContaCorrente cc && valor > cc.getLimite())
            {
                Console.WriteLine("Transferência falhou: valor excede o limite definido para a conta corrente.");
                return;
            }

            if (!origem.sacar(valor))
            {
                Console.WriteLine("Transferência falhou: saldo/limite insuficiente.");
                return;
            }

            destino.depositar(valor);
            Console.WriteLine($"Transferência de R$ {valor:F2} de {numeroOrigem} para {numeroDestino} realizada.");
        }

        public int gerarNumero()
        {
            Random rnd = new Random();
            int numero;
            do
            {
                numero = rnd.Next(100_000_000, 1_000_000_000);
            }
            while (buscarNaCollection(numero) != null);
            return numero;
        }

        public Conta buscarNaCollection(int numero)
        {
            foreach (Conta c in contas)
            if (c.getNumero() == numero) return c;
            return null;
        }
    }
}
