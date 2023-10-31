using System;
using System.Collections.Generic;

// Definição da estrutura para armazenar dados de livros
struct Livro
{
    public string Titulo;
    public string Autor;
    public int Ano;
    public int Prateleira;
}

class Program
{
    static List<Livro> biblioteca = new List<Livro>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1. Cadastrar livro");
            Console.WriteLine("2. Buscar livro por título");
            Console.WriteLine("3. Mostrar todos os livros");
            Console.WriteLine("4. Livros mais novos que um ano");
            Console.WriteLine("5. Sair");

            int escolha = int.Parse(Console.ReadLine());

            switch (escolha)
            {
                case 1:
                    CadastrarLivro();
                    break;
                case 2:
                    BuscarLivroPorTitulo();
                    break;
                case 3:
                    MostrarTodosLivros();
                    break;
                case 4:
                    LivrosMaisNovosQueAno();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    static void CadastrarLivro()
    {
        Console.WriteLine("Digite o título do livro (até 30 letras):");
        string titulo = Console.ReadLine();

        // Garanta que o título tenha no máximo 30 caracteres
        if (titulo.Length > 30)
        {
            titulo = titulo.Substring(0, 30);
        }

        Console.WriteLine("Digite o autor do livro (até 15 letras):");
        string autor = Console.ReadLine();

        // Garanta que o autor tenha no máximo 15 caracteres
        if (autor.Length > 15)
        {
            autor = autor.Substring(0, 15);
        }

        Console.WriteLine("Digite o ano do livro:");
        int ano = int.Parse(Console.ReadLine());

        Console.WriteLine("Digite a prateleira do livro:");
        int prateleira = int.Parse(Console.ReadLine());

        Livro livro = new Livro
        {
            Titulo = titulo,
            Autor = autor,
            Ano = ano,
            Prateleira = prateleira
        };

        biblioteca.Add(livro);
        Console.WriteLine("Livro cadastrado com sucesso!");
    }


    static void BuscarLivroPorTitulo()
    {
        Console.WriteLine("Digite o título do livro que deseja buscar:");
        string tituloBusca = Console.ReadLine();

        bool encontrado = false;

        foreach (var livro in biblioteca)
        {
            if (livro.Titulo.ToLower().Contains(tituloBusca.ToLower()))
            {
                Console.WriteLine($"Livro: {livro.Titulo}");
                Console.WriteLine($"Prateleira: {livro.Prateleira}");
                encontrado = true;
            }
        }

        if (!encontrado)
        {
            Console.WriteLine("Livro não encontrado.");
        }
    }

    static void MostrarTodosLivros()
    {
        Console.WriteLine("Lista de todos os livros na biblioteca:");
        foreach (var livro in biblioteca)
        {
            Console.WriteLine($"Título: {livro.Titulo}");
            Console.WriteLine($"Autor: {livro.Autor}");
            Console.WriteLine($"Ano: {livro.Ano}");
            Console.WriteLine($"Prateleira: {livro.Prateleira}");
            Console.WriteLine();
        }
    }

    static void LivrosMaisNovosQueAno()
    {
        Console.WriteLine("Digite o ano a partir do qual deseja listar os livros:");
        int anoLimite = int.Parse(Console.ReadLine());

        Console.WriteLine($"Livros mais novos que {anoLimite}:");
        foreach (var livro in biblioteca)
        {
            if (livro.Ano > anoLimite)
            {
                Console.WriteLine($"Título: {livro.Titulo}");
                Console.WriteLine($"Ano: {livro.Ano}");
                Console.WriteLine();
            }
        }
    }
}
