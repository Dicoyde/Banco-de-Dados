# Banco de Dados de Chave-Valor

Este é um projeto de banco de dados de chave-valor desenvolvido em C# que consiste em duas partes: o banco de dados em si e um cliente para interagir com ele.

## Funcionalidades

### Banco de Dados (Parte A e B)
- Inserção de objetos no banco de dados.
- Remoção de objetos do banco de dados.
- Atualização de objetos no banco de dados.
- Pesquisa de objetos no banco de dados.
- Persistência dos objetos do banco de dados em um arquivo.
- Interface por linha de comandos para realizar operações diretas no banco de dados.
- Comunicação bidirecional com o programa cliente usando threads.
- Processamento concorrente de requisições no banco de dados.

### Cliente (Parte B)
- Comando de inserção de objetos no banco de dados.
- Comando de remoção de objetos no banco de dados.
- Comando de atualização de objetos no banco de dados.
- Comando de pesquisa de objetos no banco de dados.
- Leitura das requisições do dispositivo de entrada padrão.
- Escrita das respostas recebidas das requisições feitas ao banco de dados no dispositivo de saída padrão.

## Como Usar

1. Compile o código do banco de dados e do cliente.
2. Execute o banco de dados.
3. Execute o cliente para interagir com o banco de dados.

## Comandos do Cliente

- `insert <key> <value>` - Insere um objeto no banco de dados.
- `remove <key>` - Remove um objeto do banco de dados.
- `search <key>` - Busca um objeto no banco de dados.
- `update <key> <new_value>` - Atualiza um objeto no banco de dados.
- `quit` - Encerra a execução do cliente.

---

Este README fornece apenas uma visão geral do projeto. Consulte a documentação interna dos códigos para mais detalhes e instruções de uso.
