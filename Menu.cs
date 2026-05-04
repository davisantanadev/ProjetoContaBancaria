using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaBancaria
{
    public class Menu
    {
        static ContaController controller = new ContaController();

        public static void Main(string[] args)
        {
            int opcao;

            do
            {
                ExibirMenu();

                while (true)
                {
                    Console.Write("\nDigite a opção desejada: ");
                    if (int.TryParse(Console.ReadLine(), out opcao))
                        break;
                    Cores.Erro("Digite apenas números.");
                }

                Console.Clear();

                switch (opcao)
                {
                    case 1:
                        CadastrarConta();
                        break;
                    case 2:
                        controller.listarTodas();
                        break;
                    case 3:
                        BuscarConta();
                        break;
                    case 4:
                        AtualizarConta();
                        break;
                    case 5:
                        DeletarConta();
                        break;
                    case 6:
                        RealizarSaque();
                        break;
                    case 7:
                        RealizarDeposito();
                        break;
                    case 8:
                        RealizarTransferencia();
                        break;
                    case 9:
                        Cores.Titulo("Encerrando o sistema...");
                        Cores.Sucesso("Até logo!");
                        break;
                    default:
                        Cores.Aviso("Opção inválida. Digite um número de 1 a 9.");
                        break;
                }

                if (opcao != 9)
                {
                    Console.Write("\nPressione ENTER para continuar...");
                    Console.ReadLine();
                    Console.Clear();
                }

            } while (opcao != 9);
        }

        // ─────────────────────────────────────────────
        // Menu principal
        // ─────────────────────────────────────────────
        private static void ExibirMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n" + new string('═', 50));
            Console.WriteLine("         $  BANCO MONTREAL  $");
            Console.WriteLine(new string('═', 50));
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  1  ─  Cadastrar Conta");
            Console.WriteLine("  2  ─  Listar todas as Contas");
            Console.WriteLine("  3  ─  Buscar Conta por Número");
            Console.WriteLine("  4  ─  Atualizar Conta");
            Console.WriteLine("  5  ─  Deletar Conta");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  6  ─  Sacar");
            Console.WriteLine("  7  ─  Depositar");
            Console.WriteLine("  8  ─  Transferir");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  9  ─  Sair");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(new string('═', 50));
            Console.ResetColor();
        }

        // ─────────────────────────────────────────────
        // Handlers
        // ─────────────────────────────────────────────
        private static void CadastrarConta()
        {
            Cores.Titulo("Cadastrar Nova Conta");
            Console.WriteLine("  0 - Voltar");
            Console.WriteLine("  1 - Conta Corrente");
            Console.WriteLine("  2 - Conta Poupança");
            Console.Write("\nEscolha o tipo: ");

            string tipo = Console.ReadLine()?.Trim();

            if (tipo == "0")
            {
                Cores.Aviso("Operação cancelada.");
                return;
            }

            Conta novaConta;
            switch (tipo)
            {
                case "1":
                    novaConta = new ContaCorrente();
                    novaConta.setTipo(1);
                    break;
                case "2":
                    novaConta = new ContaPoupanca();
                    novaConta.setTipo(2);
                    break;
                default:
                    Cores.Erro("Tipo inválido. Operação cancelada.");
                    return;
            }

            controller.Cadastrar(novaConta);
        }

        private static void BuscarConta()
        {
            Cores.Titulo("Buscar Conta por Número");
            int numero = LerNumero("Número da conta (0 para voltar): ", true);
            if (numero == 0) return;
            controller.procurarPorNumero(numero);
        }

        private static void AtualizarConta()
        {
            Cores.Titulo("Atualizar Conta");
            int numero = LerNumero("Número da conta a atualizar (0 para voltar): ", true);
            if (numero == 0) return;
            Conta conta = controller.buscarNaCollection(numero);
            if (conta == null)
            {
                Cores.Erro("Conta não encontrada.");
                return;
            }
            controller.Atualizar(conta);
        }

        private static void DeletarConta()
        {
            Cores.Titulo("Deletar Conta");
            int numero = LerNumero("Número da conta a deletar (0 para voltar): ", true);
            if (numero == 0) return;
            controller.Deletar(numero);
        }

        private static void RealizarSaque()
        {
            Cores.Titulo("Saque");
            int numero = LerNumero("Número da conta (0 para voltar): ", true);
            if (numero == 0) return;

            Conta conta = controller.buscarNaCollection(numero);
            if (conta == null)
            {
                Cores.Erro("Conta não encontrada.");
                return;
            }

            Console.WriteLine($"Saldo atual: R$ {conta.getSaldo():F2}");

            float valor = LerValor("Valor do saque (0 para voltar): ", true);
            if (valor == 0) return;
            controller.Sacar(numero, valor);
        }

        private static void RealizarDeposito()
        {
            Cores.Titulo("Depósito");
            int numero = LerNumero("Número da conta (0 para voltar): ", true);
            if (numero == 0) return;
            float valor = LerValor("Valor do depósito (0 para voltar): ", true);
            if (valor == 0) return;
            controller.Depositar(numero, valor);
        }

        private static void RealizarTransferencia()
        {
            Cores.Titulo("Transferência");
            int origemNum = LerNumero("Conta de origem (0 para voltar): ", true);
            if (origemNum == 0) return;
            int destinoNum = LerNumero("Conta de destino (0 para voltar): ", true);
            if (destinoNum == 0) return;

            Conta origem = controller.buscarNaCollection(origemNum);
            if (origem == null)
            {
                Cores.Erro("Conta de origem não encontrada.");
                return;
            }

            float valor;
            while (true)
            {
                valor = LerValor("Valor da transferência (0 para voltar): ", true);
                if (valor == 0) return;

                if (origem is ContaCorrente cc)
                {
                    if (valor > cc.getLimite())
                    {
                        Cores.Erro("Valor excede o limite definido para transferência. Informe um valor menor.");
                        continue;
                    }
                    if (valor > cc.getSaldo() + cc.getLimite())
                    {
                        Cores.Erro("Saldo e limite insuficientes para esta transferência.");
                        continue;
                    }
                }
                else if (origem is ContaPoupanca cp)
                {
                    if (valor > cp.getSaldo())
                    {
                        Cores.Erro("Saldo insuficiente.");
                        continue;
                    }
                }
                break;
            }
            controller.Transferir(origemNum, destinoNum, valor);
        }

        // ─────────────────────────────────────────────
        // Helpers de leitura
        // ─────────────────────────────────────────────
        private static int LerNumero(string prompt, bool permitirSair = false)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (input == "0" && permitirSair)
                    return 0;
                if (int.TryParse(input, out int numero) && numero > 0)
                    return numero;
                Cores.Erro("Valor inválido. Informe apenas números positivos.");
            }
        }

        private static float LerValor(string prompt, bool permitirSair = false)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (input == "0" && permitirSair)
                    return 0;
                if (float.TryParse(input, out float valor) && valor > 0)
                    return valor;
                Cores.Erro("Valor inválido. Informe um número maior que zero.");
            }
        }
    }
}
