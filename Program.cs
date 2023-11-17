using System;

public class KeyValueClient
{
    private KeyValueDatabase database;

    public KeyValueClient()
    {
        database = new KeyValueDatabase(10, "fifo");
    }

    public void Run()
    {
        Console.WriteLine("Welcome to SimpleDB Client!");
        Console.WriteLine("Type 'help' for available commands or 'quit' to exit.");

        string command;
        do
        {
            Console.Write("> ");
            command = Console.ReadLine();
            ProcessCommand(command);
        } while (command != "quit");
    }

    private void ProcessCommand(string command)
    {
        string[] tokens = command.Split(' ');
        string operation = tokens[0];

        switch (operation)
        {
            case "insert":
                if (tokens.Length >= 3)
                {
                    int key;
                    if (int.TryParse(tokens[1], out key))
                    {
                        string value = string.Join(" ", tokens, 2, tokens.Length - 2);
                        database.Insert(key, value);
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
                        database.Remove(keyToRemove);
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
                        string result = database.Search(keyToSearch);
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
                        database.Update(keyToUpdate, newValue);
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
            case "quit":
                Console.WriteLine("Exiting SimpleDB Client.");
                break;
            case "help":
                Console.WriteLine("Available commands:");
                Console.WriteLine("insert <key> <value> - Insert a new object into the database");
                Console.WriteLine("remove <key> - Remove an object from the database");
                Console.WriteLine("search <key> - Search for an object in the database");
                Console.WriteLine("update <key> <new_value> - Update an object in the database");
                Console.WriteLine("quit - Exit the client");
                break;
            default:
                Console.WriteLine("Invalid command. Type 'help' for available commands.");
                break;
        }
    }

    public static void Main()
    {
        KeyValueClient client = new KeyValueClient();
        client.Run();
    }
}
