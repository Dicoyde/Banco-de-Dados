# SimpleDB

SimpleDB is a basic command-line interface (CLI) program written in C# that simulates a simple key-value database. It allows users to perform operations like inserting, removing, updating, and searching for objects in the database.

## Usage

To run the program, compile and execute the `Program.cs` file in a C# environment.

Upon running, the program will prompt the user for commands. Valid commands include:

- `insert chave valor`: Inserts an object with the specified key and value.
- `remove chave`: Removes an object with the specified key.
- `search chave`: Searches for an object with the specified key and prints its value.
- `update chave novoValor`: Updates the value of an object with the specified key.
- `save caminho`: Saves the database to a file specified by `caminho`.
- `load caminho`: Loads the database from a file specified by `caminho`.
- `quit`: Exits the program.

## Examples

- Inserting an object:
Comando: insert 1 apple
Output: Inserido

- Removing an object:
Comando: remove 1
Output: Removido

- Searching for an object:
Comando: search 1
Output: apple

- Updating an object:
Comando: update 1 banana
Output: Atualizado

- Saving to a file:
Comando: save database.txt
Output: Dados salvos com sucesso!

- Loading from a file:
Comando: load database.txt
Output: Dados carregados com sucesso!

