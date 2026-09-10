using GestaoInventario.Infraestrutura.Repositorio;
using GestaoInventario.Modelo.Entidades;
using GestaoInventario.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoInventario.Servico
{
    public class FornecedorServico
    {
        private readonly IFornecedorRepositorio _fornecedorRepositorio;
        public FornecedorServico(IFornecedorRepositorio fornecedorRepositorio)
        {
            _fornecedorRepositorio = fornecedorRepositorio;
        }

        public Task<List<Fornecedor>> ObterTodosFornecedoresAsync()
        {
            return _fornecedorRepositorio.ObterTodosFornecedoresAsync();
        }

        public Task<Fornecedor?> ObterPorIdAsync(int id)
        {
            return _fornecedorRepositorio.ObterPorIdAsync(id);
        }

        public Task<Fornecedor> CriarFornecedorAsync(Fornecedor fornecedor)
        {
            return _fornecedorRepositorio.CriarFornecedorAsync(fornecedor);
        }

        public Task<Fornecedor> AtualizarFornecedorAsync(Fornecedor fornecedor)
        {
            return _fornecedorRepositorio.AtualizarFornecedorAsync(fornecedor);
        }

        public Task<Fornecedor?> DeletarFornecedorAsync(int id)
        {
            return _fornecedorRepositorio.DeletarFornecedorAsync(id);
        }
    }
}
