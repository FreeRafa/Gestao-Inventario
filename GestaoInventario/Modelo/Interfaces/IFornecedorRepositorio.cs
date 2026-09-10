using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GestaoInventario.Modelo.Entidades;

namespace GestaoInventario.Modelo.Interfaces
{
    public interface IFornecedorRepositorio
    {
        Task<Fornecedor?> ObterPorIdAsync(int id);
        Task<Fornecedor> CriarFornecedorAsync(Fornecedor fornecedor);
        Task<Fornecedor> AtualizarFornecedorAsync(Fornecedor fornecedor);
        Task<Fornecedor?> DeletarFornecedorAsync(int id);
        Task<List<Fornecedor>> ObterTodosFornecedoresAsync();
        Task<Fornecedor?> ObterPorNifAsync(string Nif);
    }
}
