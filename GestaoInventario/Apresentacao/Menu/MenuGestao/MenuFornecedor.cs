using GestaoInventario.Modelo.Entidades;
using GestaoInventario.Servico;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoInventario.Apresentacao.Menu.MenuGestao
{
    public class MenuFornecedor
    {
        private readonly FornecedorServico _fornecedorServico;

        public MenuFornecedor(FornecedorServico fornecedorServico)
        {
            _fornecedorServico = fornecedorServico;
        }

        public async Task ExibirAsyncFornecedor()
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== Gestão de Fornecedores ===");
                Console.WriteLine("1. Listar Fornecedores");
                Console.WriteLine("2. Adicionar Fornecedor");
                Console.WriteLine("3. Atualizar Fornecedor");
                Console.WriteLine("4. Deletar Fornecedor");
                Console.WriteLine("5. Voltar ao Menu Principal");
                Console.Write("Escolha uma opção: ");
                var opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        await ListarFornecedoresAsync();
                        break;
                    case "2":
                        await AdicionarFornecedorAsync();
                        break;
                    case "3":
                        await AtualizarFornecedorAsync();
                        break;
                    case "4":
                        await DeletarFornecedorAsync();
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

        private async Task ListarFornecedoresAsync()
        {
            var fornecedores = await _fornecedorServico.ObterTodosFornecedoresAsync();

            Console.WriteLine("=== Lista de Fornecedores ===");
            foreach (var fornecedor in fornecedores)
            {
                Console.WriteLine($"Id: {fornecedor.Id}, Nome: {fornecedor.Nome}, Nif: {fornecedor.Nif}, Telefone: {fornecedor.Telefone}, Email: {fornecedor.Email}");
            }
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private async Task AdicionarFornecedorAsync()
        {
            Console.WriteLine("=== Adicionar Fornecedor ===");
            Console.Write("Nome: ");
            var nome = Console.ReadLine() ?? string.Empty;
            Console.Write("NIF: ");
            var nif = Console.ReadLine() ?? string.Empty;
            Console.Write("Telefone: ");
            var telefone = Console.ReadLine() ?? string.Empty;
            Console.Write("Email: ");
            var email = Console.ReadLine();

            var fornecedor = new Fornecedor
            {
                Nome = nome,
                Nif = nif,
                Telefone = telefone,
                Email = email
            };

            try
            {
                await _fornecedorServico.CriarFornecedorAsync(fornecedor);
                Console.WriteLine("Fornecedor adicionado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar fornecedor: {ex.Message}");
            }

            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private async Task AtualizarFornecedorAsync()
        {
            var fornecedores = await _fornecedorServico.ObterTodosFornecedoresAsync();
            Console.WriteLine("=== Escolha o fornecedor que quer atualizar ===");
            foreach (var fornecedor in fornecedores)
            {
                Console.WriteLine($"Id: {fornecedor.Id}, Nome: {fornecedor.Nome}");
            }

            Console.Write("Id do Fornecedor: ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            var fornecedorAtual = await _fornecedorServico.ObterPorIdAsync(id);
            if (fornecedorAtual == null)
            {
                Console.WriteLine("Fornecedor não encontrado. Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Console.Write($"Novo Nome ({fornecedorAtual.Nome}): ");
            var nome = Console.ReadLine();

            Console.Write($"Novo NIF ({fornecedorAtual.Nif}): ");
            var nif = Console.ReadLine();

            Console.Write($"Novo Telefone ({fornecedorAtual.Telefone}): ");
            var telefone = Console.ReadLine();

            Console.Write($"Novo Email ({fornecedorAtual.Email}): ");
            var email = Console.ReadLine();

            fornecedorAtual.Nome = string.IsNullOrWhiteSpace(nome) ? fornecedorAtual.Nome : nome;
            fornecedorAtual.Nif = string.IsNullOrWhiteSpace(nif) ? fornecedorAtual.Nif : nif;
            fornecedorAtual.Telefone = string.IsNullOrWhiteSpace(telefone) ? fornecedorAtual.Telefone : telefone;
            fornecedorAtual.Email = string.IsNullOrWhiteSpace(email) ? fornecedorAtual.Email : email;

            try
            {
                await _fornecedorServico.AtualizarFornecedorAsync(fornecedorAtual);
                Console.WriteLine("Fornecedor atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar o fornecedor: {ex.Message}");
            }
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private async Task DeletarFornecedorAsync()
        {
            var fornecedores = await _fornecedorServico.ObterTodosFornecedoresAsync();
            Console.WriteLine("=== Escolha um fornecedor a ser deletado ===");
            foreach (var fornecedor in fornecedores)
            {
                Console.WriteLine($"Id: {fornecedor.Id}, Nome: {fornecedor.Nome}");
            }

            Console.WriteLine("=== Deletar Fornecedor ===");
            Console.Write("Id do Fornecedor: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            Console.Write($"Tem a certeza que deseja apagar este fornecedor com Id {id}? (S/N): ");
            var confirmacao = Console.ReadLine();
            if (!string.Equals(confirmacao, "S", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Operação cancelada.");
                Console.ReadKey();
                return;
            }

            try
            {
                var fornecedorDeletado = await _fornecedorServico.DeletarFornecedorAsync(id);
                if (fornecedorDeletado == null)
                {
                    Console.WriteLine("Fornecedor não encontrado.");
                }
                else
                {
                    Console.WriteLine("Fornecedor deletado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar fornecedor: {ex.Message}");
            }
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
    }
}