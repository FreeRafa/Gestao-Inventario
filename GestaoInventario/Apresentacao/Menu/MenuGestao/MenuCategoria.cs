using GestaoInventario.Modelo.Entidades;
using GestaoInventario.Servico;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoInventario.Apresentacao.Menu.MenuGestao
{
    public class MenuCategoria
    {
        private readonly CategoriaServico _categoriaServico;

        public MenuCategoria(CategoriaServico categoriaServico)
        {
            _categoriaServico = categoriaServico;
        }

        public async Task ExibirAsyncCategoria()
        {
            bool continuar = true;
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== Gestão de Categorias ===");
                Console.WriteLine("1. Listar Categorias");
                Console.WriteLine("2. Adicionar Categoria");
                Console.WriteLine("3. Atualizar Categoria");
                Console.WriteLine("4. Deletar Categoria");
                Console.WriteLine("5. Voltar ao Menu Principal");
                Console.Write("Escolha uma opção: ");
                var opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        await ListarCategoriasAsync();
                        break;
                    case "2":
                        await AdicionarCategoriaAsync();
                        break;
                    case "3":
                        await AtualizarCategoriaAsync();
                        break;
                    case "4":
                        await DeletarCategoriaAsync();
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

        private async Task ListarCategoriasAsync()
        {
            var categorias = await _categoriaServico.ObterTodasCategoriasAsync();
            Console.WriteLine("=== Lista de Categorias ===");
            foreach (var categoria in categorias)
            {
                Console.WriteLine($"ID: {categoria.Id}, Nome: {categoria.Nome}");
            }
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private async Task AdicionarCategoriaAsync()
        {
            Console.WriteLine("=== Adicionar Categoria ===");
            Console.Write("Nome: ");
            var nome = Console.ReadLine() ?? string.Empty;
            var categoria = new Categoria { Nome = nome };
            await _categoriaServico.CriarCategoriaAsync(categoria);
            Console.WriteLine("Categoria adicionada com sucesso! Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private async Task AtualizarCategoriaAsync()
        {
            var produtos = await _categoriaServico.ObterTodasCategoriasAsync();
            Console.WriteLine("=== Escolha a categoria a ser atualizada ===");
            foreach (var produtoAtualizar in produtos)
            {
                Console.WriteLine($"Id: {produtoAtualizar.Id}, Nome: {produtoAtualizar.Nome}");
            }

            Console.WriteLine("=== Atualizar Categoria ===");
            Console.Write("ID da Categoria: ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            var categoria = await _categoriaServico.ObterPorIdAsync(id);
            if (categoria == null)
            {
                Console.WriteLine("Categoria não encontrada. Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }
            Console.Write($"Nome ({categoria.Nome}): ");
            var nome = Console.ReadLine();
            categoria.Nome = string.IsNullOrWhiteSpace(nome) ? categoria.Nome : nome;
            await _categoriaServico.AtualizarCategoriaAsync(categoria);
            Console.WriteLine("Categoria atualizada com sucesso! Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private async Task DeletarCategoriaAsync()
        {
            var produtos = await _categoriaServico.ObterTodasCategoriasAsync();
            Console.WriteLine("=== Escolha a categoria a ser deletada ===");
            foreach (var produtoAtualizar in produtos)
            {
                Console.WriteLine($"Id: {produtoAtualizar.Id}, Nome: {produtoAtualizar.Nome}");
            }

            Console.WriteLine("=== Deletar Categoria ===");
            Console.Write("ID da Categoria: ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            await _categoriaServico.DeletarCategoriaAsync(id);
            Console.WriteLine("Categoria deletada com sucesso! Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
    }
}
