using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace NovoCaseMottu
{
    public class AdicionarMoto
    {
        public static void AdicionarNovaMoto()
        {
            int motoId = ObterMotoId();
            if (motoId == -1) return;  // Sair da operação

            int complexidade = ObterComplexidadeConserto();
            if (complexidade == -1) return;  // Sair da operação

            int tipoConserto = ObterTipoConserto();
            if (tipoConserto == -1) return;  // Sair da operação

            DateTime dataEntrada = DateTime.Now;
            string dataFormatada = dataEntrada.ToString("dd/MM/yyyy");

            // Verificar se a moto com o mesmo ID já está em conserto (tempoReal igual a NULL)
            if (MotoEmConserto(motoId))
            {
                Console.WriteLine($"A moto com ID {motoId} já está em conserto e não pode ser adicionada novamente.");
                Thread.Sleep(1500);  // Esperar 1,5 segundos
                return;
            }

            // Carregar mecânicos disponíveis
            var mecanicoId = SelecionarMecanicoDisponivel(complexidade);

            if (mecanicoId == -1)
            {
                Console.WriteLine("Erro: Nenhum mecânico disponível para essa complexidade ou superior.");
                Thread.Sleep(1500);  // Esperar 1,5 segundos
                return;
            }

            // Formatar linha para adicionar ao CSV
            string novaLinha = $"{motoId},{complexidade},{tipoConserto},NULL,{dataFormatada},{mecanicoId}";

            // Caminho para o arquivo CSV
            string caminhoCSV = "consertoDeMotos.csv";

            // Adicionar a nova linha ao CSV
            File.AppendAllText(caminhoCSV, novaLinha + Environment.NewLine);
            
            Console.Clear();  // Limpar o terminal            
            Console.WriteLine("Moto adicionada com sucesso!");
            Thread.Sleep(1500);  // Esperar 1,5 segundos
            Console.Clear();  // Limpar o terminal
        }

        private static bool MotoEmConserto(int motoId)
        {
            string caminhoCSV = "consertoDeMotos.csv";

            if (!File.Exists(caminhoCSV))
            {
                return false;
            }

            var linhas = File.ReadAllLines(caminhoCSV).Skip(1);  // Pular cabeçalho

            foreach (var linha in linhas)
            {
                var campos = linha.Split(',');
                if (int.Parse(campos[0]) == motoId && campos[3] == "NULL")
                {
                    return true;  // A moto já está em conserto
                }
            }

            return false;  // Moto não está em conserto ou foi finalizada
        }

        private static int ObterMotoId()
        {
            while (true)
            {
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
                    continue;
                }
                if (int.TryParse(inputMotoId, out int motoId) && motoId > 0)
                {
                    return motoId;
                }
                else
                {
                    Console.WriteLine("Erro: ID inválido. Tente novamente.");
                    Thread.Sleep(1500);  // Esperar 1,5 segundos
                }
            }
        }

        private static int ObterComplexidadeConserto()
        {
            while (true)
            {
                Console.Clear();  // Limpar o terminal
                Console.WriteLine("Digite a complexidade do conserto (1 a 3) ou 'sair' para cancelar:");
                string? inputComplexidade = Console.ReadLine();
                if (inputComplexidade?.ToLower() == "sair")
                {
                    ConfirmarSaida();
                    return -1;
                }
                if (string.IsNullOrEmpty(inputComplexidade))
                {
                    Console.WriteLine("Erro: Complexidade inválida. Tente novamente.");
                    Thread.Sleep(1500);  // Esperar 1,5 segundos
                    continue;
                }
                if (int.TryParse(inputComplexidade, out int complexidade) && complexidade >= 1 && complexidade <= 3)
                {
                    return complexidade;
                }
                else
                {
                    Console.WriteLine("Erro: A complexidade deve ser um número entre 1 e 3. Tente novamente.");
                    Thread.Sleep(1500);  // Esperar 1,5 segundos
                }
            }
        }

        private static int ObterTipoConserto()
        {
            while (true)
            {
                Console.Clear();  // Limpar o terminal
                Console.WriteLine("Digite o tipo de conserto (1 a 5) ou 'sair' para cancelar:");
                string? inputTipoConserto = Console.ReadLine();
                if (inputTipoConserto?.ToLower() == "sair")
                {
                    ConfirmarSaida();
                    return -1;
                }
                if (string.IsNullOrEmpty(inputTipoConserto))
                {
                    Console.WriteLine("Erro: O tipo de conserto inserido é inválido. Tente novamente.");
                    Thread.Sleep(1500);  // Esperar 1,5 segundos
                    continue;
                }
                if (int.TryParse(inputTipoConserto, out int tipoConserto) && tipoConserto >= 1 && tipoConserto <= 5)
                {
                    return tipoConserto;
                }
                else
                {
                    Console.WriteLine("Erro: O tipo de conserto deve ser um número entre 1 e 5. Tente novamente.");
                    Thread.Sleep(1500);  // Esperar 1,5 segundos
                }
            }
        }

        private static void ConfirmarSaida()
        {
            Console.WriteLine("Aguarde enquanto encerramos a operação. Nenhuma informação foi salva ou alterada.");
            Thread.Sleep(4000);  // Esperar 4 segundos
            Console.Clear();
        }

        private static int SelecionarMecanicoDisponivel(int complexidade)
        {
            // Simulação do carregamento de mecânicos
            string caminhoMecanicos = "mecanicos.csv";
            var mecanicos = File.ReadAllLines(caminhoMecanicos)
                                .Skip(1)  // Pular a linha de cabeçalho
                                .Select(linha => linha.Split(','))
                                .Select(campos => new
                                {
                                    MecanicoId = int.Parse(campos[0]),
                                    Nome = campos[1],
                                    NivelComplexidade = int.Parse(campos[4])
                                })
                                .Where(m => m.NivelComplexidade >= complexidade)  // Filtrar por complexidade igual ou superior
                                .OrderBy(m => m.NivelComplexidade)  // Ordenar para pegar o mais próximo
                                .ToList();

            string caminhoConsertos = "consertoDeMotos.csv";
            var mecanicosOcupados = File.ReadAllLines(caminhoConsertos)
                                        .Skip(1)
                                        .Select(linha => linha.Split(','))
                                        .Where(campos => campos[5] != "NULL")
                                        .Select(campos => int.Parse(campos[5]))
                                        .Distinct()
                                        .ToList();

            var mecanicosDisponiveis = mecanicos.Where(m => !mecanicosOcupados.Contains(m.MecanicoId)).ToList();

            if (mecanicosDisponiveis.Count == 0)
            {
                return -1;
            }

            Random rand = new Random();
            int indiceSorteado = rand.Next(mecanicosDisponiveis.Count);

            return mecanicosDisponiveis[indiceSorteado].MecanicoId;
        }
    }
}
