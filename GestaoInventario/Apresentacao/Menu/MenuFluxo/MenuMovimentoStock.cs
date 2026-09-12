using GestaoInventario.Modelo.Entidades;
using GestaoInventario.Servico;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoInventario.Apresentacao.Menu.MenuFluxo
{
    public class MenuMovimentoStock
    {
        private readonly MovimentoStockServico _movimentoStockServico;
        private readonly ProdutoServico _produtoServico;

        public MenuMovimentoStock(MovimentoStockServico movimentoStockServico, ProdutoServico produtoServico)
        {
            _movimentoStockServico = movimentoStockServico;
            _produtoServico = produtoServico;
        }

        public async Task ExibirAsyncMovimentoStock()
        {
            bool continuar = true;
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== Movimentos de Stock ===");
                Console.WriteLine("1. Listar Movimentos por Produto");
                Console.WriteLine("2. Registar Entrada");
                Console.WriteLine("3. Registar Saída");
                Console.WriteLine("4. Registar Ajuste");
                Console.WriteLine("5. Voltar ao Menu Principal");
                Console.Write("Escolha uma opção: ");
                var opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        await ListarMovimentosStockAsync();
                        break;
                    case "2":
                        await RegistarEntradaAsync();
                        break;
                    case "3":
                        await RegistarSaidaAsync();
                        break;
                    case "4":
                        await RegistarAjusteAsync();
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

        private async Task ListarMovimentosStockAsync()
        {
            Console.Write("ID do Produto: ");
            var produtoId = int.Parse(Console.ReadLine() ?? "0");
            var movimentos = await _movimentoStockServico.ObterMovimentosPorProdutoIdAsync(produtoId);
            foreach (var movimento in movimentos)
            {
                Console.WriteLine($"ID: {movimento.Id}, Tipo: {movimento.TipoMovimento}, Produto: {movimento.Produto.Nome}, Quantidade: {movimento.Quantidade}, Data: {movimento.Data}, Observação: {movimento.Observacao}");
            }
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private async Task RegistarEntradaAsync()
        {
            Console.Write("ID do Produto: ");
            var produtoId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Quantidade a entrar: ");
            var quantidade = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("ID do Fornecedor (Enter para nenhum): ");
            var fornecedorInput = Console.ReadLine();
            int? fornecedorId = string.IsNullOrWhiteSpace(fornecedorInput) ? null : int.Parse(fornecedorInput);

            Console.Write("Observação (opcional): ");
            var observacao = Console.ReadLine();

            try
            {
                await _movimentoStockServico.RegistarEntradaAsync(produtoId, quantidade, fornecedorId, observacao);
                Console.WriteLine("Entrada registada com sucesso!");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private async Task RegistarSaidaAsync()
        {
            Console.Write("ID do Produto: ");
            var produtoId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Quantidade a sair: ");
            var quantidade = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Observação (opcional): ");
            var observacao = Console.ReadLine();

            try
            {
                await _movimentoStockServico.RegistarSaidaAsync(produtoId, quantidade, observacao);
                Console.WriteLine("Saída registada com sucesso!");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private async Task RegistarAjusteAsync()
        {
            Console.Write("ID do Produto: ");
            var produtoId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Quantidade final correta em stock: ");
            var quantidadeFinal = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Motivo do ajuste (opcional): ");
            var observacao = Console.ReadLine();

            try
            {
                await _movimentoStockServico.RegistarAjusteAsync(produtoId, quantidadeFinal, observacao);
                Console.WriteLine("Ajuste registado com sucesso!");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
    }
}