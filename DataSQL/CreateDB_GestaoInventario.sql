CREATE DATABASE GestaoInventario

USE GestaoInventario

CREATE TABLE Categoria
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL UNIQUE,
    Descricao NVARCHAR(500) NULL
);

CREATE TABLE Fornecedor
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(150) NOT NULL,
    Nif NVARCHAR(20) NOT NULL UNIQUE,
    Telefone NVARCHAR(50) NOT NULL,
    Email NVARCHAR(150) UNIQUE
);

CREATE TABLE Produto
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(150) NOT NULL,
    Codigo NVARCHAR(50) NOT NULL UNIQUE,
    PrecoUnitario DECIMAL(10,2) NOT NULL,
    QuantidadeEmStock INT NOT NULL DEFAULT 0,
    StockMinimo INT NOT NULL DEFAULT 0,
    CategoriaId INT NOT NULL,

    CONSTRAINT CHK_Preco_NaoNegativo CHECK (PrecoUnitario >= 0),
    CONSTRAINT CHK_Stock_NaoNegativo CHECK (QuantidadeEmStock >= 0),
    CONSTRAINT CHK_StockMinimo_NaoNegativo CHECK (StockMinimo >= 0),

    CONSTRAINT FK_Categoria_Produto
        FOREIGN KEY (CategoriaId)
        REFERENCES Categoria(Id)
);

CREATE TABLE MovimentoStock
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    TipoMovimento NVARCHAR(20) NOT NULL CHECK (TipoMovimento IN ('Entrada', 'Saida', 'Ajuste')),
    Quantidade INT NOT NULL,
    Data DATETIME NOT NULL DEFAULT GETDATE(),
    Observacao NVARCHAR(500) NULL,
    ProdutoId INT NOT NULL,
    FornecedorId INT NULL,

    CONSTRAINT CHK_Quantidade_Positiva CHECK (Quantidade > 0),

    CONSTRAINT FK_Produto_MovimentoStock
        FOREIGN KEY (ProdutoId)
        REFERENCES Produto(Id),

    CONSTRAINT FK_Fornecedor_MovimentoStock
        FOREIGN KEY (FornecedorId)
        REFERENCES Fornecedor(Id)
);

