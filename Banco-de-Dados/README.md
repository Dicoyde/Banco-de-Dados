# SimpleDB

O SimpleDB é um programa básico de interface de linha de comando (CLI) escrito em C# que simula um banco de dados simples de chave-valor. Ele permite aos usuários realizar operações como inserir, remover, atualizar e buscar objetos no banco de dados.

## Utilização

Para executar o programa, compile e execute o arquivo `Program.cs` em um ambiente C#.

Ao ser executado, o programa solicitará comandos ao usuário. Comandos válidos incluem:

- `inserir chave valor`: Insere um objeto com a chave e valor especificados.
- `remover chave`: Remove um objeto com a chave especificada.
- `buscar chave`: Procura por um objeto com a chave especificada e imprime o valor.
- `atualizar chave novoValor`: Atualiza o valor de um objeto com a chave especificada.
- `salvar caminho`: Salva o banco de dados em um arquivo especificado por `caminho`.
- `carregar caminho`: Carrega o banco de dados de um arquivo especificado por `caminho`.
- `sair`: Encerra o programa.

## Exemplos

- Inserindo um objeto:
Comando: inserir 1 maçã
Saída: Inserido
- Removendo um objeto:
Comando: remover 1
Saída: Removido
- Buscando por um objeto:
Comando: buscar 1
Saída: maçã
- Atualizando um objeto:
Comando: atualizar 1 banana
Saída: Atualizado
- Salvando em um arquivo:
Comando: salvar banco_de_dados.txt
Saída: Dados salvos com sucesso!
- Carregando de um arquivo:
Comando: carregar banco_de_dados.txt
Saída: Dados carregados com sucesso!
