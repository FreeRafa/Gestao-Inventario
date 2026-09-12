# 📦 GestaoInventario

Aplicação de consola para gestão de inventário e stock, construída com **C# / .NET** e **Entity Framework Core**. 
Permite gerir categorias, fornecedores e produtos, registar movimentos de stock (entradas, saídas e ajustes) e consultar relatórios de stock baixo.

---

## 📋 Índice

- [Sobre o Projeto](#sobre-o-projeto)
- [Tecnologias](#tecnologias)
- [Arquitetura](#arquitetura)
- [Modelos de Dados](#modelos-de-dados)
- [Decisões de Modelação](#decisões-de-modelação)
- [Funcionalidades](#funcionalidades)
- [Como Executar](#como-executar)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Notas de Desenvolvimento](#notas-de-desenvolvimento)

---

## Sobre o Projeto

O **GestaoInventario** é uma aplicação de consola que permite gerir o stock de uma pequena empresa: categorias de produtos, fornecedores, produtos e o histórico de movimentos de stock associado a cada um.

O projeto utiliza **Entity Framework Core** como ORM, com todas as configurações de mapeamento feitas via `IEntityTypeConfiguration`, sem recurso a *Data Annotations* nas entidades.

Este é o segundo de uma série de projetos de treino pessoal focados em consolidar padrões de Clean Architecture em C#, antes de avançar para um projeto de maior escala.

---

## Tecnologias

- [.NET](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) com `Microsoft.EntityFrameworkCore.SqlServer`
- SQL Server
- `Microsoft.Extensions.DependencyInjection (ServiceCollection) para injeção de dependências
- Padrão Repository + Service Layer

---

## Arquitetura

O projeto segue uma arquitetura em camadas, com nomenclatura em português europeu:

```
┌─────────────────────────────────────────────────────────┐
│                  Apresentacao / Menu                     │  ← Interface de consola
├─────────────────────────────────────────────────────────┤
│                       Servico                             │  ← Lógica de negócio
├─────────────────────────────────────────────────────────┤
│                    Infraestrutura                         │  ← Repositórios (acesso a dados)
├─────────────────────────────────────────────────────────┤
│                Entity Framework Core                      │  ← DbContext, Configuracao
├─────────────────────────────────────────────────────────┤
│                    Base de Dados                           │
└─────────────────────────────────────────────────────────┘
```

Os Menus dependem apenas de Serviços; os Serviços dependem apenas de interfaces de Repositório; só os Repositórios conhecem o `GestaoInventarioContext`. 
Nenhuma lógica de negócio reside na camada de Infraestrutura — os repositórios limitam-se a ler e a gravar.

Operações que envolvem mais do que uma tabela — como registar um movimento de stock e atualizar a quantidade do produto — são coordenadas na camada `Servico`.

---

## Modelos de Dados

### Relações entre entidades

```
Categoria ──────────< Produto >──────────< MovimentoStock >──────────── Fornecedor
                                                                  (opcional, só em Entrada)
```

### Entidades principais

| Entidade | Descrição |
|---|---|
| `Categoria` | Categoria de produto (ex: Eletrónica, Papelaria) |
| `Fornecedor` | Fornecedor associado a movimentos de Entrada |
| `Produto` | Produto com preço, quantidade em stock e stock mínimo |
| `MovimentoStock` | Registo de Entrada, Saída ou Ajuste de stock |

---

## Decisões de Modelação

- **`FornecedorId` em `MovimentoStock` é opcional.** Um fornecedor só faz sentido semanticamente numa Entrada (receção de mercadoria) — em Saída e Ajuste não há fornecedor envolvido. A regra é validada na camada `Servico`, não imposta por `CHECK constraint` na base de dados.
- **Movimentos de stock são imutáveis.** Não existe operação de editar um `MovimentoStock` já registado — funciona como um livro-razão. Uma correção é feita através de um novo movimento do tipo `Ajuste`, que recalcula a quantidade em stock para o valor final correto.
- **`TipoMovimento` é gravado como `NVARCHAR`**, via `HasConversion<string>()`, alinhado por nome com o `CHECK constraint` da tabela (`'Entrada'`, `'Saida'`, `'Ajuste'`).

---

## Funcionalidades

### Categorias e Fornecedores

- Listar, Adicionar, Atualizar, Deletar
- Validação de duplicados (Nome de Categoria, NIF de Fornecedor)

### Produtos

- Listar, Adicionar, Atualizar, Deletar
- Listagem de categorias disponíveis antes de associar um produto
- Validação de código (SKU) duplicado

### Movimentos de Stock

- **Entrada** — regista receção de mercadoria, soma à quantidade em stock, associa fornecedor
- **Saída** — regista consumo/venda, valida que há stock suficiente antes de subtrair
- **Ajuste** — corrige a quantidade em stock para um valor final, sem editar movimentos anteriores

### Relatórios

- Produtos com quantidade em stock abaixo do stock mínimo definido

---

## Como Executar

### Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download)
- SQL Server (local ou remoto)

### Passos

----------------------------------------------------------------
# 1. Clonar o repositório
git clone https://github.com/teu-utilizador/GestaoInventario.git
cd GestaoInventario

# 2. Executar o script de criação da base de dados
# Executar CreateDB_GestaoInventario.sql no SQL Server Management Studio ou Azure Data Studio

# 3. Configurar a connection string em appsettings.json
# "ConnectionStrings": { "GestaoInventario": "Server=...;Database=GestaoInventario;..." }

# 4. Executar a aplicação
dotnet build
dotnet run

----------------------------------------------------------------

## Estrutura do Projeto


GestaoInventario/
│
├── Modelo/
│   ├── Entidades/          # Categoria, Fornecedor, Produto, MovimentoStock
│   ├── Enums/              # TipoMovimento
│   └── Interfaces/         # Contratos dos repositórios
│
├── Infraestrutura/
│   ├── Data/
│   │   └── GestaoInventarioContext.cs
│   ├── Configuracao/       # IEntityTypeConfiguration por entidade
│   └── Repositorio/        # Implementações concretas dos repositórios
│
├── Servico/                # Lógica de negócio
│   ├── CategoriaServico.cs
│   ├── FornecedorServico.cs
│   ├── ProdutoServico.cs
│   └── MovimentoStockServico.cs
│
├── Apresentacao/
│   └── Menu/
│       ├── MenuPrincipal.cs
│       ├── MenuGestao/     # MenuCategoria, MenuFornecedor, MenuProduto
│       └── MenuFluxo/      # MenuMovimentoStock, MenuRelatorio
│
├── CreateDB_GestaoInventario.sql
├── Program.cs
└── appsettings.json


---

## Notas de Desenvolvimento

- Todas as configurações de mapeamento do EF Core são feitas via `IEntityTypeConfiguration`, aplicadas com `ApplyConfigurationsFromAssembly` no `OnModelCreating`.
- Repositórios que precisam de navegação carregada usam `.Include()` com `Where` + `FirstOrDefaultAsync`, nunca `FindAsync` (que não suporta `Include`).
- Serviços que não encontram uma entidade lançam `KeyNotFoundException`; violações de regras de negócio lançam `InvalidOperationException`. Ambas são tratadas com `try/catch` na camada de Menu.
- Propriedades de navegação obrigatórias (`Produto.Categoria`, `MovimentoStock.Produto`) são inicializadas com `= null!`, evitando avisos `CS8618` sem recorrer a construtores extensos.
- `appsettings.json` deve ter **Copy to Output Directory** definido como `Copy if newer`, ou a aplicação não encontra a connection string em runtime.

---

*Projeto de treino pessoal desenvolvido em C# com Entity Framework Core e SQL Server.*
