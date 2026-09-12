using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GestaoInventario.Modelo.Entidades;

namespace GestaoInventario.Modelo.Interfaces
{
    public interface IMovimentoStockRepositorio
    {
        Task<MovimentoStock?> ObterPorIdAsync(int id);
        Task<MovimentoStock> CriarMovimentoStockAsync(MovimentoStock movimentoStock);
        Task<MovimentoStock> AtualizarMovimentoStockAsync(MovimentoStock movimentoStock);
        Task<MovimentoStock?> DeletarMovimentoStockAsync(int id);
        Task<List<MovimentoStock>> ObterMovimentosPorProdutoIdAsync(int produtoId);
        
    }
}
