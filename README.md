# SimpleDB - Banco de Dados de Chave-Valor

Este é um projeto de um banco de dados de chave-valor simples implementado em C#.

## Visão Geral

O SimpleDB é um banco de dados que armazena pares de chave e valor, permitindo operações básicas como inserção, remoção, busca e atualização de dados. Ele também inclui um mecanismo de cache baseado no algoritmo LRU (Least Recently Used - Menos Recente Utilizado) para otimizar o acesso aos dados mais frequentemente usados.

## Funcionalidades

- **Inserção de Dados**: Adicione novos pares de chave e valor ao banco de dados.
- **Remoção de Dados**: Remova pares de chave e valor existentes do banco de dados.
- **Busca de Dados**: Busque valores associados a uma chave específica no banco de dados.
- **Atualização de Dados**: Atualize os valores associados a uma chave existente no banco de dados.
- **Mecanismo de Cache LRU**: O banco de dados mantém um cache para melhorar o acesso aos dados mais utilizados.

## Requisitos

- [.NET Framework](https://dotnet.microsoft.com/download) - Certifique-se de ter o .NET Framework instalado para executar o projeto.

## Como Usar

1. **Clonar o Repositório**

   ```bash
   git clone https://github.com/seu-usuario/SimpleDB.git
Compilar e Executar

Navegue até o diretório do projeto e execute:

bash
Copy code
cd SimpleDB
dotnet run
Comandos Disponíveis

insert <key> <value>: Insere um novo par de chave e valor no banco de dados.
remove <key>: Remove um par de chave e valor do banco de dados.
search <key>: Busca o valor associado a uma chave no banco de dados.
update <key> <new_value>: Atualiza o valor associado a uma chave existente no banco de dados.
quit: Sai do cliente SimpleDB.