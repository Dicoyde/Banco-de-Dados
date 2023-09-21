using System;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        SimpleDB db = new SimpleDB();

        while (true)
        {
            Console.Write("Comando: ");
            string comando = Console.ReadLine();

            string[] partes = comando.Split(' ');

            if (partes.Length > 0)
            {
                string operacao = partes[0].ToLower();

                switch (operacao)
                {
                    case "insert":
                        if (partes.Length == 3 && int.TryParse(partes[1], out int chaveInsercao))
                        {
                            db.InserirObjeto(chaveInsercao, partes[2]);
                            Console.WriteLine("Inserido");
                        }
                        else
                        {
                            Console.WriteLine("Comando inválido");
                        }
                        break;

                    case "remove":
                        if (partes.Length == 2 && int.TryParse(partes[1], out int chaveRemocao))
                        {
                            db.RemoverObjeto(chaveRemocao);
                            Console.WriteLine("Removido");
                        }
                        else
                        {
                            Console.WriteLine("Comando inválido");
                        }
                        break;

                    case "search":
                        if (partes.Length == 2 && int.TryParse(partes[1], out int chaveBusca))
                        {
                            string valor = db.BuscarObjeto(chaveBusca);
                            if (valor != null)
                            {
                                Console.WriteLine(valor);
                            }
                            else
                            {
                                Console.WriteLine("Não encontrado");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Comando inválido");
                        }
                        break;

                    case "update":
                        if (partes.Length == 3 && int.TryParse(partes[1], out int chaveAtualizacao))
                        {
                            db.AtualizarObjeto(chaveAtualizacao, partes[2]);
                            Console.WriteLine("Atualizado");
                        }
                        else
                        {
                            Console.WriteLine("Comando inválido");
                        }
                        break;

                    case "save":
                        if (partes.Length == 2)
                        {
                            db.SalvarEmArquivo(partes[1]);
                            Console.WriteLine("Dados salvos com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Comando inválido");
                        }
                        break;

                    case "load":
                        if (partes.Length == 2)
                        {
                            db.CarregarDeArquivo(partes[1]);
                            Console.WriteLine("Dados carregados com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Comando inválido");
                        }
                        break;

                    case "quit":
                        return;

                    default:
                        Console.WriteLine("Comando inválido");
                        break;
                }
            }

        }
    }
}
