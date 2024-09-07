using System;

namespace NovoCaseMottu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mottu - Bem-vindo ao sistema de gerenciamento de conserto de motos!");

            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("\nEscolha uma opção:");
                Console.WriteLine("1 - Adicionar nova moto para conserto");
                Console.WriteLine("2 - Atualizar status de conserto");
                Console.WriteLine("0 - Sair");

                string? opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarMoto.AdicionarNovaMoto();
                        break;
                    case "2":
                        AtualizarConserto.AtualizarTempoReal();  // Chamar a nova funcionalidade
                        break;
                    case "0":
                        continuar = false;
                        Console.WriteLine("Saindo...");
                        Thread.Sleep(1500);  // Esperar 1,5 segundos
                        Console.Clear();  // Limpar o terminal
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        Thread.Sleep(1500);  // Esperar 1,5 segundos
                        Console.Clear();  // Limpar o terminal
                        break;
                }
            }
        }
    }
}
