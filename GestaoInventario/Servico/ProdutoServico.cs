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

        public Task<Produto> CriarProdutoAsync(Produto produto)
        {
            return _produtoRepositorio.CriarProdutoAsync(produto);
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
