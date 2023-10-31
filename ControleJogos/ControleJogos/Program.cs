using System;
using System.Linq;

struct Data
{
    public DateTime Date;
    public string PersonName;
    public char IsBorrowed;
}

class Jogo
{
    public string Titulo { get; set; }
    public string Console { get; set; }
    public int Ano { get; set; }
    public int Ranking { get; set; }
    public Data Emprestimo { get; set; }
}

class Program
{
    static Jogo[] jogos;

    static void Main(string[] args)
    {
        Console.WriteLine("Bem-vindo ao sistema de controle de coleções de jogos!");

        Console.Write("Quantos jogos deseja catalogar? ");
        int n = int.Parse(Console.ReadLine());

        jogos = new Jogo[n];

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Jogo {i + 1}:");
            jogos[i] = LerJogo();
        }

        while (true)
        {
            Console.WriteLine("\nOpções:");
            Console.WriteLine("1. Procurar jogo por título");
            Console.WriteLine("2. Listar jogos de um console");
            Console.WriteLine("3. Realizar empréstimo");
            Console.WriteLine("4. Devolver jogo");
            Console.WriteLine("5. Mostrar jogos emprestados");
            Console.WriteLine("6. Sair");
            Console.Write("Escolha uma opção: ");


            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    ProcurarJogoPorTitulo();
                    break;
                case 2:
                    ListarJogosDeConsole();
                    break;
                case 3:
                    RealizarEmprestimo();
                    break;
                case 4:
                    DevolverJogo();
                    break;
                case 5:
                    MostrarJogosEmprestados();
                    break;
                case 6:
                    Console.WriteLine("Obrigado por usar o sistema. Adeus!");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    static Jogo LerJogo()
    {
        Jogo jogo = new Jogo();
        Console.Write("Título (até 30 letras): ");
        jogo.Titulo = Console.ReadLine();
        Console.Write("Console (até 15 letras): ");
        jogo.Console = Console.ReadLine();
        Console.Write("Ano: ");
        jogo.Ano = int.Parse(Console.ReadLine());
        Console.Write("Ranking: ");
        jogo.Ranking = int.Parse(Console.ReadLine());
        jogo.Emprestimo = new Data
        {
            Date = DateTime.MinValue,
            PersonName = "",
            IsBorrowed = 'N'
        };
        return jogo;
    }

    static void ProcurarJogoPorTitulo()
    {
        Console.Write("Digite o título do jogo: ");
        string titulo = Console.ReadLine();
        var jogoEncontrado = jogos.FirstOrDefault(j => j.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
        if (jogoEncontrado != null)
        {
            Console.WriteLine("Jogo encontrado:");
            MostrarDetalhesJogo(jogoEncontrado);
        }
        else
        {
            Console.WriteLine("Jogo não encontrado.");
        }
    }

    static void ListarJogosDeConsole()
    {
        Console.Write("Digite o console: ");
        string console = Console.ReadLine();
        var jogosDoConsole = jogos.Where(j => j.Console.Equals(console, StringComparison.OrdinalIgnoreCase)).ToList();
        if (jogosDoConsole.Count > 0)
        {
            Console.WriteLine($"Jogos no console '{console}':");
            foreach (var jogo in jogosDoConsole)
            {
                MostrarDetalhesJogo(jogo);
            }
        }
        else
        {
            Console.WriteLine($"Nenhum jogo encontrado para o console '{console}'.");
        }
    }

    static void RealizarEmprestimo()
    {
        Console.Write("Digite o título do jogo a ser emprestado: ");
        string titulo = Console.ReadLine();
        var jogoEncontrado = jogos.FirstOrDefault(j => j.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
        if (jogoEncontrado != null)
        {
            if (jogoEncontrado.Emprestimo.IsBorrowed == 'N')
            {
                Console.Write("Nome da pessoa que pegou o jogo: ");
                string pessoa = Console.ReadLine();
                jogoEncontrado.Emprestimo = new Data
                {
                    Date = DateTime.Now,
                    PersonName = pessoa,
                    IsBorrowed = 'S'
                };
                Console.WriteLine("Empréstimo realizado com sucesso!");
            }
            else
            {
                Console.WriteLine("O jogo já está emprestado.");
            }
        }
        else
        {
            Console.WriteLine("Jogo não encontrado.");
        }
    }


    static void DevolverJogo()
    {
        Console.Write("Digite o título do jogo a ser devolvido: ");
        string titulo = Console.ReadLine();
        var jogoEncontrado = jogos.FirstOrDefault(j => j.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
        if (jogoEncontrado != null)
        {
            if (jogoEncontrado.Emprestimo.IsBorrowed == 'S')
            {
                jogoEncontrado.Emprestimo = new Data
                {
                    Date = DateTime.MinValue,
                    PersonName = "",
                    IsBorrowed = 'N'
                };
                Console.WriteLine("Jogo devolvido com sucesso!");
            }
            else
            {
                Console.WriteLine("O jogo não está emprestado.");
            }
        }
        else
        {
            Console.WriteLine("Jogo não encontrado.");
        }
    }


    static void MostrarJogosEmprestados()
    {
        var jogosEmprestados = jogos.Where(j => j.Emprestimo.IsBorrowed == 'S').ToList();
        if (jogosEmprestados.Count > 0)
        {
            Console.WriteLine("Jogos emprestados:");
            foreach (var jogo in jogosEmprestados)
            {
                Console.WriteLine($"Título: {jogo.Titulo}, Pessoa: {jogo.Emprestimo.PersonName}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum jogo está emprestado no momento.");
        }
    }

    static void MostrarDetalhesJogo(Jogo jogo)
    {
        Console.WriteLine($"Título: {jogo.Titulo}");
        Console.WriteLine($"Console: {jogo.Console}");
        Console.WriteLine($"Ano: {jogo.Ano}");
        Console.WriteLine($"Ranking: {jogo.Ranking}");
        string emprestimoStatus = jogo.Emprestimo.IsBorrowed == 'S' ? "Sim" : "Não";
        Console.WriteLine($"Empréstimo: {emprestimoStatus}");
        if (jogo.Emprestimo.IsBorrowed == 'S')
        {
            Console.WriteLine($"Data de Empréstimo: {jogo.Emprestimo.Date}");
            Console.WriteLine($"Pessoa que pegou o jogo: {jogo.Emprestimo.PersonName}");
        }
    }
}

