using GestaoInventario.Infraestrutura.Repositorio;
using GestaoInventario.Modelo.Entidades;
using GestaoInventario.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace GestaoInventario.Servico
{
    public class ProdutoServico
    {
        private readonly IProdutoRepositorio _produtoRepositorio;

        public ProdutoServico(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public Task<List<Produto>> ObterTodosProdutosAsync()
        {
            return _produtoRepositorio.ObterTodosProdutosAsync();
        }

        public Task<Produto?> ObterPorIdAsync(int id)
        {
            return _produtoRepositorio.ObterPorIdAsync(id);
        }

        public async Task<Produto> CriarProdutoAsync(Produto produto)
        {
            var produtoExistente = await _produtoRepositorio.ObterPorCodigoAsync(produto.Codigo);

            if (produtoExistente != null)
            {
                throw new InvalidOperationException($"Já existe um produto com o código '{produto.Codigo}'.");
            }

            return await _produtoRepositorio.CriarProdutoAsync(produto);
        }

        public Task<Produto> AtualizarProdutoAsync(Produto produto)
        {
            return _produtoRepositorio.AtualizarProdutoAsync(produto);
        }

        public Task<Produto?> DeletarProdutoAsync(int id)
        {
            return _produtoRepositorio.DeletarProdutoAsync(id);
        }
    }
}
