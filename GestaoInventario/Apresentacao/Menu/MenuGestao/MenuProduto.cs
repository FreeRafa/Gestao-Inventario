using GestaoInventario.Modelo.Entidades;
using GestaoInventario.Servico;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoInventario.Apresentacao.Menu.MenuGestao
{
    public class MenuProduto
    {
        private readonly ProdutoServico _produtoServico;
        private readonly CategoriaServico _categoriaServico;

        public MenuProduto(ProdutoServico produtoServico, CategoriaServico categoriaServico)
        {
            _produtoServico = produtoServico;
            _categoriaServico = categoriaServico;
        }

        public async Task ExibirAsyncProduto()
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== Gestão de Produtos ===");
                Console.WriteLine("1. Listar Produtos");
                Console.WriteLine("2. Adicionar Produto");
                Console.WriteLine("3. Atualizar Produto");
                Console.WriteLine("4. Deletar Produto");
                Console.WriteLine("5. Voltar ao Menu Principal");
                Console.Write("Escolha uma opção: ");
                var opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        await ListarProdutosAsync();
                        break;

                    case "2":
                        await AdicionarProdutoAsync();
                        break;

                    case "3":
                        await AtualizarProdutoAsync();
                        break;

                    case "4":
                        await DeletarProdutoAsync();
                        break;

                    case "5":
                        return; // Voltar ao menu principal
                    default:

                        Console.WriteLine("Opção inválida. Pressione qualquer tecla para tentar novamente...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task ListarProdutosAsync()
        {
            var produtos = await _produtoServico.ObterTodosProdutosAsync();
            Console.WriteLine("=== Lista de Produtos ===");
            foreach (var produto in produtos)
            {
                Console.WriteLine($"Id: {produto.Id}, Nome: {produto.Nome}, Código: {produto.Codigo}, Quantidade em Stock: {produto.QuantidadeEmStock}");
            }
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private async Task AdicionarProdutoAsync()
        {
            Console.WriteLine("=== Adicionar Produto ===");
            Console.Write("Nome: ");
            var nome = Console.ReadLine();

            Console.Write("Código: ");
            var codigo = Console.ReadLine();

            Console.Write("Preço Unitário: ");
            var precoUnitario = decimal.Parse(Console.ReadLine() ?? "0");

            Console.Write("Quantidade em Stock: ");
            var quantidadeEmStock = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Stock Mínimo: ");
            var stockMinimo = int.Parse(Console.ReadLine() ?? "0");

            // Listar categorias disponíveis antes de pedir o Id
            var categorias = await _categoriaServico.ObterTodasCategoriasAsync();
            Console.WriteLine("=== Categorias disponíveis ===");
            foreach (var categoria in categorias)
            {
                Console.WriteLine($"Id: {categoria.Id}, Nome: {categoria.Nome}");
            }

            Console.Write("ID da Categoria: ");
            var categoriaId = int.Parse(Console.ReadLine() ?? "0");

            var produto = new Produto
            {
                Nome = nome ?? string.Empty,
                Codigo = codigo ?? string.Empty,
                PrecoUnitario = precoUnitario,
                QuantidadeEmStock = quantidadeEmStock,
                StockMinimo = stockMinimo,
                CategoriaId = categoriaId
            };

            try
            {
                await _produtoServico.CriarProdutoAsync(produto);
                Console.WriteLine("Produto adicionado com sucesso!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Erro ao adicionar produto: {ex.Message}");
            }
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private async Task AtualizarProdutoAsync()
        {
            var produtos = await _produtoServico.ObterTodosProdutosAsync();
            Console.WriteLine("=== Escolha o produto a ser atualizado ===");
            foreach (var produtoAtualizar in produtos)
            {
                Console.WriteLine($"Id: {produtoAtualizar.Id}, Nome: {produtoAtualizar.Nome}, Código: {produtoAtualizar.Codigo}, Quantidade em Stock: {produtoAtualizar.QuantidadeEmStock}");
            }

            Console.WriteLine("=== Atualizar Produto ===");
            Console.Write("ID do Produto: ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            var produto = await _produtoServico.ObterPorIdAsync(id);
            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado. Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Console.Write($"Nome ({produto.Nome}): ");
            var nome = Console.ReadLine();

            Console.Write($"Código ({produto.Codigo}): ");
            var codigo = Console.ReadLine();

            Console.Write($"Preço Unitário ({produto.PrecoUnitario}): ");
            var precoUnitarioInput = Console.ReadLine();
            var precoUnitario = string.IsNullOrWhiteSpace(precoUnitarioInput)
                ? produto.PrecoUnitario
                : decimal.Parse(precoUnitarioInput);

            Console.Write($"Quantidade em Stock ({produto.QuantidadeEmStock}): ");
            var quantidadeEmStockInput = Console.ReadLine();
            var quantidadeEmStock = string.IsNullOrWhiteSpace(quantidadeEmStockInput)
                ? produto.QuantidadeEmStock
                : int.Parse(quantidadeEmStockInput);

            Console.Write($"Stock Mínimo ({produto.StockMinimo}): ");
            var stockMinimoInput = Console.ReadLine();
            var stockMinimo = string.IsNullOrWhiteSpace(stockMinimoInput)
                ? produto.StockMinimo
                : int.Parse(stockMinimoInput);

            Console.WriteLine($"Categoria atual: {produto.Categoria.Nome} (ID: {produto.CategoriaId})");
            var categorias = await _categoriaServico.ObterTodasCategoriasAsync();
            Console.WriteLine("Categorias disponíveis:");
            foreach (var categoria in categorias)
            {
                Console.WriteLine($"  Id: {categoria.Id}, Nome: {categoria.Nome}");
            }
            Console.Write("Novo ID de Categoria (Enter para manter): ");
            var categoriaIdInput = Console.ReadLine();
            var categoriaId = string.IsNullOrWhiteSpace(categoriaIdInput)
                ? produto.CategoriaId
                : int.Parse(categoriaIdInput);

            produto.Nome = string.IsNullOrWhiteSpace(nome) ? produto.Nome : nome;
            produto.Codigo = string.IsNullOrWhiteSpace(codigo) ? produto.Codigo : codigo;
            produto.PrecoUnitario = precoUnitario;
            produto.QuantidadeEmStock = quantidadeEmStock;
            produto.StockMinimo = stockMinimo;
            produto.CategoriaId = categoriaId;

            try
            {
                await _produtoServico.AtualizarProdutoAsync(produto);
                Console.WriteLine("Produto atualizado com sucesso!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Erro ao atualizar produto: {ex.Message}");
            }
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private async Task DeletarProdutoAsync()
        {
            var produtos = await _produtoServico.ObterTodosProdutosAsync();
            Console.WriteLine("=== Escolha o produto a ser deletado ===");
            foreach (var produto in produtos)
            {
                Console.WriteLine($"Id: {produto.Id}, Nome: {produto.Nome}, Código: {produto.Codigo}, Quantidade em Stock: {produto.QuantidadeEmStock}");
            }
           
            Console.WriteLine("=== Deletar Produto ===");
            Console.Write("ID do Produto: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            Console.Write($"Tem a certeza que deseja apagar o produto com Id {id}? (S/N): ");
            var confirmacao = Console.ReadLine();
            if (!string.Equals(confirmacao, "S", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Operação cancelada.");
                Console.ReadKey();
                return;
            }

            try
            {
                var produtoDeletado = await _produtoServico.DeletarProdutoAsync(id);
                if (produtoDeletado == null)
                {
                    Console.WriteLine("Produto não encontrado.");
                }
                else
                {
                    Console.WriteLine("Produto deletado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar produto: {ex.Message}");
            }
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
    }
}
