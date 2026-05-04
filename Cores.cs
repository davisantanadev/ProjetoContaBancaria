using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaBancaria
{
    public static class Cores
    {
        // Cores de texto
        public static string Vermelho(string texto) => $"\u001b[31m{texto}\u001b[0m";
        public static string Verde(string texto) => $"\u001b[32m{texto}\u001b[0m";
        public static string Amarelo(string texto) => $"\u001b[33m{texto}\u001b[0m";
        public static string Azul(string texto) => $"\u001b[34m{texto}\u001b[0m";
        public static string Ciano(string texto) => $"\u001b[36m{texto}\u001b[0m";
        public static string Branco(string texto) => $"\u001b[37m{texto}\u001b[0m";
        public static string Negrito(string texto) => $"\u001b[1m{texto}\u001b[0m";

        // Atalhos do Console.WriteLine
        public static void Escrever(string texto, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine(texto);
            Console.ResetColor();
        }

        public static void Titulo(string texto)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine($"  {texto}");
            Console.WriteLine(new string('=', 50));
            Console.ResetColor();
        }

        public static void Sucesso(string texto)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[✓] {texto}");
            Console.ResetColor();
        }

        public static void Erro(string texto)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[X] {texto}");
            Console.ResetColor();
        }

        public static void Aviso(string texto)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[!] {texto}");
            Console.ResetColor();
        }

        public static void Info(string texto)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"    {texto}");
            Console.ResetColor();
        }

        public static void Separador()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(new string('-', 50));
            Console.ResetColor();
        }
    }
}
