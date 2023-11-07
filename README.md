# API .NET de Gerenciamento de Produtos

Este projeto é uma API .NET desenvolvida para gerenciar produtos em um mercado. A aplicação oferece operações CRUD (Create, Read, Update, Delete) para manipular os produtos no banco de dados. A arquitetura Clean Architecture é usada para garantir uma separação clara de responsabilidades, incluindo camadas Application, Domain, Data e IoC. O código segue princípios de Clean Code para manutenção e legibilidade.

## Tecnologias Utilizadas

- Linguagem: C#
- Banco de Dados: SQL Server
- ORM: Entity Framework Core
- Mapeamento de Objetos: AutoMapper
- Validação de Dados: Fluent Validation
- Autenticação: JWT Bearer
- Testes de Unidade: xUnit
- Arquitetura: Clean Architecture (Camadas Application, Domain, Data, IoC)
- Princípios: Clean Code

## Funcionalidades

- Cadastro, leitura, atualização e exclusão de produtos.
- Validação rigorosa dos dados de entrada usando Fluent Validation.
- Autenticação segura com JWT Bearer.
- Separação clara de camadas para facilitar a manutenção e o teste de cada componente.

## Pré-requisitos

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- SQL Server instalado e configurado

## Como Executar o Projeto

1. Clone o repositório:

   ```shell
   git clone https://github.com/abimaeldcm/ProjArquiteturaLimpa.git
   cd seu-projeto
