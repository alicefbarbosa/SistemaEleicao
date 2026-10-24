# Sistema de Eleição

Projeto desenvolvido para a disciplina de **Garantia da Qualidade de Software**, do curso de Ciência da Computação.

O objetivo é desenvolver uma aplicação de console em C# para simular um sistema de votação, utilizando testes unitários para verificar as regras de negócio.

## Funcionalidades

- Cadastro de candidatos com nome e número.
- Prevenção de números duplicados entre candidatos.
- Registro de eleitores por título ou CPF simulado.
- Prevenção de votos duplicados por eleitor.
- Registro de votos válidos.
- Contabilização de votos brancos e nulos.
- Apuração da quantidade de votos por candidato.
- Identificação do candidato vencedor e tratamento de empates.

## Tecnologias utilizadas

- C#
- .NET
- Git
- GitHub
- xUnit para testes unitários

## Estrutura do projeto

```text
SistemaEleicao/
├── SistemaEleicao.Console/
│   ├── Candidato.cs
│   ├── EleicaoManager.cs
│   ├── Eleitor.cs
│   ├── Program.cs
│   └── SistemaEleicao.Console.csproj
├── SistemaEleicao.Tests/
│   ├── UnitTest1.cs
│   └── SistemaEleicao.Tests.csproj
├── .gitignore
├── SistemaEleicao.slnx
└── README.md
```

## Pré-requisitos

Para executar o projeto, é necessário ter o SDK do .NET instalado.

## Como executar a aplicação

Clone o repositório e acesse a pasta do projeto. Em seguida, execute:

```bash
dotnet run --project SistemaEleicao.Console/SistemaEleicao.Console.csproj
```

O menu interativo permite cadastrar candidatos, registrar eleitores, realizar votações e consultar os resultados.

## Como executar os testes

Na pasta principal do projeto, execute:

```bash
dotnet test
```

Os testes unitários verificam as regras de cadastro de candidatos, registro de eleitores, prevenção de votos duplicados, contabilização dos votos e identificação do vencedor.

## Disciplina

Garantia da Qualidade de Software

Gestão e Qualidade de Software