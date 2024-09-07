using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace NovoCaseMottu
{
    public class AtualizarConserto
    {
        public static void AtualizarTempoReal()
        {
            int motoId = ObterMotoId();
            if (motoId == -1) return;  // Sair da operação

            // Caminho para o arquivo CSV
            string caminhoCSV = "consertoDeMotos.csv";

            // Carregar todas as linhas do CSV
            var linhas = File.ReadAllLines(caminhoCSV).ToList();

            // Verificar se existe um conserto em aberto para a moto
            bool encontrado = false;
            for (int i = 1; i < linhas.Count; i++)  // Pular o cabeçalho
            {
                var campos = linhas[i].Split(',');

                if (int.Parse(campos[0]) == motoId && campos[3] == "NULL")
                {
                    // Perguntar o novo tempo real
                    int tempoReal = ObterTempoReal();
                    if (tempoReal == -1) return;  // Sair da operação

                    // Atualizar o tempo real do conserto em aberto
                    campos[3] = tempoReal.ToString();
                    linhas[i] = string.Join(",", campos);
                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("Erro: Nenhum conserto em aberto encontrado para a moto com ID especificado.");
                Thread.Sleep(1500);  // Esperar 1,5 segundos
                Console.Clear();  // Limpar o terminal
                return;
            }

            // Escrever as linhas atualizadas de volta no CSV
            File.WriteAllLines(caminhoCSV, linhas);

            Console.Clear();  // Limpar o terminal
            Console.WriteLine("Tempo do conserto atualizado com sucesso!");
            Thread.Sleep(1500);  // Esperar 1,5 segundos
            Console.Clear();  // Limpar o terminal
        }

        private static int ObterMotoId()
        {
            while (true)
            {
                Thread.Sleep(1500);  // Esperar 1,5 segundos
                Console.Clear();  // Limpar o terminal
                Console.WriteLine("Digite o ID da moto (ou 'sair' para cancelar):");
                string? inputMotoId = Console.ReadLine();
                if (inputMotoId?.ToLower() == "sair")
                {
                    ConfirmarSaida();
                    return -1;
                }
                if (string.IsNullOrEmpty(inputMotoId))
                {
                    Console.WriteLine("Erro: ID inválido. Tente novamente.");
                    Thread.Sleep(1500);  // Esperar 1,5 segundos
                    Console.Clear();  // Limpar o terminal
                    continue;
                }
                if (int.TryParse(inputMotoId, out int motoId))
                {
                    return motoId;
                }
                else
                {
                    Console.WriteLine("Erro: O ID da moto deve ser um número. Tente novamente.");
                    Thread.Sleep(1500);  // Esperar 1,5 segundos
                    Console.Clear();  // Limpar o terminal
                }
            }
        }

        private static int ObterTempoReal()
        {
            while (true)
            {
                Thread.Sleep(1500);  // Esperar 1,5 segundos
                Console.Clear();  // Limpar o terminal
                Console.WriteLine("Digite o tempo do conserto (em horas) ou 'sair' para cancelar:");
                string? inputTempoReal = Console.ReadLine();
                if (inputTempoReal?.ToLower() == "sair")
                {
                    ConfirmarSaida();
                    return -1;
                }
                if (string.IsNullOrEmpty(inputTempoReal))
                {
                    Console.WriteLine("Erro: O tempo digitado é inválido. Tente novamente.");
                    Thread.Sleep(1500);  // Esperar 1,5 segundos
                    Console.Clear();  // Limpar o terminal
                    continue;
                }
                if (int.TryParse(inputTempoReal, out int tempoReal))
                {
                    return tempoReal;
                }
                else
                {
                    Console.WriteLine("Erro: O tempo do conserto deve ser um número. Tente novamente.");
                    Thread.Sleep(1500);  // Esperar 1,5 segundos
                    Console.Clear();  // Limpar o terminal
                }
            }
        }

        private static void ConfirmarSaida()
        {
            Console.WriteLine("Aguarde enquanto encerramos a operação. Nenhuma informação foi salva ou alterada.");
            Thread.Sleep(4000);  // Esperar 4 segundos
            Console.Clear();  // Limpar o terminal
        }
    }
}
