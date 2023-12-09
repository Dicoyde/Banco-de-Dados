using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class KeyValueRecord
{
    public int Key { get; set; }
    public string Value { get; set; }
}

public class KeyValueDatabase
{
    private Dictionary<int, string> database;
    private int cacheSize;
    private List<int> cache;
    private LinkedList<int> lruList; // Lista vinculada para o LRU
    private const string filePath = "simpledb.db";
    private Dictionary<int, int> agingDictionary = new Dictionary<int, int>();

    public KeyValueDatabase(int cacheSize)
    {
        database = new Dictionary<int, string>();
        this.cacheSize = cacheSize;
        cache = new List<int>();
        lruList = new LinkedList<int>();
    }

    public void Insert(int key, string value)
    {
        if (!database.ContainsKey(key))
        {
            if (database.Count >= cacheSize)
            {
                EvictCache();
            }
            database[key] = value;
            UpdateCache(key);
            SaveToFile(key, value);
            Console.WriteLine($"inserted");
        }
        else
        {
            Console.WriteLine($"Key {key} already exists. Use update to modify.");
        }
    }

    public void Remove(int key)
    {
        if (database.ContainsKey(key))
        {
            database.Remove(key);
            RemoveFromFile(key);
            cache.Remove(key);
            lruList.Remove(key); // Remover chave da lista LRU
            agingDictionary.Remove(key); // Remover chave do dicionário de envelhecimento
            Console.WriteLine($"removed");
        }
        else
        {
            Console.WriteLine($"Key {key} does not exist.");
        }
    }

    public string Search(int key)
    {
        if (database.ContainsKey(key))
        {
            UpdateCache(key);
            return database[key];
        }
        else
        {
            return "not found";
        }
    }

    public void Update(int key, string newValue)
    {
        if (database.ContainsKey(key))
        {
            database[key] = newValue;
            UpdateCache(key);
            SaveToFile(key, newValue);
            Console.WriteLine($"updated");
        }
        else
        {
            Console.WriteLine($"Key {key} does not exist.");
        }
    }

    private void EvictCache()
    {
        int keyToRemove = lruList.First.Value; // Obtém o primeiro elemento da lista (menos recentemente usado)

        if (keyToRemove != -1)
        {
            database.Remove(keyToRemove);
            RemoveFromFile(keyToRemove);
            cache.Remove(keyToRemove);
            lruList.RemoveFirst(); // Remove o item menos recentemente usado da lista LRU
            agingDictionary.Remove(keyToRemove); // Remove a chave do dicionário de envelhecimento
        }
    }

    private void UpdateCache(int key)
    {
        if (cache.Contains(key))
        {
            cache.Remove(key);
            lruList.Remove(key); // Remover a chave da lista LRU
        }
        else
        {
            if (cache.Count >= cacheSize)
            {
                EvictCache();
            }
        }

        cache.Add(key);
        lruList.AddLast(key); // Adicionar a chave à lista LRU no final (indicando uso mais recente)
    }

    private void EvictFIFOCache()
    {
        int keyToRemove = cache[0]; // Primeiro elemento adicionado é o mais antigo (FIFO)

        if (keyToRemove != -1)
        {
            database.Remove(keyToRemove);
            RemoveFromFile(keyToRemove);
            cache.RemoveAt(0); // Remove o item mais antigo do cache (FIFO)
            agingDictionary.Remove(keyToRemove); // Remove a chave do dicionário de envelhecimento
        }
    }

    private void UpdateFIFOCache(int key)
    {
        if (cache.Contains(key))
        {
            cache.Remove(key);
        }
        else
        {
            if (cache.Count >= cacheSize)
            {
                EvictFIFOCache();
            }
        }

        cache.Add(key);
    }

    private void EvictAgingCache()
    {
        int minAge = agingDictionary.Values.Min();
        int keyToRemove = agingDictionary.First(x => x.Value == minAge).Key;

        if (keyToRemove != -1)
        {
            database.Remove(keyToRemove);
            RemoveFromFile(keyToRemove);
            cache.Remove(keyToRemove);
            agingDictionary.Remove(keyToRemove);
        }
    }

    private void UpdateAgingCache(int key)
    {
        if (cache.Contains(key))
        {
            agingDictionary[key] = 0; // Reseta a idade do item para 0 (mais recentemente usado)
        }
        else
        {
            if (cache.Count >= cacheSize)
            {
                EvictAgingCache();
            }
        }

        foreach (var item in agingDictionary.ToList())
        {
            agingDictionary[item.Key]++; // Aumenta a idade de todos os itens no cache
        }

        if (!agingDictionary.ContainsKey(key))
        {
            agingDictionary[key] = 0; // Adiciona o novo item com idade 0
        }

        cache.Add(key);
    }

    private void SaveToFile(int key, string value)
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine($"{key},{value}");
        }
    }

    private void RemoveFromFile(int key)
    {
        string[] lines = File.ReadAllLines(filePath);
        File.WriteAllLines(filePath, lines.Where(line => !line.StartsWith($"{key},")));
    }
}
