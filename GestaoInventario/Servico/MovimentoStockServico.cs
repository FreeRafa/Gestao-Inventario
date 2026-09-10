using GestaoInventario.Modelo.Entidades;
using GestaoInventario.Modelo.Enums;
using GestaoInventario.Modelo.Interfaces;

namespace GestaoInventario.Servico
{
    public class MovimentoStockServico
    {
        private readonly IMovimentoStockRepositorio _movimentoStockRepositorio;
        private readonly IProdutoRepositorio _produtoRepositorio;

        public MovimentoStockServico(
            IMovimentoStockRepositorio movimentoStockRepositorio,
            IProdutoRepositorio produtoRepositorio)
        {
            _movimentoStockRepositorio = movimentoStockRepositorio;
            _produtoRepositorio = produtoRepositorio;
        }

        public Task<List<MovimentoStock>> ObterMovimentosPorProdutoIdAsync(int produtoId)
        {
            return _movimentoStockRepositorio.ObterMovimentosPorProdutoIdAsync(produtoId);
        }

        public Task<MovimentoStock?> ObterPorIdAsync(int id)
        {
            return _movimentoStockRepositorio.ObterPorIdAsync(id);
        }

        public async Task<MovimentoStock> RegistarEntradaAsync(int produtoId, int quantidade, int? fornecedorId, string? observacao)
        {
            var produto = await _produtoRepositorio.ObterPorIdAsync(produtoId);
            if (produto is null)
                throw new KeyNotFoundException($"Produto com Id {produtoId} não encontrado.");

            produto.QuantidadeEmStock += quantidade;
            await _produtoRepositorio.AtualizarProdutoAsync(produto);

            var movimento = new MovimentoStock
            {
                TipoMovimento = TipoMovimento.Entrada,
                Quantidade = quantidade,
                ProdutoId = produtoId,
                FornecedorId = fornecedorId,
                Observacao = observacao
            };

            return await _movimentoStockRepositorio.CriarMovimentoStockAsync(movimento);
        }

        public async Task<MovimentoStock> RegistarSaidaAsync(int produtoId, int quantidade, string? observacao)
        {
            var produto = await _produtoRepositorio.ObterPorIdAsync(produtoId);
            if (produto is null)
                throw new KeyNotFoundException($"Produto com Id {produtoId} não encontrado.");

            if (produto.QuantidadeEmStock < quantidade)
                throw new InvalidOperationException(
                    $"Stock insuficiente para '{produto.Nome}'. Disponível: {produto.QuantidadeEmStock}, pedido: {quantidade}.");

            produto.QuantidadeEmStock -= quantidade;
            await _produtoRepositorio.AtualizarProdutoAsync(produto);

            var movimento = new MovimentoStock
            {
                TipoMovimento = TipoMovimento.Saida,
                Quantidade = quantidade,
                ProdutoId = produtoId,
                FornecedorId = null, // reforça a regra: Saída nunca tem Fornecedor
                Observacao = observacao
            };

            return await _movimentoStockRepositorio.CriarMovimentoStockAsync(movimento);
        }
    }
}