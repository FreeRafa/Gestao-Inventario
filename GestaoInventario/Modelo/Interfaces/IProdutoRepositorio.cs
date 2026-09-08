using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GestaoInventario.Modelo.Entidades;

namespace GestaoInventario.Modelo.Interfaces
{
    public interface IProdutoRepositorio
    {
        Task<Produto?> ObterPorIdAsync(int id);
        Task<Produto> CriarProdutoAsync(Produto produto);
        Task<Produto> AtualizarProdutoAsync(Produto produto);
        Task<Produto?> DeletarProdutoAsync(int id);
        Task<List<Produto>> ObterTodosProdutosAsync();
    }
}
