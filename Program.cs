using System;
using System.Text.RegularExpressions;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static FilmeRepositorio repositorio_filme = new FilmeRepositorio();

        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						ListarSeries();
						break;
					case "2":
						InserirSerie();
						break;
					case "3":
						AtualizarSerie();
						break;
					case "4":
					    if	(ExcluirSerie() == false)
                        {
							Console.Write("A Série não foi excluido !\n");
						}
						break;
					case "5":
						VisualizarSerie();
						break;
					case "6":
						ListarFilme();
						break;
					case "7":
						Console.WriteLine("Inserir nova Filme");
						Inserir_alteraFilmes(true);
						break;
					case "8":
						Console.Write("Digite o id da Filme:");
						Inserir_alteraFilmes(false, int.Parse(Console.ReadLine()));
						break;
					case "9":
						if (ExcluirFilme() == false)
						{
							Console.Write("O Filme não foi excluido !\n");
						}
						break;
					case "10":
						VisualizarFilme();
						break;
					case "C":
						Console.Clear();
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}

			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
        }

        private static bool ExcluirSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			Console.Write("Deseja Realmente excluir a Série: y - sim, n - não ");
			string ret = Console.ReadLine();

			if (ret == "y")
            {
               repositorio.Exclui(indiceSerie);
            }
			else
            {
				return false;
            }

			return true;
		}

		private static bool ExcluirFilme()
		{
			Console.Write("Digite o id da Filme: ");
			int indiceFilme = int.Parse(Console.ReadLine());

			Console.Write("Deseja Realmente excluir a Filme: y - sim, n - não ");
			string ret = Console.ReadLine();

			if (ret == "y")
			{
               repositorio_filme.Exclui(indiceFilme);
			}
			else
			{
				return false;
			}

			return true;

		}

		private static void VisualizarSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			var serie = repositorio.RetornaPorId(indiceSerie);

			Console.WriteLine(serie);
		}

		private static void VisualizarFilme()
		{
			Console.Write("Digite o id da Filme: ");
			int indiceFilme = int.Parse(Console.ReadLine());

			var filme = repositorio_filme.RetornaPorId(indiceFilme);

			Console.WriteLine(filme);
		}

		private static void AtualizarSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Série: ");
			int entradaAno;

			if (validar_numero(Console.ReadLine()))
			{
				entradaAno = int.Parse(Console.ReadLine());
			}
			else
			{
				entradaAno = 0;

			}

			Console.Write("Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine();

			Serie atualizaSerie = new Serie(id: indiceSerie,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Atualiza(indiceSerie, atualizaSerie);
		}
        private static void ListarSeries()
		{
			Console.WriteLine("Listar séries");

			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhuma série cadastrada.");
				return;
			}

			foreach (var serie in lista)
			{
                var excluido = serie.retornaExcluido();
                
				Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
			}
		}

		private static void ListarFilme()
		{
			Console.WriteLine("Listar Filmes");

			var lista = repositorio_filme.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhuma Filme cadastrada.");
				return;
			}

			foreach (var filme in lista)
			{
				var excluido = filme.retornaExcluido();

				Console.WriteLine("#ID {0}: - {1} {2}", filme.retornaId(), filme.retornaTitulo(), (excluido ? "*Excluído*" : ""));
			}
		}

		private static void InserirSerie()
		{
			Console.WriteLine("Inserir nova Série");
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Série: ");
			int entradaAno;

			if (validar_numero(Console.ReadLine()))
			{
				entradaAno = int.Parse(Console.ReadLine());
			}
			else
			{
				entradaAno = 0;

			}

			Console.Write("Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine();

			
				Serie novoSerie = new Serie(id: repositorio.ProximoId(),
											genero: (Genero)entradaGenero,
											titulo: entradaTitulo,
											ano: entradaAno,
											descricao: entradaDescricao);

				repositorio.Insere(novoSerie);
			
		}
		private static void Inserir_alteraFilmes(bool par, int indiceFilme = 0)
		{
		   
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título do Filme: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início do Filme: ");
			int entradaAno;

			if (validar_numero(Console.ReadLine()))
            {
				entradaAno = int.Parse(Console.ReadLine());
			}
			else
            {
				entradaAno = 0;

			}

		    Console.Write("Digite a Descrição do Filme: ");
			string entradaDescricao = Console.ReadLine();

			Console.Write("Digite o da Produtora do Filme: ");
			string produtora = Console.ReadLine();

			Console.Write("Digite o da Classificação do Filme: ");
			string classificao = Console.ReadLine();


			if (par)
			{
				Filme novoFirme = new Filme(id: repositorio_filme.ProximoId(),
											genero: (Genero)entradaGenero,
											titulo: entradaTitulo,
											ano: entradaAno,
											descricao: entradaDescricao,
											produtora: produtora,
											classificacao: classificao);

				repositorio_filme.Insere(novoFirme);
			}
			else
            {
				Filme atualizaFilme = new Filme(id: indiceFilme,
										    genero: (Genero)entradaGenero,
											titulo: entradaTitulo,
											ano: entradaAno,
											descricao: entradaDescricao,
											produtora: produtora,
											classificacao: classificao);
            repositorio_filme.Atualiza(indiceFilme, atualizaFilme);
			}

		}

        private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Séries ou Filmes a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar séries");
			Console.WriteLine("2- Inserir nova série");
			Console.WriteLine("3- Atualizar série");
			Console.WriteLine("4- Excluir série");
			Console.WriteLine("5- Visualizar série");
			Console.WriteLine("");
			Console.WriteLine("6- Listar Filmes");
			Console.WriteLine("7- Inserir novo Filmes");
			Console.WriteLine("8- Atualizar Filmes");
			Console.WriteLine("9- Excluir Filmes");
			Console.WriteLine("10-Visualizar Filmes");
			Console.WriteLine("C- Limpar Tela");

			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}


		private static bool validar_numero(string valor)
		{
			return Regex.IsMatch(valor, @"^[0-9]");
		}
	}
}
