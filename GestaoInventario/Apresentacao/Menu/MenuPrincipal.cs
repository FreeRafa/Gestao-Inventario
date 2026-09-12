using GestaoInventario.Apresentacao.Menu.MenuFluxo;
using GestaoInventario.Apresentacao.Menu.MenuGestao;
using System;

namespace GestaoInventario.Apresentacao.Menu
{
    public class MenuPrincipal
    {
        private readonly MenuCategoria _menuCategoria;
        private readonly MenuFornecedor _menuFornecedor;
        private readonly MenuProduto _menuProduto;
        private readonly MenuMovimentoStock _menuMovimentoStock;
        private readonly MenuRelatorio _menuRelatorio;

        public MenuPrincipal(
            MenuCategoria menuCategoria,
            MenuFornecedor menuFornecedor,
            MenuProduto menuProduto,
            MenuMovimentoStock menuMovimentoStock,
            MenuRelatorio menuRelatorio)
        {
            _menuCategoria = menuCategoria;
            _menuFornecedor = menuFornecedor;
            _menuProduto = menuProduto;
            _menuMovimentoStock = menuMovimentoStock;
            _menuRelatorio = menuRelatorio;
        }

        public async Task ExibirAsync()
        {
            bool continuar = true;
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== GestaoInventario ===");
                Console.WriteLine("1. Gestão de Categorias");
                Console.WriteLine("2. Gestão de Fornecedores");
                Console.WriteLine("3. Gestão de Produtos");
                Console.WriteLine("4. Movimentos de Stock");
                Console.WriteLine("5. Relatórios");
                Console.WriteLine("6. Sair");
                Console.Write("Escolha uma opção: ");
                var opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        await _menuCategoria.ExibirAsyncCategoria();
                        break;
                    case "2":
                        await _menuFornecedor.ExibirAsyncFornecedor();
                        break;
                    case "3":
                        await _menuProduto.ExibirAsyncProduto();
                        break;
                    case "4":
                        await _menuMovimentoStock.ExibirAsyncMovimentoStock();
                        break;
                    case "5":
                        await _menuRelatorio.ExibirAsyncRelatorio();
                        break;
                    case "6":
                        continuar = false;
                        Console.WriteLine("A sair. Até breve!");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla para tentar novamente...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}