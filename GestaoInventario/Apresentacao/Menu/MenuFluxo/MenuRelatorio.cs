using GestaoInventario.Servico;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoInventario.Apresentacao.Menu.MenuFluxo
{
    public class MenuRelatorio
    {
        private readonly ProdutoServico _produtoServico;

        public MenuRelatorio(ProdutoServico produtoServico)
        {
            _produtoServico = produtoServico;
        }

        public async Task ExibirAsyncRelatorio()
        {
            bool continuar = true;
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== Relatórios ===");
                Console.WriteLine("1. Produtos com Stock Abaixo do Mínimo");
                Console.WriteLine("2. Voltar ao Menu Principal");
                Console.Write("Escolha uma opção: ");
                var opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        await ListarStockAbaixoDoMinimoAsync();
                        break;
                    case "2":
                        return; // Voltar ao menu principal
                    default:
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla para tentar novamente...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task ListarStockAbaixoDoMinimoAsync()
        {
            var produtos = await _produtoServico.ObterProdutosComStockAbaixoDoMinimoAsync();

            Console.WriteLine("=== Produtos com Stock Abaixo do Mínimo ===");
            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto está abaixo do stock mínimo.");
            }
            else
            {
                foreach (var produto in produtos)
                {
                    Console.WriteLine($"Id: {produto.Id}, Nome: {produto.Nome}, Categoria: {produto.Categoria.Nome}, Stock Atual: {produto.QuantidadeEmStock}, Stock Mínimo: {produto.StockMinimo}");
                }
            }
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
    }
}