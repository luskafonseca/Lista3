using System;
using System.IO;

struct Data
{
    public int mes;
    public int ano;
}

struct CabecaGado
{
    public int codigo;
    public double leite;
    public double alim;
    public Data nasc;
    public char abate;
}

class Program
{
    private const int MaxGado = 100;
    private static CabecaGado[] fazenda = new CabecaGado[MaxGado];
    private static int numCabeças = 0;

    static void Main(string[] args)
    {
        char escolha;
        do
        {
            Console.WriteLine("Menu de Opções:");
            Console.WriteLine("a) Informar dados do gado");
            Console.WriteLine("b) Preencher campo abate");
            Console.WriteLine("c) Retornar a quantidade total de leite produzida");
            Console.WriteLine("d) Retornar a quantidade total de alimento consumido");
            Console.WriteLine("e) Listar animais para abate");
            Console.WriteLine("f) Salvar e carregar dados");
            Console.WriteLine("g) Sair");

            escolha = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (escolha)
            {
                case 'a':
                    LerDados();
                    break;
                case 'b':
                    PreencherCampoAbate();
                    break;
                case 'c':
                    Console.WriteLine($"Total de leite produzido por semana: {TotalLeiteProduzido()} litros");
                    break;
                case 'd':
                    Console.WriteLine($"Total de alimento consumido por semana: {TotalAlimentoConsumido()} quilos");
                    break;
                case 'e':
                    ListarAnimaisParaAbate();
                    break;
                case 'f':
                    SalvarCarregarDados();
                    break;
                case 'g':
                    Console.WriteLine("Saindo do programa...");
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        } while (escolha != 'g');
    }

    static void LerDados()
    {
        Console.WriteLine("Informe os dados das cabeças de gado:");
        Console.Write("Quantas cabeças de gado deseja cadastrar? ");
        int numCabeçasNovas = 0; // Definir um valor padrão
        bool entradaVálida = false;

        while (!entradaVálida)
        {
            string entrada = Console.ReadLine();

            if (int.TryParse(entrada, out numCabeçasNovas))
            {
                entradaVálida = true;
            }
            else
            {
                Console.WriteLine("Entrada inválida. Por favor, insira um número inteiro válido.");
            }
        }

        if (numCabeças + numCabeçasNovas > MaxGado)
        {
            Console.WriteLine("Não é possível cadastrar mais cabeças de gado, pois excede o limite.");
            return;
        }

        for (int i = numCabeças; i < numCabeças + numCabeçasNovas; i++)
        {
            Console.WriteLine($"Cabeça de gado #{i + 1}:");
            Console.Write("Código: ");
            fazenda[i].codigo = int.Parse(Console.ReadLine());
            Console.Write("Leite produzido por semana (litros): ");
            fazenda[i].leite = double.Parse(Console.ReadLine());
            Console.Write("Alimento consumido por semana (quilos): ");
            fazenda[i].alim = double.Parse(Console.ReadLine());
            Console.Write("Mês de nascimento: ");
            fazenda[i].nasc.mes = int.Parse(Console.ReadLine());
            Console.Write("Ano de nascimento: ");
            fazenda[i].nasc.ano = int.Parse(Console.ReadLine());
            fazenda[i].abate = 'N'; // Inicialmente, definimos que o gado não vai para o abate
        }

        numCabeças += numCabeçasNovas;
    }


    static void PreencherCampoAbate()
    {
        for (int i = 0; i < numCabeças; i++)
        {
            if (DateTime.Now.Year - fazenda[i].nasc.ano > 5 || fazenda[i].leite < 40)
            {
                fazenda[i].abate = 'S';
            }
        }

        Console.WriteLine("Campos de abate preenchidos.");
    }

    static double TotalLeiteProduzido()
    {
        double totalLeite = 0;
        for (int i = 0; i < numCabeças; i++)
        {
            totalLeite += fazenda[i].leite;
        }
        return totalLeite;
    }

    static double TotalAlimentoConsumido()
    {
        double totalAlimento = 0;
        for (int i = 0; i < numCabeças; i++)
        {
            totalAlimento += fazenda[i].alim;
        }
        return totalAlimento;
    }

    static void ListarAnimaisParaAbate()
    {
        Console.WriteLine("Cabeças de gado para abate:");
        for (int i = 0; i < numCabeças; i++)
        {
            if (fazenda[i].abate == 'S')
            {
                Console.WriteLine($"Cabeça de gado #{i + 1}");
            }
        }
    }

    static void SalvarCarregarDados()
    {
        Console.WriteLine("Escolha uma opção:");
        Console.WriteLine("1) Salvar dados em arquivo");
        Console.WriteLine("2) Carregar dados de arquivo");
        int escolha = int.Parse(Console.ReadLine());

        if (escolha == 1)
        {
            using (StreamWriter writer = new StreamWriter("dados.txt"))
            {
                writer.WriteLine(numCabeças);
                for (int i = 0; i < numCabeças; i++)
                {
                    writer.WriteLine($"{fazenda[i].codigo},{fazenda[i].leite},{fazenda[i].alim},{fazenda[i].nasc.mes},{fazenda[i].nasc.ano},{fazenda[i].abate}");
                }
                Console.WriteLine("Dados salvos em arquivo.");
            }
        }
        else if (escolha == 2)
        {
            if (File.Exists("dados.txt"))
            {
                using (StreamReader reader = new StreamReader("dados.txt"))
                {
                    numCabeças = int.Parse(reader.ReadLine());
                    for (int i = 0; i < numCabeças; i++)
                    {
                        string[] campos = reader.ReadLine().Split(',');
                        fazenda[i].codigo = int.Parse(campos[0]);
                        fazenda[i].leite = double.Parse(campos[1]);
                        fazenda[i].alim = double.Parse(campos[2]);
                        fazenda[i].nasc.mes = int.Parse(campos[3]);
                        fazenda[i].nasc.ano = int.Parse(campos[4]);
                        fazenda[i].abate = campos[5][0];
                    }
                    Console.WriteLine("Dados carregados do arquivo.");
                }
            }
            else
            {
                Console.WriteLine("O arquivo de dados não existe.");
            }
        }
    }
}
