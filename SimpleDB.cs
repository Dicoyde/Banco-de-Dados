using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class KeyValueRecord
{
    public int Key { get; set; }
    public string Value { get; set; }
}

public class KeyValueDatabase
{
    private Dictionary<int, string> database;
    private int cacheSize;
    private string cacheReplacementPolicy;
    private List<int> cache;
    private const string filePath = "simpledb.db";
    private BlockingCollection<string> messageQueue;
    private CancellationTokenSource cancellationTokenSource;

    public KeyValueDatabase(int cacheSize, string cacheReplacementPolicy)
    {
        database = new Dictionary<int, string>();
        this.cacheSize = cacheSize;
        this.cacheReplacementPolicy = cacheReplacementPolicy;
        cache = new List<int>();
        messageQueue = new BlockingCollection<string>();
        cancellationTokenSource = new CancellationTokenSource();

        Task.Factory.StartNew(ProcessMessages, cancellationTokenSource.Token);
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
        int keyToRemove = -1;
        switch (cacheReplacementPolicy)
        {
            case "fifo":
                keyToRemove = cache.First();
                break;
            case "aging":
                // Implement aging policy logic here
                break;
            case "lru":
                keyToRemove = cache.First();
                break;
            default:
                break;
        }

        if (keyToRemove != -1)
        {
            database.Remove(keyToRemove);
            RemoveFromFile(keyToRemove);
            cache.Remove(keyToRemove);
        }
    }

    private void UpdateCache(int key)
    {
        if (cache.Contains(key))
        {
            cache.Remove(key);
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

    private void ProcessMessages()
    {
        foreach (var message in messageQueue.GetConsumingEnumerable())
        {
            string[] tokens = message.Split(' ');
            string command = tokens[0];

            switch (command)
            {
                case "insert":
                    if (tokens.Length >= 3)
                    {
                        int key;
                        if (int.TryParse(tokens[1], out key))
                        {
                            string value = string.Join(" ", tokens, 2, tokens.Length - 2);
                            Insert(key, value);
                        }
                        else
                        {
                            Console.WriteLine("Invalid key. Please provide a valid integer key.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Usage: insert <key> <value>");
                    }
                    break;
                case "remove":
                    if (tokens.Length == 2)
                    {
                        int keyToRemove;
                        if (int.TryParse(tokens[1], out keyToRemove))
                        {
                            Remove(keyToRemove);
                        }
                        else
                        {
                            Console.WriteLine("Invalid key. Please provide a valid integer key.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Usage: remove <key>");
                    }
                    break;
                case "search":
                    if (tokens.Length == 2)
                    {
                        int keyToSearch;
                        if (int.TryParse(tokens[1], out keyToSearch))
                        {
                            string result = Search(keyToSearch);
                            Console.WriteLine(result);
                        }
                        else
                        {
                            Console.WriteLine("Invalid key. Please provide a valid integer key.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Usage: search <key>");
                    }
                    break;
                case "update":
                    if (tokens.Length >= 3)
                    {
                        int keyToUpdate;
                        if (int.TryParse(tokens[1], out keyToUpdate))
                        {
                            string newValue = string.Join(" ", tokens, 2, tokens.Length - 2);
                            Update(keyToUpdate, newValue);
                        }
                        else
                        {
                            Console.WriteLine("Invalid key. Please provide a valid integer key.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Usage: update <key> <new_value>");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }
    }

    public void StopDatabase()
    {
        cancellationTokenSource.Cancel();
    }
}
