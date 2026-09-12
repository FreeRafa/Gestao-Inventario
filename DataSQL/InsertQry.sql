USE GestaoInventario

INSERT INTO Categoria (Nome, Descricao)
VALUES 
    ('Eletrónica', 'Produtos eletrónicos em geral'),
    ('Papelaria', 'Material de escritório e papelaria');

INSERT INTO Fornecedor (Nome, Nif, Telefone, Email)
VALUES
    ('TechDistribuidora Lda', '500123456', '210123456', 'geral@techdist.pt'),
    ('PapelPlus Unipessoal', '501987654', '220987654', NULL);