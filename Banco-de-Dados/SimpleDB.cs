using System;
using System.Collections.Generic;

public class SimpleDB
{
    private Dictionary<int, string> database = new Dictionary<int, string>();

    public void InserirObjeto(int chave, string valor)
    {
        database[chave] = valor;
    }

    public void RemoverObjeto(int chave)
    {
        if (database.ContainsKey(chave))
        {
            database.Remove(chave);
        }
    }

    public void AtualizarObjeto(int chave, string novoValor)
    {
        if (database.ContainsKey(chave))
        {
            database[chave] = novoValor;
        }
    }

    public string BuscarObjeto(int chave)
    {
        if (database.ContainsKey(chave))
        {
            return database[chave];
        }
        return null;
    }

    public void SalvarEmArquivo(string caminho)
    {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(caminho))
        {
            foreach (var entry in database)
            {
                file.WriteLine($"{entry.Key},{entry.Value}");
            }
        }
    }

    public void CarregarDeArquivo(string caminho)
    {
        try
        {
            string[] linhas = System.IO.File.ReadAllLines(caminho);
            foreach (var linha in linhas)
            {
                string[] partes = linha.Split(',');
                if (partes.Length == 2 && int.TryParse(partes[0], out int chave))
                {
                    InserirObjeto(chave, partes[1]);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro ao carregar o arquivo: " + ex.Message);
        }
    }
}
